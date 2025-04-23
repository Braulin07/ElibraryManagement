using ElibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElibraryManagement.Controllers
{
    public class UsuariosAdminController : Controller
    {
        private readonly LibreriaContext _context;

        public UsuariosAdminController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: UsuariosAdmin
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            var administradores = await _context.Administradores.ToListAsync();

            ViewBag.Administradores = administradores;

            var usuariosConRol = usuarios.Select(u => new
            {
                Usuario = u,
                EsAdmin = _context.Administradores.Any(a => a.IdUsuario == u.IdUsuario)
            }).ToList();

            

            return View(usuariosConRol);
        }


        // GET: UsuariosAdmin/Ascender/5
        public async Task<IActionResult> Ascender(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            // Ascender a administrador
            var administrador = new Administradore
            {
                IdUsuario = usuario.IdUsuario
            };

            _context.Administradores.Add(administrador);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: UsuariosAdmin/Inhabilitar/5
        [HttpPost, ActionName("Inhabilitar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inhabilitar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            usuario.IsActive = false;
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: UsuariosAdmin/Habilitar/5
        [HttpPost, ActionName("Habilitar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Habilitar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            usuario.IsActive = true;
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Verificamos si el usuario es un administrador
            bool esAdmin = await _context.Administradores.AnyAsync(a => a.IdUsuario == id);

            if (esAdmin)
            {
                // No se permite eliminar administradores
                TempData["Error"] = "No puedes eliminar un usuario con rol de administrador.";
                return RedirectToAction(nameof(Index));
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Usuario eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return NotFound();

            // ModelState.IsValid se puede omitir porque asignas campos manualmente
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente != null)
                {
                    usuarioExistente.NombreCompleto = usuario.NombreCompleto;
                    usuarioExistente.CorreoElectronico = usuario.CorreoElectronico;
                    usuarioExistente.Telefono = usuario.Telefono;
                    usuarioExistente.IsActive = usuario.IsActive;

                    _context.Update(usuarioExistente);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.IdUsuario == id)) return NotFound();
                else throw;
            }

            return View(usuario);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Instituciones = _context.Instituciones.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState no válido:");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state != null)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"🔺 {key}: {error.ErrorMessage}");
                        }
                    }
                }

                ViewBag.Instituciones = _context.Instituciones.ToList();
                return View(usuario);
            }

            usuario.FechaRegistro = DateTime.Now;
            usuario.IsActive = true;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            Console.WriteLine("✅ Usuario creado correctamente.");

            return RedirectToAction(nameof(Index));
        }






    }
}
