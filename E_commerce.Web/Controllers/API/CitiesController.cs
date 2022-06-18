using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Domain.ViewModels.City;
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
    public class CitiesController : ControllerBase
    {
        private readonly ICityServices _cityServices;
        private readonly ICountryServices _countryServices;

        public CitiesController(ICityServices cityServices, ICountryServices countryServices)
        {
            _cityServices = cityServices;
            _countryServices = countryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities(Guid CountryId)
         {
            if (!_countryServices.AnyAsync(country => country.Id == CountryId).Result)
                return NotFound("This Country not found");

            List<CityViewModel> Cities = await _cityServices.GetCitiesInCountryByCountryId(CountryId);
            
            if (Cities == null)
                return NotFound("Cities not found");

            return Ok(new SelectList(Cities, "Id", "Name"));
        }

        [HttpPut("{id}/{newName}")]
        public async Task<IActionResult> EditAsync(string id, string newName)
        {
            int Result = await _cityServices.UpDateAsync(id, newName);

            if (Result == 404)
                return NotFound();

            if (Result == 500)
                return BadRequest();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            int Result = await _cityServices.DeleteAsync(id);

            if (Result == 404)
                return NotFound();

            if (Result == 500)
                return BadRequest();

            return Ok();
        }
    }
}