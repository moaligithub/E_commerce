using AutoMapper;
using E_commerce.Core.Const;
using E_commerce.Core.ViewModels.Admin;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Domain.ViewModels.AdminManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers
{
    [Authorize(Roles = Roles.AdminManager)]
    public class AdminManagerController : Controller
    {
        private readonly IAdminServices _adminServices;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public AdminManagerController(IAdminServices adminServices, IToastNotification toastNotification,
            IMapper mapper)
        {
            _adminServices = adminServices;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            AdminEditViewModel Result = await _adminServices.GetAdminForEditByIdAsync(id);

            if(Result.UserName == null)
                return View(@"Views/Shared/error-404.cshtml");

            return View(_mapper.Map<EditAdminManagerViewModel>(Result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAdminManagerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Model State is not valid");
                return View(viewModel);
            }

            bool Result = await _adminServices.UpDateAdminFromViewModelAsync(_mapper.Map<AdminEditViewModel>(viewModel));

            if (!Result)
                return View();

            _toastNotification.AddSuccessToastMessage("UpDated Successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
