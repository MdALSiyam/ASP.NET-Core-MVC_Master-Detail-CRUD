using Microsoft.AspNetCore.Mvc;

namespace CoreMvcMasterDetailCrud.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}
