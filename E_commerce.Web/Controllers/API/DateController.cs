using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Domain.Interfaces.IServices;
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
    public class DateController : ControllerBase
    {
        private readonly IDateServices _dateServices;

        public DateController(IDateServices dateServices)
        {
            _dateServices = dateServices;
        }

        [HttpGet("GetMonths")]
        public IActionResult GetMonths(int year)
        {
            return Ok(_dateServices.GetMonthsNamesFromYear(year));
        }

        [HttpGet("GetDays")]
        public IActionResult GetDays(int month, int year)
        {
            int Month = Convert.ToInt32(month);
            return Ok(_dateServices.GetDaysFromMonth(Month, year));
        }
    }
}
