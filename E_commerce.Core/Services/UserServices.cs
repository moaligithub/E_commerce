using AutoMapper;
using E_commerce.Core.Const;
using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Core.ViewModels.Admin;
using E_commerce.Domain.ViewModels.User;
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
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _repository;
        private readonly IToastNotification _toastNotification;

        public UserServices(IUnitOfWork repository, IToastNotification toastNotification)
        {
            _repository = repository;
            _toastNotification = toastNotification;
        }

        public async Task<bool> AnyAsync(Expression<Func<ApplicationUser, bool>> expression)
           => await _repository.User.AnyAsync(expression);

        public bool EmailIsExist(string email)
            => _repository.User.Any(user => user.Email == email);

        public async Task<bool> EmailIsExistAsync(string email)
            => await _repository.User.AnyAsync(user => user.Email == email);

        public List<ApplicationUser> FilterUsersIsActive(IEnumerable<ApplicationUser> applicationUsers) 
            => applicationUsers.Where(user => user.IsActive == true).ToList();

        public async Task<UserEditViewModel> GetUserForEditByIdAsync(string id)
        {
            UserEditViewModel user = new UserEditViewModel();

            if(_repository.User.AnyAsync(user => user.Id == id).Result)
            {
                City city = await _repository.User.GetUserCityByUserIdAsync(id);

                user.FirstName = await _repository.User.GetFirstNameByIdAsync(id);
                user.LastName = await _repository.User.GetLastNameByIdAsync(id);
                user.PhoneNumber = await _repository.User.GetPhoneNumberByIdAsync(id);
                user.CityId = city.Id.ToString();
                user.CountryId = city.CountryId.ToString();
                user.UserName = await _repository.User.GetUserNameByIdAsync(id);
                user.Id = id;
            }

            return user;
        }

        public bool IsAdmin(string userId) 
            => _repository.AdminProperties.Any(User => User.UserId == userId); 

        public async Task<bool> IsAdminAsync(string userId)
            => await _repository.AdminProperties.AnyAsync(User => User.UserId == userId);

        public async Task<bool> UpDateUserFromViewModelAsync(UserEditViewModel viewModel)
        {
            ApplicationUser user = await _repository.User.FindAsync(user => user.UserName ==  viewModel.UserName,
                new Expression<Func<ApplicationUser, object>>[] { include => include.City });

            if (viewModel.FirstName != user.FirstName)
                user.FirstName = viewModel.FirstName;

            if (viewModel.LastName != user.LastName)
                user.LastName = viewModel.LastName;

            if (viewModel.PhoneNumber != user.PhoneNumber)
                user.PhoneNumber = viewModel.PhoneNumber;

            if (viewModel.CityId != user.City.Id.ToString())
                user.City = await _repository.City.GetByIdAsync(Guid.Parse(viewModel.CityId));

            try
            {
                _repository.User.UpDate(user);
                _repository.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry!, Update failed");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("UpDate successfully");
            return true;
        }

        public async Task<bool> IsActiveByUserNameAsync(string userName)
            => await _repository.User.ISActiveByUserNameAsync(userName);
    }
}