using Microsoft.AspNetCore.Mvc;
using ElibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace ElibraryManagement.Controllers
{
    public class InstitucionesController : Controller
    {
        private readonly LibreriaContext _context;

        public InstitucionesController(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var instituciones = await _context.Instituciones.ToListAsync();
            return View(instituciones);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Institucione institucion)
        {
            if (ModelState.IsValid)
            {
                _context.Instituciones.Add(institucion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(institucion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var institucion = await _context.Instituciones.FindAsync(id);
            if (institucion == null) return NotFound();
            return View(institucion);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Institucione institucion)
        {
            if (ModelState.IsValid)
            {
                _context.Update(institucion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(institucion);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var institucion = await _context.Instituciones.FindAsync(id);
            if (institucion == null) return NotFound();
            return View(institucion);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institucion = await _context.Instituciones.FindAsync(id);
            if (institucion != null)
            {
                _context.Instituciones.Remove(institucion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }

}

