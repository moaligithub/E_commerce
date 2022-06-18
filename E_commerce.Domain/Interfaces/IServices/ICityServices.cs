using E_commerce.Core.Models;
using E_commerce.Domain.ViewModels.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Interfaces.IServices
{
    public interface ICityServices
    {
        /// <summary>
        ///     Check of City Exist. 
        /// </summary>
        /// <param name="expression">Expresses the properties that be find by it.</param>
        /// <returns>True in Case City Exist, or False in Case City not Exist</returns>
        Task<bool> AnyAsync(Expression<Func<City, bool>> expression);

        /// <summary>
        ///     Get Cities In Country By Country Id.
        /// </summary>
        /// <param name="CountryId">Expresses Country Id.</param>
        /// <returns>List of CityViewModel</returns>
        Task<List<CityViewModel>> GetCitiesInCountryByCountryId(Guid CountryId);

        /// <summary>
        ///     Create a new city from AddCityViewModel.
        /// </summary>
        /// <param name="viewModel">Expresses AddCityViewModel</param>
        /// <param name="countryid">Expresses CountryId</param>
        /// <returns>True in case create successfully or False in case create failed.</returns>
        Task<bool> CreateCityFromViewModel(AddCityViewModel viewModel, string countryid);

        /// <summary>
        ///     UpDate City Name.
        /// </summary>
        /// <param name="id">Expresses City id</param>
        /// <param name="name">Expresses new City name</param>
        /// <returns>Status Code</returns>
        Task<int> UpDateAsync(string id, string name);

        /// <summary>
        ///     Delete City from database by city id.
        /// </summary>
        /// <param name="id">Expresses City id.</param>
        /// <returns>200 in case delete success, or 404 in case City not found, or 500 in case bad request</returns>
        Task<int> DeleteAsync(string id);
    }
}
