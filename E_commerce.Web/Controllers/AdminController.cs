using E_commerce.Core;
using E_commerce.Core.Const;
using E_commerce.Core.Consts;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Core.ViewModels.Admin;
using E_commerce.Core.ViewModels.AdminActivity;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Ef.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers
{
    [Authorize(Roles = Roles.AdminManager)]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminServices _adminServices;
        private readonly IToastNotification _toastNotification;

        public AdminController(UserManager<ApplicationUser> userManager, IAdminServices adminServices,
            IToastNotification toastNotification)
        {
            _userManager = userManager;
            _adminServices = adminServices;
            _toastNotification = toastNotification;
        }

        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            return View(await _adminServices.GetAllAdminAsync());
        }

        // GET: AdminController/Details/test@gmail.com
        public async Task<ActionResult> Details(string email)
        {
            // Get Admin Details
            AdminDetailsViewModel adminDeatils = await _adminServices.GetAdminDetailsByEmailAsync(email);

            if (adminDeatils == null)
                return View(@"Views/Shared/error-404.cshtml");
           
            return View(adminDeatils);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAdminViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("This Form Is Not Valid!");
                return View();
            }

            bool Result = await _adminServices.CreateAdminFromViewModelAsync(viewModel);
               
            if (!Result)
                return View();

            _toastNotification.AddSuccessToastMessage("Successfully Add " + viewModel.FirstName);
            return RedirectToAction(nameof(Index));
        }

        // GET: AdminController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            AdminEditViewModel admin = await _adminServices.GetAdminForEditByIdAsync(id);

            if (admin == null)
                return View(@"Views/Shared/error-404.cshtml");

            return View(admin);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AdminEditViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("This Form Is Not Valid!");
                return View(viewModel);
            }

            bool result = await _adminServices.UpDateAdminFromViewModelAsync(viewModel);

            if (!result)
                return View(viewModel);

            _toastNotification.AddSuccessToastMessage("Success Save Changes");
            return RedirectToAction(nameof(Index));
        }
    }
}