using Microsoft.AspNetCore.Mvc;

namespace ElibraryManagement.Controllers
{
    public class TermsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
