using ElibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElibraryManagement.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibreriaContext _context;

        public LibrosController(LibreriaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var libros = _context.Libros
                .Include(l => l.IdInstitucionNavigation)
                .ToList();

            return View(libros);
        }
    }
}
