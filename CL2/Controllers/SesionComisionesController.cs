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
    public class SesionComisionesController : BaseController
    {
        private readonly CL2Context _context;

        public SesionComisionesController(CL2Context context)
        {
            _context = context;
        }

        // GET: SesionComisiones
        [Authorize]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cL2Context = _context.SesionComision.Include(s => s.ComisionLegislativos).Include(t => t.ComisionLegislativos.Comision).Include(v => v.ComisionLegislativos.Legislativo);
            return View(await Models.PaginatedList<SesionComision>.CreateAsync(cL2Context, pageNumber, 10));
        }

        // GET: SesionComisiones/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionComision = await _context.SesionComision
                .Include(s => s.ComisionLegislativos)
                .FirstOrDefaultAsync(m => m.SesionComisionID == id);
            if (sesionComision == null)
            {
                return NotFound();
            }

            return View(sesionComision);
        }
        //GET
        [Authorize]
        public async Task<IActionResult> SelectComision()
        {
            //Lista de comisiones existentes
            var comisiones = (from x in _context.Comisiones select x).ToList();
            List<SelectListItem> ListaComisiones = new List<SelectListItem>();
            foreach (var item in comisiones)
            {
                ListaComisiones.Add(new SelectListItem
                {
                    Value = item.ComisionID.ToString(),
                    Text = item.NombreComision
                });
            }
            ViewBag.listaComisiones = ListaComisiones;

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetLegislativosFromComision(int IDComision)
        {
            try
            {
                var comisionLegislativos = _context.ComisionLegislativo.Include(c => c.Legislativo).Where(z => z.ComisionID == IDComision).ToList();

                return Json(new SelectList(comisionLegislativos, "ComisionLegislativoID", "Legislativo.AnnoLegislativo"));

            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }

        // GET: SesionComisiones/Create
        public IActionResult Create()
        {
            ViewData["ComisionLegislativoID"] = new SelectList(_context.ComisionLegislativo, "ComisionLegislativoID", "ComisionLegislativoID");
            return View();
        }

        // POST: SesionComisiones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("SesionComisionID,ComisionLegislativoID,NumeroSesionComision,PeriodoSesionComision,FechaSesionComision,TipoSesionComision")] SesionComision sesionComision)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(sesionComision);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ComisionLegislativoID"] = new SelectList(_context.ComisionLegislativo, "ComisionLegislativoID", "ComisionLegislativoID", sesionComision.ComisionLegislativoID);
                return View(sesionComision);
            }
            catch
            {
                ModelState.AddModelError("", "Ya existe un número de sesión para este Comisión/Legislación, porfavor escoger otro.");
                ViewData["Comision"] = new SelectList(_context.ComisionLegislativo, "ComisionLegislativoID", "ComisionLegislativoID", sesionComision.ComisionLegislativoID);
                return View();
            }
        }

        // GET: SesionComisiones/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionComision = await _context.SesionComision.FindAsync(id);
            if (sesionComision == null)
            {
                return NotFound();
            }
            var listaComisiones = _context.Comisiones.ToList();
            //ViewData["ComisionLegislativoID"] = new SelectList(listaComisiones, "ComisionID", "ComisionID", sesionComision.ComisionLegislativos.ComisionID);
            ViewData["Comision"] = new SelectList(listaComisiones, "ComisionID", "NombreComision", sesionComision);
            var listaLegislativos = _context.Legislativos.ToList();
            ViewData["Legislativo"] = new SelectList(listaLegislativos, "LegislativoID", "AnnoLegislativo", sesionComision);
            return View(sesionComision);
        }

        // POST: SesionComisiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("SesionComisionID,ComisionLegislativoID,NumeroSesionComision," +
                                                        "PeriodoSesionComision,FechaSesionComision,TipoSesionComision")] SesionComision sesionComision,
                                                int comisionID, int legislativoID)
        {
            if (id != sesionComision.SesionComisionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //Busco el comisionLegislativoID que se desea eliminar
                    int comisionLegislativo = _context.ComisionLegislativo.Where(a => a.LegislativoID == legislativoID && a.ComisionID == comisionID)
                        .FirstOrDefault().ComisionLegislativoID;

                    //Con el ID, busco en SesionComision cual se desea eliminar
                    var sesionComisionEditada = _context.SesionComision.Where(s => s.ComisionLegislativoID == comisionLegislativo);

                    //Lo elimino
                    foreach(var s in sesionComisionEditada)
                    {
                        _context.SesionComision.Remove(s);
                    }

                    //agrego el entrante
                    _context.Add(sesionComision);

                    try
                    {
                        _context.SaveChanges();
                    }
                    catch
                    {
                        BasicNotification("No es posible eliminar la sesión", NotificationType.Error, "Existen datos dependientes");
                        return View();
                    }
                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesionComisionExists(sesionComision.SesionComisionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _context.Entry(sesionComision).State = EntityState.Modified;
                return RedirectToAction(nameof(Index));
            }
            var listaComisiones = _context.Comisiones.ToList();

            ViewData["Comision"] = new SelectList(listaComisiones, "ComisionID", "NombreComision", sesionComision);

            var listaLegislativos = _context.Legislativos.ToList();
            ViewData["Legislativo"] = new SelectList(listaLegislativos, "LegislativoID", "AnnoLegislativo", sesionComision);
            return View(sesionComision);
        }

        // GET: SesionComisiones/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionComision = await _context.SesionComision
                .Include(s => s.ComisionLegislativos.Comision)
                .Include(t => t.ComisionLegislativos.Legislativo)
                .FirstOrDefaultAsync(m => m.SesionComisionID == id);
            if (sesionComision == null)
            {
                return NotFound();
            }

            return View(sesionComision);
        }

        // POST: SesionComisiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var sesionComision = await _context.SesionComision.FindAsync(id);
                _context.SesionComision.Remove(sesionComision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                BasicNotification("No es posible eliminar la sesión", NotificationType.Error, "Existen datos dependientes");

                return View();
            }
        }

        private bool SesionComisionExists(int id)
        {
            return _context.SesionComision.Any(e => e.SesionComisionID == id);
        }
    }
}
