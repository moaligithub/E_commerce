using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Domain.ViewModels.Branch;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly ICountryServices _countryServices;
        private readonly IBranchServices _branchServices;

        public BranchController(ICountryServices countryServices, IBranchServices branchServices)
        {
            _countryServices = countryServices;
            _branchServices = branchServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetBranches(Guid countryid)
        {
            // Check of Country
            bool CheckCountry = await _countryServices.AnyAsync(country
                => country.Id == countryid);

            if (!CheckCountry)
                return NotFound("This Country Not Found");

            List<BranchViewModel> branches = await _branchServices.GetBranchsInCountryByCountryIdIdAsync(countryid);

            if (branches == null)
                return NotFound("Not Existing Branches");

            return Ok(new SelectList(branches, "Id", "Title"));
        }

        [HttpPut("{id}/{newName}")]
        public async Task<IActionResult> EditAsync(string id, string newName)
        {
            int Result = await _branchServices.UpDateAsync(id, newName);

            if (Result == 404)
                return NotFound();

            if (Result == 500)
                return BadRequest();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            int Result = await _branchServices.DeleteAsync(id);

            if (Result == 404)
                return NotFound();

            if (Result == 500)
                return BadRequest();

            return Ok();
        }
    }
}
