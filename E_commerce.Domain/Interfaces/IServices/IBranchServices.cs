using E_commerce.Core.Models;
using E_commerce.Domain.ViewModels.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Interfaces
{
    public interface IBranchServices
    {
        /// <summary>
        ///     Check of Branch Exist. 
        /// </summary>
        /// <param name="expression">Expresses the properties that be find by it.</param>
        /// <returns>True in Case Branch Exist, or False in Case Branch not Exist</returns>
        Task<bool> AnyAsync(Expression<Func<Branch, bool>> expression);
        
        /// <summary>
        ///     Get Branchs In City By Country Id. 
        /// </summary>
        /// <param name="CityId">Expresses Country Id</param>
        /// <returns>List of BranchViewModel.</returns>
        Task<List<BranchViewModel>> GetBranchsInCountryByCountryIdIdAsync(Guid CountryId);

        /// <summary>
        ///     Create a new branch from AddBranchViewModel
        /// </summary>
        /// <param name="viewModel">Expresses AddBranchViewModel</param>
        /// <param name="countryid">Expresses CountryId</param>
        /// <returns>True in case create successfully or False in case create failed.</returns>
        Task<bool> CreateBranchFromViewModel(AddBranchViewModel viewModel, string countryid);

        /// <summary>
        ///     UpDate branch Name.
        /// </summary>
        /// <param name="id">Expresses branch id</param>
        /// <param name="name">Expresses new branch name</param>
        /// <returns>Status Code</returns>
        Task<int> UpDateAsync(string id, string name);

        /// <summary>
        ///     Delete branch from database by branch id.
        /// </summary>
        /// <param name="id">Expresses branch id.</param>
        /// <returns>200 in case delete success, or 404 in case branch not found, or 500 in case bad request</returns>
        Task<int> DeleteAsync(string id);
    }
}