using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Domain.ViewModels.City;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityServices _cityServices;
        private readonly IToastNotification _toastNotification;

        public CityController(ICityServices cityServices, IToastNotification toastNotification)
        {
            _cityServices = cityServices;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(string countryid)
        {
            List<CityViewModel> cities = await _cityServices.GetCitiesInCountryByCountryId(Guid.Parse(countryid));

            if(cities.Count() == 0)
                return View(@"Views/Shared/error-404.cshtml");

            ViewData["CountryId"] = countryid;
            return View(cities);
        }

        public async Task<IActionResult> Add(AddCityViewModel viewModel, string Countryid)
        {
            if(!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("the model state is not valid");
                return View();
            }

            bool Result = await _cityServices.CreateCityFromViewModel(viewModel, Countryid);

            if (!Result)
                return View();

            return RedirectToAction(nameof(Index), new { countryid = Countryid });
        }
    }
}