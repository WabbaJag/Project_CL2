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

namespace CL2.Controllers
{
    public class LegislativosController : BaseController
    {
        private readonly CL2Context _context;

        public LegislativosController(CL2Context context)
        {
            _context = context;
        }

        // GET: Legislativos
        public async Task<IActionResult> Index()
        {
            var cL2Context = _context.Legislativos.Include(l => l.Administracion);
            return View(await cL2Context.ToListAsync());
        }

        // GET: Legislativos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legislativo = await _context.Legislativos
                .Include(l => l.Administracion)
                .FirstOrDefaultAsync(m => m.LegislativoID == id);
            if (legislativo == null)
            {
                return NotFound();
            }

            return View(legislativo);
        }

        // GET: Legislativos/Create
        public IActionResult Create()
        {
            ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo");
            return View();
        }

        // POST: Legislativos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("LegislativoID,AnnoLegislativo,AdministracionID")] Legislativo legislativo)
        {
            try
            {
                if (legislativo.AnnoLegislativo == null)
                {
                    return Json(new { mensaje = "vacio" });
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var listaLegislativos = (from x in _context.Legislativos select x).ToList();
                        foreach (var item in listaLegislativos)
                        {
                            if (legislativo.AnnoLegislativo.ToLower().Trim() == item.AnnoLegislativo.ToLower().Trim())
                            {
                                return Json(new { mensaje = "legisExistente" });
                            }
                        }

                        var listaAdministraciones = (from x in _context.Administraciones select x).ToList();

                        if (legislativo.UltimoAnno <= listaAdministraciones.First().PrimerAnno || legislativo.UltimoAnno > listaAdministraciones.Last().UltimoAnno)
                        {
                            return Json(new { mensaje = "noExisteAdministracion" });
                        }
                        else
                        {
                            foreach (var item in listaAdministraciones)
                            {
                                if (legislativo.UltimoAnno <= item.UltimoAnno)
                                {
                                    legislativo.AdministracionID = item.AdministracionID;
                                }
                            }

                        }

                        _context.Add(legislativo);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "La Legislación " + legislativo.AnnoLegislativo + " fue agregada correctamente.";
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

        // GET: Legislativos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legislativo = await _context.Legislativos.FindAsync(id);
            if (legislativo == null)
            {
                return NotFound();
            }
            ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo", legislativo.AdministracionID);
            return View(legislativo);

        }

        // POST: Legislativos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LegislativoID,AnnoLegislativo,AdministracionID")] Legislativo legislativo)
        {
            try
            {
                if (id != legislativo.LegislativoID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(legislativo);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LegislativoExists(legislativo.LegislativoID))
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
                ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo", legislativo.AdministracionID);
                return View(legislativo);
            }
            catch (DbUpdateException)
            {
                BasicNotification("", NotificationType.Error, "Legislación ya existe");
                ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo", legislativo.AdministracionID);

                return View(legislativo);
            }
        }

        // GET: Legislativos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legislativo = await _context.Legislativos
                .Include(l => l.Administracion)
                .FirstOrDefaultAsync(m => m.LegislativoID == id);
            if (legislativo == null)
            {
                return NotFound();
            }

            return View(legislativo);
        }

        // POST: Legislativos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var legislativo = await _context.Legislativos.FindAsync(id);
                _context.Legislativos.Remove(legislativo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Utilidades");
            }
            catch
            {
                BasicNotification("No es posible eliminar la Legislación", NotificationType.Error, "Existen datos dependientes de esta Legislación");

                return View();
            }
        }

        private bool LegislativoExists(int id)
        {
            return _context.Legislativos.Any(e => e.LegislativoID == id);
        }
    }
}
