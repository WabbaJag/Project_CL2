using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CL2.Data;
using CL2.Models;
using CL2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using CL2.Extensions;

namespace CL2.Controllers
{
    public class AdministracionesController : BaseController
    {
        private readonly CL2Context _context;

        public AdministracionesController(CL2Context context)
        {
            _context = context;
        }

        // GET: Administraciones
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administraciones.ToListAsync());
        }

        // GET: Administraciones/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administracion = await _context.Administraciones
                .FirstOrDefaultAsync(m => m.AdministracionID == id);
            if (administracion == null)
            {
                return NotFound();
            }

            return View(administracion);
        }

        // GET: Administraciones/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administraciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> Create([Bind("AdministracionID,AdministracionPeriodo,PresidenteRepublica")] Administracion administracion)
        {
            try
            {
                if (administracion.AdministracionPeriodo == null || administracion.PresidenteRepublica == null)
                {
                    return Json(new { mensaje = "vacio" });
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var listaAdmnistraciones = (from x in _context.Administraciones select x).ToList();
                        foreach (var item in listaAdmnistraciones)
                        {
                            if (administracion.AdministracionPeriodo.ToLower().Trim() == item.AdministracionPeriodo.ToLower().Trim())
                            {
                                return Json(new { mensaje = "adminExistente" });
                            }
                        }
                        _context.Add(administracion);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "La Administración " + administracion.AdministracionPeriodo + " fue agregada correctamente.";
                        return Json(new { mensaje = "agregado" });

                    }
                    else
                    {
                        return Json(new { mensaje = "error" });
                    }
                }

            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }

        }

        // GET: Administraciones/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administracion = await _context.Administraciones.FindAsync(id);
            if (administracion == null)
            {
                return NotFound();
            }
            return View(administracion);
        }

        // POST: Administraciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("AdministracionID,AdministracionPeriodo,PresidenteRepublica")] Administracion administracion)
        {
            try
            {
                if (id != administracion.AdministracionID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(administracion);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdministracionExists(administracion.AdministracionID))
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
                return View(administracion);
            }
            catch (DbUpdateException)
            {
                BasicNotification("", NotificationType.Error, "Administración ya existe");

                return View(administracion);
            }
        }

        // GET: Administraciones/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administracion = await _context.Administraciones
                .FirstOrDefaultAsync(m => m.AdministracionID == id);
            if (administracion == null)
            {
                return NotFound();
            }

            return View(administracion);
        }

        // POST: Administraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var administracion = await _context.Administraciones.FindAsync(id);
                _context.Administraciones.Remove(administracion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Utilidades");
            }
            catch
            {
                BasicNotification("No es posible eliminar la Administración", NotificationType.Error, "Existen datos dependientes de esta Administración");

                return View();
            }
        }

        private bool AdministracionExists(int id)
        {
            return _context.Administraciones.Any(e => e.AdministracionID == id);
        }
    }
}
