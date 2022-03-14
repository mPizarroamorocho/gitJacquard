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
    public class regionClassesController : Controller
    {
        private readonly contextoBD _context;

        public regionClassesController(contextoBD context)
        {
            _context = context;
        }

        // GET: regionClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.TablaRegion.ToListAsync());
        }

        // GET: regionClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionClass = await _context.TablaRegion
                .FirstOrDefaultAsync(m => m.IdRegion == id);
            if (regionClass == null)
            {
                return NotFound();
            }

            return View(regionClass);
        }

        // GET: regionClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: regionClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRegion,NumeroRegion,Nombre,AreaKm,Poblacion")] regionClass regionClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regionClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regionClass);
        }

        // GET: regionClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionClass = await _context.TablaRegion.FindAsync(id);
            if (regionClass == null)
            {
                return NotFound();
            }
            return View(regionClass);
        }

        // POST: regionClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRegion,NumeroRegion,Nombre,AreaKm,Poblacion")] regionClass regionClass)
        {
            if (id != regionClass.IdRegion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regionClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!regionClassExists(regionClass.IdRegion))
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
            return View(regionClass);
        }

        // GET: regionClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           
            if (id == null)
            {
             
                return NotFound();
            }

            var regionClass = await _context.TablaRegion
                .FirstOrDefaultAsync(m => m.IdRegion == id);
            if (regionClass == null)
            {
                System.Diagnostics.Debug.WriteLine("Id No encontrado");
                return NotFound();
            }

            return View(regionClass);
        }

        // POST: regionClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var count = 0;
            var aux = id;
            var listaCiudades = _context.TablaCiudad.ToList();

            

            foreach (var item in (List<ciudadClass>)listaCiudades)
            {

                if (_context.TablaRegion.Find(id).IdRegion == item.IdRegion){
                    count += 1;
                    
                }
                else
                {

                }
            }
            
            if (count == 0)
            {
                var regionClass = await _context.TablaRegion.FindAsync(id);
                _context.TablaRegion.Remove(regionClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (count > 0)
            {
                
                return RedirectToAction("ErrorBorrarRegion", "regionClasses", new { dsf = aux});
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            

        }

        public IActionResult ErrorBorrarRegion(int dsf)
        {
            var ciudadesAsociadas = _context.TablaCiudad.Where(x => x.IdRegion == dsf).ToList();
            ViewBag.ciudadesAsociadas = ciudadesAsociadas;
            
            return View();
            
        }

        private bool regionClassExists(int id)
        {
            return _context.TablaRegion.Any(e => e.IdRegion == id);
        }

        
    }
}
