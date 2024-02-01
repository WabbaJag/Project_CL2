using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CL2.Data;
using CL2.Models;
using Microsoft.AspNetCore.Authorization;
using CL2.Extensions;

namespace CL2.Controllers
{
    public class FraccionsController : BaseController
    {
        private readonly CL2Context _context;

        public FraccionsController(CL2Context context)
        {
            _context = context;
        }

        // GET: Fraccions
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fracciones.ToListAsync());
        }

        // GET: Fraccions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fraccion = await _context.Fracciones
                .FirstOrDefaultAsync(m => m.FraccionID == id);
            if (fraccion == null)
            {
                return NotFound();
            }

            return View(fraccion);
        }

        // GET: Fraccions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fraccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("FraccionID,NombreFraccion,SiglasFraccion")] Fraccion fraccion)
        {
            if (ModelState.IsValid)
            {
                if (_context.Fracciones.Where(u => u.NombreFraccion == fraccion.NombreFraccion).Any())
                {
                    BasicNotification("Fracción ya existe.", NotificationType.Error, "Error");
                    return View(fraccion);
                }
                else
                {
                    _context.Add(fraccion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Utilidades");
                }
            }
            return View(fraccion);
        }

        // GET: Fraccions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fraccion = await _context.Fracciones.FindAsync(id);
            if (fraccion == null)
            {
                return NotFound();
            }
            return View(fraccion);
        }

        // POST: Fraccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("FraccionID,NombreFraccion,SiglasFraccion")] Fraccion fraccion)
        {
            try
            {
                if (id != fraccion.FraccionID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(fraccion);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FraccionExists(fraccion.FraccionID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index", "Utilidades");
                }
                return View(fraccion);
            }
            catch (DbUpdateException)
            {
                BasicNotification("", NotificationType.Error, "Fracción ya existe");
                return View(fraccion);
            }
        }

        // GET: Fraccions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fraccion = await _context.Fracciones
                .FirstOrDefaultAsync(m => m.FraccionID == id);
            if (fraccion == null)
            {
                return NotFound();
            }

            return View(fraccion);
        }

        // POST: Fraccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var fraccion = await _context.Fracciones.FindAsync(id);
                _context.Fracciones.Remove(fraccion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Utilidades");
            }
            catch
            {
                BasicNotification("No es posible eliminar la Fracción", NotificationType.Error, "Existen datos dependientes de esta Fracción");

                return View();
            }
        }

        private bool FraccionExists(int id)
        {
            return _context.Fracciones.Any(e => e.FraccionID == id);
        }
    }
}
