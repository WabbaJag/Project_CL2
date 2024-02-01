using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CL2.Data;
using CL2.Models;
using CL2.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CL2.Controllers
{
    public class ControlPoliticoController : BaseController
    {
        private readonly CL2Context _context;

        public ControlPoliticoController(CL2Context context)
        {
            _context = context;
        }

        // GET: ControlPolitico
        public async Task<IActionResult> Index()
        {
            var cL2Context = _context.ControlPoliticos.Include(c => c.Diputados).Include(c => c.SesionPlenario).Include(c => c.Temas);
            return View(cL2Context);
        }

        public async Task<IActionResult> IndexSpecific(int? id)
        {
            SesionPlenario sesion = _context.SesionPlenarios.Where(c => c.SesionPlenarioID == id).FirstOrDefault();
            var cL2Context = _context.ControlPoliticos.Where(c=> c.SesionPlenarioID == id).Include(c => c.Diputados).Include(c => c.SesionPlenario).Include(c => c.Temas);
            ViewData["FechaSesion"] = sesion.FechaSesionPlenario;
            ViewData["idSesion"] = sesion.SesionPlenarioID.ToString();
            ViewData["NumSesion"] = sesion.NumeroSesion;
            return View(cL2Context);
        }

        // GET: ControlPolitico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlPolitico = await _context.ControlPoliticos
                .Include(c => c.Diputados)
                .Include(c => c.SesionPlenario)
                .Include(c => c.Temas)
                .Include(c => c.SesionPlenario.Plenario.Administracion)
                .FirstOrDefaultAsync(m => m.ControlPoliticoID == id);
            if (controlPolitico == null)
            {
                return NotFound();
            }
            return View(controlPolitico);
        }

        // GET: ControlPolitico/Create
        public IActionResult Create(int? id)
        {
            ViewData["Sesion"] = id;
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "nombreCompleto");
            ViewData["SesionPlenarioID"] = new SelectList(_context.SesionPlenarios, "SesionPlenarioID", "SesionPlenarioID");
            ViewData["TemaID"] = new SelectList(_context.Temas, "TemaID", "TemaDesc");
            
            return View();
        }

        // POST: ControlPolitico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ControlPoliticoID,SesionPlenarioID,DiputadoID,TemaID,ResumenComentario,DetalleComentario,PosicionControl")] ControlPolitico controlPolitico, int? idSesion)
        {
            var id = controlPolitico.SesionPlenarioID;
            if (ModelState.IsValid)
            {
                _context.Add(controlPolitico);
                await _context.SaveChangesAsync();
                BasicNotification("", NotificationType.Success, "Control Político agregado.");
                return RedirectToAction("IndexSpecific", new { id = controlPolitico.SesionPlenarioID });
            }
            
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "nombreCompleto", controlPolitico.DiputadoID);
            ViewData["SesionPlenarioID"] = new SelectList(_context.SesionPlenarios, "SesionPlenarioID", "PeriodoSesionPlenario", controlPolitico.SesionPlenarioID);
            ViewData["TemaID"] = new SelectList(_context.Temas, "TemaID", "TemaDesc", controlPolitico.TemaID);
            
            return View(controlPolitico);
        }

        // GET: ControlPolitico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlPolitico = await _context.ControlPoliticos.FindAsync(id);
            
            if (controlPolitico == null)
            {
                return NotFound();
            }


            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "nombreCompleto", controlPolitico.DiputadoID);
            ViewData["SesionPlenarioID"] = new SelectList(_context.SesionPlenarios, "SesionPlenarioID", "PeriodoSesionPlenario", controlPolitico.SesionPlenarioID);
            ViewData["TemaID"] = new SelectList(_context.Temas, "TemaID", "TemaDesc", controlPolitico.TemaID);
            
            return View(controlPolitico);
        }

        // POST: ControlPolitico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ControlPoliticoID,SesionPlenarioID,DiputadoID,TemaID,ResumenComentario,DetalleComentario,PosicionControl")] ControlPolitico controlPolitico)
        {
            if (id != controlPolitico.ControlPoliticoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlPolitico);
                    BasicNotification("", NotificationType.Success, "Control Político actualizado.");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlPoliticoExists(controlPolitico.ControlPoliticoID))
                    {
                        BasicNotification("", NotificationType.Error, "No fue posible actualizar la información");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexSpecific", new { id = controlPolitico.SesionPlenarioID });
            }
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "Cedula", controlPolitico.DiputadoID);
            ViewData["SesionPlenarioID"] = new SelectList(_context.SesionPlenarios, "SesionPlenarioID", "PeriodoSesionPlenario", controlPolitico.SesionPlenarioID);
            ViewData["TemaID"] = new SelectList(_context.Temas, "TemaID", "TemaDesc", controlPolitico.TemaID);
            return View(controlPolitico);
        }

        // GET: ControlPolitico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlPolitico = await _context.ControlPoliticos
                .Include(c => c.Diputados)
                .Include(c => c.SesionPlenario)
                .Include(c => c.Temas)
                .FirstOrDefaultAsync(m => m.ControlPoliticoID == id);
            if (controlPolitico == null)
            {
                return NotFound();
            }

            return View(controlPolitico);
        }

        // POST: ControlPolitico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlPolitico = await _context.ControlPoliticos.FindAsync(id);
            _context.ControlPoliticos.Remove(controlPolitico);
            BasicNotification("", NotificationType.Success, "Control Político eliminado.");
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexSpecific", new { id = controlPolitico.SesionPlenarioID });
        }

        private bool ControlPoliticoExists(int id)
        {
            return _context.ControlPoliticos.Any(e => e.ControlPoliticoID == id);
        }
    }
}
