using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Domain.ViewModels;
using E_commerce.Domain.ViewModels.Country;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryServices _countryServices;
        private readonly IToastNotification _toastNotification;

        public CountryController(ICountryServices countryServices, IToastNotification toastNotification)
        {
            _countryServices = countryServices;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            List<CountryViewModel> countries = await _countryServices.GetAllCountryAsync();

            if (countries.Count == 0)
                return View(@"Views/Shared/error-404.cshtml");

            return View(countries);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCountryViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("This modelState is not vaild");
                return View();
            }

            bool Result = await _countryServices.AddFromViewModelAsync(viewModel);

            if (!Result)
                return View();

            return RedirectToAction(nameof(Index));
        }

    }
}