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
using CL2.Models.ViewModel;

namespace CL2.Controllers
{
    public class SesionPlenariosController : BaseController
    {
        private readonly CL2Context _context;

        public SesionPlenariosController(CL2Context context)
        {
            _context = context;
        }

        // GET: SesionPlenarios
        [Authorize]

        public IActionResult Index()
        {
            var sesionPlenarios = from s in _context.SesionPlenarios.Include(p => p.Plenario.Administracion) select s;
            return View(sesionPlenarios);
        }

        // GET: SesionPlenarios/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionPlenario = await _context.SesionPlenarios
                .Include(s => s.Plenario.Administracion)
                .Include(s => s.ControlPolitico)
                .FirstOrDefaultAsync(m => m.SesionPlenarioID == id);

            if(sesionPlenario == null)
            {
                return NotFound();
            }


            return View(sesionPlenario);
        }

        // GET: SesionPlenarios/Create
        [Authorize]
        public IActionResult Create()
        {
            var result = (from a in _context.Plenarios
                          join c in _context.Administraciones on a.AdministracionID equals c.AdministracionID
                          select new { a.PlenarioID, c.AdministracionPeriodo }).ToList();

            List<SelectListItem> item = result.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.AdministracionPeriodo.ToString(),
                    Value = a.PlenarioID.ToString(),
                    Selected = false
                };
            });

            ViewData["PlenarioID"] = item;
            return View();
        }



        // POST: SesionPlenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("SesionPlenarioID,PlenarioID,NumeroSesion,PeriodoSesionPlenario,TipoSesionPlenario,FechaSesionPlenario")] SesionPlenario sesionPlenario)
        {
            var result = (from a in _context.Plenarios
                          join c in _context.Administraciones on a.AdministracionID equals c.AdministracionID
                          select new { a.PlenarioID, c.AdministracionPeriodo }).ToList();

            List<SelectListItem> item = result.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.AdministracionPeriodo.ToString(),
                    Value = a.PlenarioID.ToString(),
                    Selected = false
                };
            });
            //Try catch para agarrar errores de duplicidad en NumeroSesion
            try
            {
                if (ModelState.IsValid)
                {

                    _context.Add(sesionPlenario);
                    await _context.SaveChangesAsync();
                    BasicNotification("", NotificationType.Success, "Sesión de Plenario Agregada Correctamente");
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PlenarioID"] = item;
            }
            catch
            {
                ModelState.AddModelError("", "Ya existe la sesión #"+sesionPlenario.NumeroSesion.ToString() +" para este Plenario, por favor escoger otro.");
                ViewData["PlenarioID"] = item;
                return View();
            }
            return View(sesionPlenario);
        }

        // GET: SesionPlenarios/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionPlenario = await _context.SesionPlenarios.FindAsync(id);
            if (sesionPlenario == null)
            {
                return NotFound();
            }
            //almacena la lista de Plenarios
            ViewData["PlenarioID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo"); ;
            return View(sesionPlenario);
        }

        // POST: SesionPlenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SesionPlenarioID,PlenarioID,NumeroSesion,PeriodoSesionPlenario,TipoSesionPlenario,FechaSesionPlenario")] SesionPlenario sesionPlenario)
        {
            try
            {
                if (id != sesionPlenario.SesionPlenarioID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(sesionPlenario);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SesionPlenarioExists(sesionPlenario.SesionPlenarioID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    BasicNotification("", NotificationType.Success, "Sesión de Plenario Actualizada Correctamente");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Ya existe un número de sesión para este Plenario, porfavor escoger otro.");
                ViewData["PlenarioID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo"); ;
                return View();
            }
            ViewData["PlenarioID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo");
            return View(sesionPlenario);
        }

        // GET: SesionPlenarios/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionPlenario = await _context.SesionPlenarios
                .Include(s => s.Plenario.Administracion)
                .FirstOrDefaultAsync(m => m.SesionPlenarioID == id);
            if (sesionPlenario == null)
            {
                return NotFound();
            }

            return View(sesionPlenario);
        }

        // POST: SesionPlenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var sesionPlenario = await _context.SesionPlenarios.FindAsync(id);
                _context.SesionPlenarios.Remove(sesionPlenario);
                await _context.SaveChangesAsync();
                BasicNotification("", NotificationType.Success, "Sesión de Plenario Eliminada Correctamente");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                BasicNotification("No es posible eliminar la sesión", NotificationType.Error, "Existen datos dependientes");

                return View();
            }
        }

        private bool SesionPlenarioExists(int id)
        {
            return _context.SesionPlenarios.Any(e => e.SesionPlenarioID == id);
        }
    }
}
