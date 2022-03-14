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
    public class ciudadClassesController : Controller
    {
        private readonly contextoBD _context;

        public ciudadClassesController(contextoBD context)
        {
            _context = context;
        }

        // GET: ciudadClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.TablaCiudad.ToListAsync());
        }

        // GET: ciudadClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudadClass = await _context.TablaCiudad
                .FirstOrDefaultAsync(m => m.IdCiudad == id);
            if (ciudadClass == null)
            {
                return NotFound();
            }

            return View(ciudadClass);
        }

        // GET: ciudadClasses/Create
        public IActionResult Create()
        {
            var listaRegiones = _context.TablaRegion.ToList();
            ViewBag.listaRegiones = listaRegiones;
            return View();
        }

        // POST: ciudadClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCiudad,IdRegion,Nombre,AreaKm,Poblacion,EsCapital")] ciudadClass ciudadClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudadClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciudadClass);
        }

        // GET: ciudadClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var listaRegiones = _context.TablaRegion.ToList();
            ViewBag.listaRegiones = listaRegiones;
            if (id == null)
            {
                return NotFound();
            }

            var ciudadClass = await _context.TablaCiudad.FindAsync(id);
            if (ciudadClass == null)
            {
                return NotFound();
            }
            return View(ciudadClass);
        }

        // POST: ciudadClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCiudad,IdRegion,Nombre,AreaKm,Poblacion,EsCapital")] ciudadClass ciudadClass)
        {
            if (id != ciudadClass.IdCiudad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudadClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ciudadClassExists(ciudadClass.IdCiudad))
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
            return View(ciudadClass);
        }

        // GET: ciudadClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudadClass = await _context.TablaCiudad
                .FirstOrDefaultAsync(m => m.IdCiudad == id);
            if (ciudadClass == null)
            {
                System.Diagnostics.Debug.WriteLine("Id No encontrado");
                return NotFound();
            }

            return View(ciudadClass);
        }

        // POST: ciudadClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var count = 0;
            var aux = id;
            var listaComunas = _context.TablaComuna.ToList();



            foreach (var item in (List<comunaClass>)listaComunas)
            {

                if (_context.TablaCiudad.Find(id).IdCiudad == item.IdCiudad)
                {
                    count += 1;

                }
                else
                {

                }
            }

            if (count == 0) { 
                var ciudadClass = await _context.TablaCiudad.FindAsync(id);
            _context.TablaCiudad.Remove(ciudadClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            else if (count > 0)
            {
                return RedirectToAction("ErrorBorrarCiudad", "ciudadClasses", new { dsf = aux });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult ErrorBorrarCiudad(int dsf)
        {
            var comunasAsociadas = _context.TablaComuna.Where(x => x.IdCiudad == dsf).ToList();
            ViewBag.comunasAsociadas = comunasAsociadas;

            return View();

        }
        private bool ciudadClassExists(int id)
        {
            return _context.TablaCiudad.Any(e => e.IdCiudad == id);
        }
    }
}
