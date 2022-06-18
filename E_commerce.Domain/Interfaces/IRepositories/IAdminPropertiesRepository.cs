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
    public interface IAdminPropertiesRepository : IBaseRepository<AdminProperties>
    {
        /// <summary>
        ///     Get Admin By Email And Admin Details.
        /// </summary>
        /// <param name="email">This Parameter expresses admin email, And be get admin by this email.</param>
        /// <returns>Return Single Application User or Null</returns>
        Task<ApplicationUser> GetAdminDetailsByIdAsync(string email);

        /// <summary>
        ///     Get Admin Activities.
        /// </summary>
        /// <param name="year">Expresses the year, And benig get activities in this year</param>
        /// <param name="month">Expresses the month, And benig get activities in this month</param>
        /// <param name="day">Expresses the day, And benig get activities in this day</param>
        /// <returns>List of Admin Acivity type of AdminActivity</returns>
        Task<List<AdminActivity>> GetAdminAcitviesByEmailAsync(string email, int year, int month, int day);

        /// <summary>
        ///     Get the profile picture for admin by admin id.
        /// </summary>
        /// <param name="id">Expresses admin id.</param>
        /// <returns>array of byte expresses the profile picture</returns>
        Task<byte[]> GetProfilePictureByIdAsync(string id);


        /// <summary>
        ///     Get the profile picture for admin by Email.
        /// </summary>
        /// <param name="email">Expresses Email.</param>
        /// <returns>array of byte expresses the profile picture</returns>
        byte[] GetProfilePictureByEmailAsync(string email);

        /// <summary>
        ///     Get admin branch by admin id
        /// </summary>
        /// <param name="id">Expresses admin id</param>
        /// <returns>Admin branch type of branch.</returns>
        Task<Branch> GetAdminBranchByAdminIdAsync(string id);
    }
}
