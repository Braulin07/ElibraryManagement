using Microsoft.AspNetCore.Mvc;

namespace ElibraryManagement.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
