using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using E_commerce.Core.Const;
using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NToastNotify;
using static System.Collections.Specialized.BitVector32;

namespace E_commerce.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IUnitOfWork _repositories;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserServices _userServices;
        private readonly IToastNotification _toastNotification;

        public RegisterModel(
            IUnitOfWork repositories,
            UserManager<ApplicationUser> UserManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IUserServices userServices,
            IToastNotification toastNotification)

        {
            _repositories = repositories;
            _userManager = UserManager;
            _signInManager = signInManager;
            _logger = logger;
            _userServices = userServices;
            _toastNotification = toastNotification;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email*")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password*")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password*")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "FirstName*")]
            [DataType(DataType.Text)]
            [StringLength(100)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "LastName*")]
            [DataType(DataType.Text)]
            [StringLength(100)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Country*")]
            [DataType(DataType.Text)]
            [StringLength(100)]
            public string CountryId { get; set; }

            [Required]
            [Display(Name = "City*")]
            [DataType(DataType.Text)]
            [StringLength(100)]
            public string CityId { get; set; }

            [Display(Name = "PhoneNumber")]
            [Phone]
            public string PhoneNumber { get; set; }
            public ApplicationUser user { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {   
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                        
            if (await _userServices.EmailIsExistAsync(Input.Email))
            {
                _toastNotification.AddErrorToastMessage("Thie Email already Exsits");
                return Page();
            }
            
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = new MailAddress(Input.Email).User,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    IsActive = true,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                    City = _repositories.City.GetById(Guid.Parse(Input.CityId))
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Add This User To Role
                    await _userManager.AddToRoleAsync(user, Roles.User);

                    // Add User Properties
                    UserProperties userProperties = new UserProperties
                    {
                        Id = Guid.Parse(user.Id),
                        UserId = user.Id, 
                    };

                    await _repositories.UserProperties.AddAsync(userProperties);
                    _repositories.Complete();

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _services.EmailSenderServices.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
