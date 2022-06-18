using E_commerce.Core.Consts;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Core.ViewModels.AdminActivity;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Ef.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpGet("GetActivity")]
        [Produces("application/json")]
        public async Task<IActionResult> GetActivity(string email, int year, int month, int day)
        {
            if (!_adminServices.EmailIsExistAsync(email).Result)
                return NotFound($"Not Found Admin by this Email {email}");

            return Ok(await _adminServices.GetAdminActivitiesAsync(email, year, month, day));
        }

        [HttpGet("GetAllStatsForAdminActivity")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllStatsForAdminActivity(int year, string AdminId)
        {
            return Ok(await _adminServices.GetAllStatsForAdminActivityInYear(year, AdminId));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            if (!_adminServices.AnyAsync(admin => admin.Id == id).Result)
                return NotFound();

            bool Result = _adminServices.DeleteById(id);

            if (Result)
                return Ok();
            else
                return NoContent();
        }

        [HttpPatch("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate(string id)
        {
            bool Result = await _adminServices.DeactivateByIdAsync(id);

            if (Result)
                return Ok();
            else
                return NoContent();
        }

        [HttpPatch("Activation/{id}")]
        public async Task<IActionResult> Activation(string id)
        {
            bool Result = await _adminServices.ActivationByIdAsync(id);

            if (Result)
                return Ok();
            else
                return NoContent();
        }
    }
}