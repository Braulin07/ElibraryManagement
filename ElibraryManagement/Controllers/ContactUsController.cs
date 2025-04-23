using Microsoft.AspNetCore.Mvc;

namespace ElibraryManagement.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
