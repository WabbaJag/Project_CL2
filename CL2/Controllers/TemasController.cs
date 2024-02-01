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
using Microsoft.Data.SqlClient;

namespace CL2.Controllers
{
    public class TemasController : BaseController
    {
        private readonly CL2Context _context;

        public TemasController(CL2Context context)
        {
            _context = context;
        }

        // GET: Temas
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Temas.ToListAsync());
        }

        // GET: Temas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tema = await _context.Temas
                .FirstOrDefaultAsync(m => m.TemaID == id);
            if (tema == null)
            {
                return NotFound();
            }

            return View(tema);
        }

        // GET: Temas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Temas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("TemaID,TemaDesc")] Tema tema)
        {


            if (ModelState.IsValid)
            {
                if (_context.Temas.Where(u => u.TemaDesc == tema.TemaDesc).Any())
                {
                    BasicNotification("Tema ya existe.", NotificationType.Error, "Error");
                    return View(tema);
                }
                else
                {
                    _context.Add(tema);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Utilidades");
                }

            }
            return View(tema);
        }

        // GET: Temas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tema = await _context.Temas.FindAsync(id);
            if (tema == null)
            {
                return NotFound();
            }
            return View(tema);
        }

        // POST: Temas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("TemaID,TemaDesc")] Tema tema)
        {
            try
            {
                if (id != tema.TemaID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(tema);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TemaExists(tema.TemaID))
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
                return View(tema);

            }
            catch (DbUpdateException)
            {
                BasicNotification("", NotificationType.Error, "Tema ya existe");
                return View(tema);
            }
        }

        // GET: Temas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tema = await _context.Temas
                .FirstOrDefaultAsync(m => m.TemaID == id);
            if (tema == null)
            {
                return NotFound();
            }

            return View(tema);
        }

        // POST: Temas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var tema = await _context.Temas.FindAsync(id);
                _context.Temas.Remove(tema);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Utilidades");
            }
            catch
            {
                BasicNotification("No es posible eliminar el Tema", NotificationType.Error, "Existen datos dependientes de este Tema");

                return View();
            }
        }

        private bool TemaExists(int id)
        {
            return _context.Temas.Any(e => e.TemaID == id);
        }
    }
}
