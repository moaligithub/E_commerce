using E_commerce.Domain.ViewModels.Branch;
using E_commerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchServices _branchServices;
        private readonly IToastNotification _toastNotification;

        public BranchController(IBranchServices branchServices, IToastNotification toastNotification)
        {
            _branchServices = branchServices;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(string countryid)
        {
            List<BranchViewModel> cities = await _branchServices.GetBranchsInCountryByCountryIdIdAsync(Guid.Parse(countryid));

            ViewData["CountryId"] = countryid;
            return View(cities);
        }

        public async Task<IActionResult> Add(AddBranchViewModel viewModel, string Countryid)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("the model state is not valid");
                return View();
            }

            bool Result = await _branchServices.CreateBranchFromViewModel(viewModel, Countryid);

            if (!Result)
                return View();

            return RedirectToAction(nameof(Index), new { countryid = Countryid });
        }
    }
}
