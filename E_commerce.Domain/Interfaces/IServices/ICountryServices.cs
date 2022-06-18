using E_commerce.Core.Models;
using E_commerce.Domain.ViewModels;
using E_commerce.Domain.ViewModels.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Interfaces.IServices
{
    public interface ICountryServices
    {
        /// <summary>
        ///     Check of Country Exist. 
        /// </summary>
        /// <param name="expression">Expresses the properties that be find by it.</param>
        /// <returns>True in Case Country Exist, or False in Case Country not Exist</returns>
        Task<bool> AnyAsync(Expression<Func<Country, bool>> expression);

        /// <summary>
        ///     Get All Country.
        /// </summary>
        /// <returns>List of CountryViewModel</returns>
        Task<List<CountryViewModel>> GetAllCountryAsync();

        /// <summary>
        ///     UpDate Country Name.
        /// </summary>
        /// <param name="id">Expresses country id</param>
        /// <param name="name">Expresses new country name</param>
        /// <returns>Status Code</returns>
        Task<int> UpDateAsync(string id, string name);

        /// <summary>
        ///     Delete country from database by country id.
        /// </summary>
        /// <param name="id">Expresses country id.</param>
        /// <returns>200 in case delete success, or 404 in case country not found, or 500 in case bad request</returns>
        Task<int> DeleteAsync(string id);

        /// <summary>
        ///     Craete a new country from viewModel.
        /// </summary>
        /// <param name="viewModel">Expresses AddCountryViewModel.</param>
        /// <returns>True in case created successfully, or False in case created failed.</returns>
        Task<bool> AddFromViewModelAsync(AddCountryViewModel viewModel);
    }
}
