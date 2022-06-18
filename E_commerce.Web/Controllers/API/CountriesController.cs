using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Domain.ViewModels;
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
    public class CountriesController : ControllerBase
    {
        private readonly ICountryServices _countryServices;

        public CountriesController(ICountryServices countryServices)
        {
            _countryServices = countryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            List<CountryViewModel> countries = await _countryServices.GetAllCountryAsync();

            if (countries == null)
                return NotFound("Countries not found");

            return Ok(new SelectList(countries, "Id", "Name"));
        }

        [HttpPut("{id}/{newName}")]
        public async Task<IActionResult> EditAsync(string id, string newName)
        {
            int Result = await _countryServices.UpDateAsync(id, newName);

            if (Result != 200)
            {
                if (Result == 404)
                    return NotFound("Not found a country");

                if (Result == 500)
                    return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            int Result = await _countryServices.DeleteAsync(id);

            if(Result != 200)
            {
                if (Result == 404)
                    return NotFound();

                if (Result == 500)
                    return BadRequest();
            }

            return Ok();
        }
    }
}