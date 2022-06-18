using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
