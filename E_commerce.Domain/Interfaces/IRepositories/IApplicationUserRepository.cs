using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Interfaces.IRepositories
{
    public interface IApplicationUserRepository : IBaseRepository<ApplicationUser>
    {
        // Sync Methods

        /// <summary>
        ///     Check of user is active or is not active.
        /// </summary>
        /// <param name="Id">benig Check of user by this id.</param>
        /// <returns>true in case the user is active or false in case the user is not active</returns>
        bool ISActive(string id);

        // Async Method

        /// <summary>
        ///     Check of user is active or is not active.
        /// </summary>
        /// <param name="Id">benig Check of user by this id.</param>
        /// <returns>true in case the user is active or false in case the user is not active</returns>
        Task<bool> ISActiveAsync(string id);

        /// <summary>
        ///     Check of user is active or is not active.
        /// </summary>
        /// <param name="username">benig Check of user by this userName.</param>
        /// <returns>true in case the user is active or false in case the user is not active</returns>
        Task<bool> ISActiveByUserNameAsync(string username);

        /// <summary>
        ///     Get User Name By User Id
        /// </summary>
        /// <param name="id">Expresses User Id</param>
        /// <returns>User Name of string data type</returns>
        Task<string> GetUserNameByIdAsync(string id);

        /// <summary>
        ///     Get User Email By User Id
        /// </summary>
        /// <param name="id">Expresses User Id</param>
        /// <returns>User Email of string data type</returns>
        Task<string> GetUserEmailByIdAsync(string id);

        /// <summary>
        ///     Get First Name By User Id
        /// </summary>
        /// <param name="id">Expresses User Id</param>
        /// <returns>First Name of string data type</returns>
        Task<string> GetFirstNameByIdAsync(string id);

        /// <summary>
        ///     Get Last Name By User Id
        /// </summary>
        /// <param name="id">Expresses User Id</param>
        /// <returns>Last Name of string data type</returns>
        Task<string> GetLastNameByIdAsync(string id);

        /// <summary>
        ///     Get Phone Number By User Id
        /// </summary>
        /// <param name="id">Expresses User Id</param>
        /// <returns>Phone Number of string data type</returns>
        Task<string> GetPhoneNumberByIdAsync(string id);

        /// <summary>
        ///     Get User City By User Id
        /// </summary>
        /// <param name="id">Expresses User Id</param>
        /// <returns>User City of City data type</returns>
        Task<City> GetUserCityByUserIdAsync(string id);
    }
}