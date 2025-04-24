using ElibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElibraryManagement.Controllers
{
    public class LogInController1 : Controller
    {
        private readonly LibreriaContext _context;

        public LogInController1(LibreriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correoElectronico, string contrasena)
        {
            if (string.IsNullOrEmpty(correoElectronico) || string.IsNullOrEmpty(contrasena))
            {
                ViewBag.Error = "Por favor, completa todos los campos.";
                return View("Index");
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Administradores)
                .FirstOrDefaultAsync(u => u.CorreoElectronico == correoElectronico && u.Contrasena == contrasena);

            if (usuario == null)
            {
                ViewBag.Error = "El correo electrónico o contraseña no coinciden.";
                return View("Index");
            }

            // Guardar datos mínimos en sesión
            HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);
            HttpContext.Session.SetString("NombreUsuario", usuario.NombreCompleto);

            // Determinar rol
            bool esAdmin = usuario.Administradores.Any();

            if (esAdmin)
            {
                return RedirectToAction("Index", "Administradores");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }


        // En LogInController1.cs
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Borra toda la sesión
            return RedirectToAction("Index", "LogInController1"); // Te lleva de vuelta al login
        }


        // Vista del Login (GET)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}