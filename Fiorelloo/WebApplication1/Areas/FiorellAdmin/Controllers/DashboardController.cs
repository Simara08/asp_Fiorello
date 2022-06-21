using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.FiorelloAdmin.Controllers
{
    [Area("FiorelloAdmin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
