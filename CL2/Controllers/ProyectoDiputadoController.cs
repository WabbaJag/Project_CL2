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
    public class ProyectoDiputadoController : BaseController
    {
        private readonly CL2Context _context;

        public ProyectoDiputadoController(CL2Context context)
        {
            _context = context;
        }

        // GET: ProyectoDiputado
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cL2Context = _context.ProyectoDiputados.Include(p => p.Diputado).Include(p => p.ProyectosLey);
            return View(await cL2Context.ToListAsync());
        }

        // GET: ProyectoDiputado/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var proyectoDiputado = await _context.ProyectoDiputados
                .Include(p => p.Diputado)
                .Include(p => p.ProyectosLey)
                .FirstOrDefaultAsync(m => m.ProyectoLeyID == id);
            if (proyectoDiputado == null)
            {
                return NotFound();
            }

            return View(proyectoDiputado);
        }

        // GET: ProyectoDiputado/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID");
            ViewData["ProyectoLeyID"] = new SelectList(_context.ProyectoLeys, "ProyectoLeyID", "ProyectoLeyID");
            return View();
        }

        // POST: ProyectoDiputado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ProyectoLeyID,DiputadoID")] ProyectoDiputado proyectoDiputado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyectoDiputado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID", proyectoDiputado.DiputadoID);
            ViewData["ProyectoLeyID"] = new SelectList(_context.ProyectoLeys, "ProyectoLeyID", "ProyectoLeyID", proyectoDiputado.ProyectoLeyID);
            return View(proyectoDiputado);
        }

        // GET: ProyectoDiputado/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectoDiputado = await _context.ProyectoDiputados.FindAsync(id);
            if (proyectoDiputado == null)
            {
                return NotFound();
            }
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID", proyectoDiputado.DiputadoID);
            ViewData["ProyectoLeyID"] = new SelectList(_context.ProyectoLeys, "ProyectoLeyID", "ProyectoLeyID", proyectoDiputado.ProyectoLeyID);
            return View(proyectoDiputado);
        }

        // POST: ProyectoDiputado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ProyectoLeyID,DiputadoID")] ProyectoDiputado proyectoDiputado)
        {
            if (id != proyectoDiputado.ProyectoLeyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyectoDiputado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoDiputadoExists(proyectoDiputado.ProyectoLeyID))
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
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID", proyectoDiputado.DiputadoID);
            ViewData["ProyectoLeyID"] = new SelectList(_context.ProyectoLeys, "ProyectoLeyID", "ProyectoLeyID", proyectoDiputado.ProyectoLeyID);
            return View(proyectoDiputado);
        }

        // GET: ProyectoDiputado/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectoDiputado = await _context.ProyectoDiputados
                .Include(p => p.Diputado)
                .Include(p => p.ProyectosLey)
                .FirstOrDefaultAsync(m => m.ProyectoLeyID == id);
            if (proyectoDiputado == null)
            {
                return NotFound();
            }

            return View(proyectoDiputado);
        }

        // POST: ProyectoDiputado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var proyectoDiputado = await _context.ProyectoDiputados.FindAsync(id);
                _context.ProyectoDiputados.Remove(proyectoDiputado);
                await _context.SaveChangesAsync();
                BasicNotification("", NotificationType.Success, "Proyecto de Ley Eliminado");
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException)
            {

                BasicNotification("Proyecto de Ley No Eliminado", NotificationType.Success, "Existen datos dependientes de este Proyecto");
                return RedirectToAction(nameof(Index));
            }
         
        }

        private bool ProyectoDiputadoExists(int id)
        {
            return _context.ProyectoDiputados.Any(e => e.ProyectoLeyID == id);
        }
    }
}
