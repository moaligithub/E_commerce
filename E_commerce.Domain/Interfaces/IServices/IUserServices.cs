using E_commerce.Core.Models;
using E_commerce.Core.ViewModels.Admin;
using E_commerce.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Interfaces.IServices
{
    public interface IUserServices
    {
        /// <summary>
        ///     Filter Users on condition User Is Active.
        /// </summary>
        /// <param name="applicationUsers">Users that being filter it</param>
        /// <returns>List of Application User</returns>
        List<ApplicationUser> FilterUsersIsActive(IEnumerable<ApplicationUser> applicationUsers);

        // ASync Method

        /// <summary>
        ///     this Sync Method It Working on Check User Type.
        /// </summary>
        /// <param name="user">Check User Type By This Parameter</param>
        /// <returns>Return True if This User of Admin Type, Or Return False If This User Not of Admin Type</returns>
        Task<bool> IsAdminAsync(string userId);

        /// <summary>
        ///     Check of Email existing 
        /// </summary>
        /// <param name="email">Expresses Email that being checking</param>
        /// <returns>True In Case Email Exist, or False In Case Email not Exist.</returns>
        Task<bool> EmailIsExistAsync(string email);

        /// <summary>
        ///     Check of User Exist. 
        /// </summary>
        /// <param name="expression">Expresses the properties that be find by it.</param>
        /// <returns>True in Case Admin Exist, or False in Case User not Exist</returns>
        Task<bool> AnyAsync(Expression<Func<ApplicationUser, bool>> expression);

        /// <summary>
        ///     Get User Details by email for display in view. 
        /// </summary>
        /// <param name="email">Expresses User Eamil</param>
        /// <returns>User Details type of UserEditViewModel</returns>
        Task<UserEditViewModel> GetUserForEditByIdAsync(string id);

        /// <summary>
        ///     Update user from UserEditViewModel.
        /// </summary>
        /// <param name="viewModel">Expresses UserEditViewModel</param>
        /// <returns>True in case Update successfully, or False in case Update failed.</returns>
        Task<bool> UpDateUserFromViewModelAsync(UserEditViewModel viewModel);

        /// <summary>
        ///     Check of user is active or not active.
        /// </summary>
        /// <param name="userName">Expresses user Name.</param>
        /// <returns>True in case this user is active or False in case this user is not active.</returns>
        Task<bool> IsActiveByUserNameAsync(string userName);
    }
}
