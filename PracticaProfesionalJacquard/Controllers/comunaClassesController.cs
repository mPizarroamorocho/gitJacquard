using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaProfesionalJacquard.Datos;
using PracticaProfesionalJacquard.Models;

namespace PracticaProfesionalJacquard.Controllers
{
    public class comunaClassesController : Controller
    {
        private readonly contextoBD _context;

        public comunaClassesController(contextoBD context)
        {
            _context = context;
        }

        // GET: comunaClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.TablaComuna.ToListAsync());
        }

        // GET: comunaClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunaClass = await _context.TablaComuna
                .FirstOrDefaultAsync(m => m.IdComuna == id);
            if (comunaClass == null)
            {
                return NotFound();
            }

            return View(comunaClass);
        }

        // GET: comunaClasses/Create
        public IActionResult Create()
        {
            var listaCiudades = _context.TablaCiudad.ToList();
            ViewBag.listaCiudades = listaCiudades;
            return View();
        }

        // POST: comunaClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComuna,IdCiudad,Nombre,AreaKm,Poblacion")] comunaClass comunaClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comunaClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comunaClass);
        }

        // GET: comunaClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var listaCiudades = _context.TablaCiudad.ToList();
            ViewBag.listaCiudades = listaCiudades;

            if (id == null)
            {
                return NotFound();
            }

            var comunaClass = await _context.TablaComuna.FindAsync(id);
            if (comunaClass == null)
            {
                return NotFound();
            }
            return View(comunaClass);
        }

        // POST: comunaClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComuna,IdCiudad,Nombre,AreaKm,Poblacion")] comunaClass comunaClass)
        {
            if (id != comunaClass.IdComuna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comunaClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!comunaClassExists(comunaClass.IdComuna))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comunaClass);
        }

        // GET: comunaClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunaClass = await _context.TablaComuna
                .FirstOrDefaultAsync(m => m.IdComuna == id);
            if (comunaClass == null)
            {
                return NotFound();
            }

            return View(comunaClass);
        }

        // POST: comunaClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comunaClass = await _context.TablaComuna.FindAsync(id);
            _context.TablaComuna.Remove(comunaClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool comunaClassExists(int id)
        {
            return _context.TablaComuna.Any(e => e.IdComuna == id);
        }
    }
}
