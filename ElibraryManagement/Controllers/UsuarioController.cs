using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElibraryManagement.Models;

namespace ElibraryManagement.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly LibreriaContext _context;

        public UsuarioController(LibreriaContext context)
        {
            _context = context;
        }

        // Muestra los libros directamente
        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                // Por si alguien accede sin estar logueado
                return RedirectToAction("Index", "LogInController1");
            }

            // Obtener la institución del usuario
            var usuario = _context.Usuarios
                .Include(u => u.IdInstitucionNavigation)
                .FirstOrDefault(u => u.IdUsuario == usuarioId);

            if (usuario == null)
            {
                return NotFound();
            }

            var libros = _context.Libros
                .Include(l => l.IdInstitucionNavigation)
                .Where(l => l.IdInstitucion == usuario.IdInstitucion)
                .ToList();

            return View(libros);
        }


        // Muestra el libro PDF embebido
        public IActionResult Leer(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "LogInController1");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == usuarioId);
            if (usuario == null) return NotFound();

            var libro = _context.Libros
                .Include(l => l.IdInstitucionNavigation)
                .FirstOrDefault(l => l.IdLibro == id && l.IdInstitucion == usuario.IdInstitucion);

            if (libro == null || string.IsNullOrEmpty(libro.UrlArchivo))
                return NotFound();

            return View(libro);
        }

    }
}

