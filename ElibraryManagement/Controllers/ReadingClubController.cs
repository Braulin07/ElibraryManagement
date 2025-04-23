using Microsoft.AspNetCore.Mvc;

namespace ElibraryManagement.Controllers
{
    public class ReadingClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
