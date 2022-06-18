using E_commerce.Core.Const;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Domain.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IToastNotification _toastNotification;

        public UserController(IUserServices userServices, IToastNotification toastNotification)
        {
            _userServices = userServices;
            _toastNotification = toastNotification;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if(!_userServices.AnyAsync(user => user.Id == id).Result)
                return View(@"Views/Shared/error-404.cshtml");

            return View(await _userServices.GetUserForEditByIdAsync(id));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEditViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Model state is not valid");
                return View(viewModel);
            }

            bool Result = await _userServices.UpDateUserFromViewModelAsync(viewModel);

            return View(viewModel); 
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
