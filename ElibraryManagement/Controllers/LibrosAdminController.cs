using ElibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElibraryManagement.Controllers
{
    public class LibrosAdminController : Controller
    {
        private readonly LibreriaContext _context;
        private readonly IWebHostEnvironment _env;

        public LibrosAdminController(LibreriaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: LibrosAdmin
        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros.Include(l => l.IdInstitucionNavigation).ToListAsync();
            return View(libros);
        }



        // GET: LibrosAdmin/Create
        public IActionResult Create()
        {
            ViewBag.Instituciones = _context.Instituciones.ToList();
            return View();
        }

        // POST: LibrosAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro, IFormFile? PortadaFile, IFormFile? ArchivoPdf)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }

                ViewBag.Instituciones = _context.Instituciones.ToList();
                return View(libro);
            }

            // Guardar portada
            if (PortadaFile != null && PortadaFile.Length > 0)
            {
                var rutaCarpeta = Path.Combine(_env.WebRootPath, "portadas");
                Directory.CreateDirectory(rutaCarpeta);

                var nombreArchivo = Guid.NewGuid() + Path.GetExtension(PortadaFile.FileName);
                var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await PortadaFile.CopyToAsync(stream);
                }

                libro.Portada = nombreArchivo;
            }

            // Guardar PDF
            if (ArchivoPdf != null && ArchivoPdf.Length > 0)
            {
                var carpetaPdf = Path.Combine(_env.WebRootPath, "pdf");
                Directory.CreateDirectory(carpetaPdf);

                var nombrePdf = Guid.NewGuid() + Path.GetExtension(ArchivoPdf.FileName);
                var rutaPdf = Path.Combine(carpetaPdf, nombrePdf);

                using (var stream = new FileStream(rutaPdf, FileMode.Create))
                {
                    await ArchivoPdf.CopyToAsync(stream);
                }

                // Guardamos la ruta relativa como URL
                libro.UrlArchivo = "/pdf/" + nombrePdf;
            }

            libro.FechaPublicacion ??= DateTime.Now;

            _context.Libros.Add(libro);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un error al guardar el libro: " + ex.Message);
                return View(libro);
            }

            return RedirectToAction("Index");
        }






        // GET: LibrosAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();

            ViewBag.Instituciones = _context.Instituciones.ToList();
            return View(libro);
        }

        // POST: LibrosAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro, IFormFile portada)
        {
            if (id != libro.IdLibro) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (portada != null && portada.Length > 0)
                    {
                        var rutaCarpeta = Path.Combine(_env.WebRootPath, "portadas");
                        Directory.CreateDirectory(rutaCarpeta);

                        var nombreArchivo = Guid.NewGuid() + Path.GetExtension(portada.FileName);
                        var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

                        using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                        {
                            await portada.CopyToAsync(stream);
                        }

                        libro.Portada = nombreArchivo;
                    }

                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Libros.Any(e => e.IdLibro == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Instituciones = _context.Instituciones.ToList();
            return View(libro);
        }

        // GET: LibrosAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros
                .Include(l => l.IdInstitucionNavigation)
                .FirstOrDefaultAsync(m => m.IdLibro == id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        // POST: LibrosAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

