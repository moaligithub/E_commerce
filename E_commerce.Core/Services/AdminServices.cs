using AutoMapper;
using E_commerce.Core.Const;
using E_commerce.Core.Consts;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Core.ViewModels.Admin;
using E_commerce.Core.ViewModels.AdminActivity;
using E_commerce.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IUnitOfWork _repositories;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly FileHelper _fileHelper;
        private readonly StatsHelper _statsHelper;

        public AdminServices(IUnitOfWork repositories, UserManager<ApplicationUser> userManager,
                                IMapper mapper, IToastNotification toastNotification)
        {
            _repositories = repositories;
            _userManager = userManager;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _fileHelper = new FileHelper();
            _statsHelper = new StatsHelper(_repositories);
        }

        public async Task<bool> UpDateAdminFromViewModelAsync(AdminEditViewModel viewModel)
        {
            if (viewModel.File != null)
            {
                string[] extensions = { Extensions.JPG, Extensions.PNG };

                if (!_fileHelper.ValidExtension(viewModel.File, extensions))
                {
                    _toastNotification.AddErrorToastMessage("Only , jpg, png Extensioms");
                    return false;
                }

                if (!_fileHelper.ValidSize(viewModel.File, FilesSize.MB1))
                {
                    _toastNotification.AddErrorToastMessage("Sorry, 1 Mb Is Max Size For Files");
                    return false;
                }
            }

            ApplicationUser admin = await _repositories.User.FindAsync(x => x.Id == viewModel.Id,
                new Expression<Func<ApplicationUser, object>>[] { x => x.City});

            AdminProperties adminProperties = await _repositories.AdminProperties.FindAsync(x => x.UserId == admin.Id,
                   new Expression<Func<AdminProperties, object>>[] { x => x.Branch });

            if (viewModel.FirstName != admin.FirstName)
                admin.FirstName = viewModel.FirstName;

            if (viewModel.LastName != admin.LastName)
                admin.LastName = viewModel.LastName;

            if (viewModel.PhoneNumber != admin.PhoneNumber)
                admin.PhoneNumber = viewModel.PhoneNumber;

            if (viewModel.Email != admin.Email)
                admin.Email = viewModel.Email;

            if (viewModel.File != null)
                admin.AdminProperties.ProfilePicture = _fileHelper.UpLoadeToDatabase(viewModel.File);

            if (adminProperties.Branch != null)
            {
                if (viewModel.BranchId != adminProperties.Branch.Id)
                    admin.AdminProperties.Branch = await _repositories.Branch.GetByIdAsync(viewModel.BranchId);
            }

            if (viewModel.CityId != admin.City.Id.ToString())
                admin.City = await _repositories.City.FindAsync(city => city.Id == Guid.Parse(viewModel.CityId));

            try
            {
                _repositories.User.UpDate(admin);
                _repositories.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed Save Changes");
                return false;
            }

            return true;
        }

        public List<ApplicationUser> FilterUsersIsActive(IEnumerable<ApplicationUser> applicationUsers)
            => applicationUsers.Where(user => user.IsActive == true).ToList();

        public async Task<AdminDetailsViewModel> GetAdminDetailsByEmailAsync(string id)
        {
            ApplicationUser adminDetails = await _repositories.AdminProperties.GetAdminDetailsByIdAsync(id);

            // Check of user exiting
            if (adminDetails == null)
                return null;

            // Mapping ApplicationUser with AdminDetailsViewModel
            AdminDetailsViewModel adminDetailsView = _mapper.Map<AdminDetailsViewModel>(adminDetails);

            adminDetailsView.AdminActivityStats = await GetAllStatsForAdminActivityInYear(DateTime.Now.Year, adminDetails.Id);

            return adminDetailsView;
        }

        public async Task<AdminEditViewModel> GetAdminForEditByIdAsync(string id)
        {
            AdminEditViewModel user = new AdminEditViewModel();

            if (_repositories.User.AnyAsync(user => user.Id == id).Result)
            {
                City city = await _repositories.User.GetUserCityByUserIdAsync(id);
                Branch branch = await _repositories.AdminProperties.GetAdminBranchByAdminIdAsync(id);

                user.FirstName = await _repositories.User.GetFirstNameByIdAsync(id);
                user.LastName = await _repositories.User.GetLastNameByIdAsync(id);
                user.PhoneNumber = await _repositories.User.GetPhoneNumberByIdAsync(id);
                user.CityId = city.Id.ToString();
                user.CountryId = city.CountryId.ToString();
                user.UserName = await _repositories.User.GetUserNameByIdAsync(id);
                user.ProfilePicture = await _repositories.AdminProperties.GetProfilePictureByIdAsync(id);
                user.Email = await _repositories.User.GetUserEmailByIdAsync(id);
                user.Id = id;

                if (branch != null)
                    user.BranchId = branch.Id;
            }

            return user;
        }

        public async Task<bool> CreateAdminFromViewModelAsync(CreateAdminViewModel viewModel)
        {
            if (await EmailIsExistAsync(viewModel.Email))
            {
                _toastNotification.AddErrorToastMessage("Thie Email already Exsits");
                return false;
            }

            if (viewModel.File != null)
            {
                string[] extensions = { Extensions.JPG, Extensions.PNG };

                if (!_fileHelper.ValidExtension(viewModel.File, extensions))
                {
                    _toastNotification.AddErrorToastMessage("Only , jpg, png Extensioms");
                    return false;
                }

                if (!_fileHelper.ValidSize(viewModel.File, FilesSize.MB1))
                {
                    _toastNotification.AddErrorToastMessage("Sorry, 1 Mb Is Max Size For Files");
                    return false;
                }
            }

            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(viewModel);
            applicationUser.UserName = new MailAddress(applicationUser.Email).User;
            applicationUser.IsActive = true;

            applicationUser.City = await _repositories.City.GetByIdAsync(viewModel.CityId);

            var Result = await _userManager.CreateAsync(applicationUser, viewModel.Password);

            // Check of Create user successfully
            if (!Result.Succeeded)
                return false;

            AdminProperties adminProperties = new AdminProperties();
            adminProperties.Id = Guid.Parse(applicationUser.Id);
            adminProperties.UserId = applicationUser.Id;
            adminProperties.Branch = await _repositories.Branch.GetByIdAsync(viewModel.BranchId);

            if (viewModel.File != null)
                adminProperties.ProfilePicture = _fileHelper.UpLoadeToDatabase(viewModel.File);

            try
            {
                await _repositories.AdminProperties.AddAsync(adminProperties);
                _repositories.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry!.. Can`t Added This Admin");
                return false;
            }

            // Add this user to role
            await _userManager.AddToRoleAsync(applicationUser, Roles.Admin);

            return true;
        }

        public List<ApplicationUser> FilterAdminsIsActive(IEnumerable<ApplicationUser> applicationUsers)
            => applicationUsers.Where(admin => admin.IsActive == true).ToList();

        public async Task<bool> EmailIsExistAsync(string email)
            => await _repositories.User.AnyAsync(user => user.Email == email);

        public async Task<List<AdminActivityViewModel>> GetAdminActivitiesAsync(string email, int year, int month, int day)
        {
            List<AdminActivity> adminActivities = await _repositories.AdminProperties
                .GetAdminAcitviesByEmailAsync(email, year, month, day);

            return _mapper.Map<List<AdminActivityViewModel>>(adminActivities);
        }

        public async Task<bool> AnyAsync(Expression<Func<ApplicationUser, bool>> expression)
            => await _repositories.User.AnyAsync(expression);
      
        public bool DeleteById(string id)
        {
            ApplicationUser admin = _repositories.User.GetById(id);

            if (admin == null)
                return false;

            try
            {
                _repositories.User.Delete(admin);
                _repositories.Complete();
            }
            catch 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeactivateByIdAsync(string id)
        {
            ApplicationUser user = await _repositories.User.GetByIdAsync(id);
            user.IsActive = false;

            try
            {
                _repositories.User.UpDate(user);
                _repositories.Complete();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ActivationByIdAsync(string id)
        {
            ApplicationUser user = await _repositories.User.GetByIdAsync(id);
            user.IsActive = true;

            try
            {
                _repositories.User.UpDate(user);
                _repositories.Complete();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<List<AdminsIndexViewModel>> GetAllAdminAsync()
        {
            IList<ApplicationUser> Result = await _userManager.GetUsersInRoleAsync(Roles.Admin);
            List<AdminsIndexViewModel> adminsIndexViewModel = Result.Select(x => new AdminsIndexViewModel
            {
                Email = x.Email,
                FullName = x.FirstName + " " + x.LastName,
                IsActive = x.IsActive,
                PhoneNumber = x.PhoneNumber,
                ProfilePicture = _repositories.AdminProperties.GetProfilePictureByIdAsync(x.Id).Result
            }).ToList();

            return adminsIndexViewModel;
        }

        public async Task<AdminActivityStatsViewModel> GetAllStatsForAdminActivityInYear(int year, string AdminId)
        {
            AdminActivityStatsViewModel adminActivityStats = new AdminActivityStatsViewModel
            {
                // Get the Added Stats
                AddedStats = await _statsHelper.StatsForAdminActionToProductsInYearAsync(year, AdminActivitiesTypes.Add, AdminId),

                // Get the Updated Stats
                UpDateStats = await _statsHelper.StatsForAdminActionToProductsInYearAsync(year, AdminActivitiesTypes.UpDate, AdminId),

                // Get the Deleted Stats
                DeleteStats = await _statsHelper.StatsForAdminActionToProductsInYearAsync(year, AdminActivitiesTypes.Delete, AdminId),

                // Get The Realtive Stats For Added
                RelativeStatsForAdded = await _statsHelper.RelativeStatsForAdminActionInYear(year, AdminActivitiesTypes.Add, AdminId),

                // Get The Realtive Stats For UpDated 
                RelativeStatsForUpDated = await _statsHelper.RelativeStatsForAdminActionInYear(year, AdminActivitiesTypes.UpDate, AdminId),

                // Get The Realtive Stats For Deleted

                RelativeStatsForDeleted = await _statsHelper.RelativeStatsForAdminActionInYear(year, AdminActivitiesTypes.Delete, AdminId)
            };

            return adminActivityStats;
        }

        public async Task<bool> IsActiveByUserNameAsync(string userName) 
            => await _repositories.User.ISActiveByUserNameAsync(userName);
    }
}