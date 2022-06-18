using E_commerce.Core.Models;
using E_commerce.Core.ViewModels.Admin;
using E_commerce.Core.ViewModels.AdminActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Interfaces.IServices
{
    public interface IAdminServices
    {
        /// <summary>
        ///     Delete Admin By Admin Id.
        /// </summary>
        /// <param name="id">Expresses Admin Id</param>
        /// <returns>True In Case Dlete Successfully, or False In Case Delete Failed</returns>
        bool DeleteById(string id);

        /// <summary>
        ///     Filter Users on condition User Is Active.
        /// </summary>
        /// <param name="applicationUsers">Users that being filter it</param>
        /// <returns>List of Application User</returns>
        List<ApplicationUser> FilterAdminsIsActive(IEnumerable<ApplicationUser> applicationUsers);

        /// <summary>
        ///     Get Admin Details by email for display in view. 
        /// </summary>
        /// <param name="email">Expresses Admin Eamil</param>
        /// <returns>Admin Details type of AdminDetailsViewModel</returns>
        Task<AdminDetailsViewModel> GetAdminDetailsByEmailAsync(string email);

        /// <summary>
        ///     Get Admin for Edit by email for display in view. 
        /// </summary>
        /// <param name="email">Expresses Admin Eamil</param>
        /// <returns>Admin Details for edit type of AdminEditViewModel</returns>
        Task<AdminEditViewModel> GetAdminForEditByIdAsync(string email);

        /// <summary>
        ///     Check of Email existing 
        /// </summary>
        /// <param name="email">Expresses Email that being checking</param>
        /// <returns>True In Case Email Exist, or False In Case Email not Exist.</returns>
        Task<bool> EmailIsExistAsync(string email);

        /// <summary>
        ///     UpDate Admin From View Model.
        /// </summary>
        /// <param name="viewModel">Expresses View Model</param>
        /// <returns>True In Case UpDate Successfully, or False In Case Updated Failed</returns>
        Task<bool> UpDateAdminFromViewModelAsync(AdminEditViewModel viewModel);

        /// <summary>
        ///     Create Admin From View Model.
        /// </summary>
        /// <param name="viewModel">Expresses View Model</param>
        /// <returns>True In Case Create Successfully, or False In Case Create Failed</returns>
        Task<bool> CreateAdminFromViewModelAsync(CreateAdminViewModel viewModel);

        /// <summary>
        ///     Get Admin Activities for Admin By the email , and be get activities in Year And month And Day.
        /// </summary>
        /// <param name="email">Expresses admin email</param>
        /// <param name="year">Expresses Year, And be get activities in this year.</param>
        /// <param name="month">Expresses month, And be get activities in this month.</param>
        /// <param name="day">Expresses day, And be get activities in this day.</param>
        /// <returns>List of Admin Activities type of Admin ActivityViewModel.</returns>
        Task<List<AdminActivityViewModel>> GetAdminActivitiesAsync(string email, int year, int month, int day);

        /// <summary>
        ///     Check of Admin Exist. 
        /// </summary>
        /// <param name="expression">Expresses the properties that be find by it.</param>
        /// <returns>True in Case Admin Exist, or False in Case Admin not Exist</returns>
        Task<bool> AnyAsync(Expression<Func<ApplicationUser, bool>> expression);
        
        /// <summary>
        ///     Admin Deactivate By Admin id.
        /// </summary>
        /// <param name="id">Expresses Admin Id</param>
        /// <returns>True in Case Deactivate Successfully, or False in Case Deactivate Failed</returns>
        Task<bool> DeactivateByIdAsync(string id);

        /// <summary>
        ///     Admin Activation By Admin id.
        /// </summary>
        /// <param name="id">Expresses Admin Id</param>
        /// <returns>True in Case Activation Successfully, or False in Case Activation Failed</returns>
        Task<bool> ActivationByIdAsync(string id);

        /// <summary>
        ///     Get All Admin.
        /// </summary>
        /// <returns>List of AdminIndexViewModel</returns>
        Task<List<AdminsIndexViewModel>> GetAllAdminAsync();

        /// <summary>
        ///     Get All Stats For Admin Activity In Year.
        /// </summary>
        /// <param name="year">Expresses The Year, And Being Get All Stats In This Year.</param>
        /// <returns>AdminActivityStatsViewModel</returns>
        Task<AdminActivityStatsViewModel> GetAllStatsForAdminActivityInYear(int year, string AdminId);

        /// <summary>
        ///     Check of user is active or not active.
        /// </summary>
        /// <param name="userName">Expresses user Name.</param>
        /// <returns>True in case this user is active or False in case this user is not active.</returns>
        Task<bool> IsActiveByUserNameAsync(string userName);
    }
}
