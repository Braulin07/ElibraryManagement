using ElibraryManagement.Models;
using ElibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElibraryManagement.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly LibreriaContext _context;

        public AdministradoresController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: /Administradores
        public async Task<IActionResult> Index()
        {
            // Total de usuarios registrados
            var totalUsuarios = await _context.Usuarios.CountAsync();

            // Total de libros disponibles
            var totalLibros = await _context.Libros.CountAsync();

            // Total de instituciones
            var totalInstituciones = await _context.Instituciones.CountAsync();

            // Libros agregados recientemente (último año)
            DateTime fechaLimite = DateTime.Today.AddDays(-30);

            var libros = await _context.Libros
                .Where(l => l.FechaPublicacion != null && l.FechaPublicacion > fechaLimite)
                .ToListAsync();


            // Armamos el ViewModel
            var viewModel = new AdminDashboardViewModel
            {
                TotalUsuarios = totalUsuarios,
                TotalLibros = totalLibros,
                TotalInstituciones = totalInstituciones,
                LibrosRecientes = libros
            };

            return View(viewModel);
        }
    }
}

