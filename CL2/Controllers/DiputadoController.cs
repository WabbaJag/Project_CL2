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
using objComisionesDiputado = CL2.Entities.ComisionesDiputado;
using CL2.Extensions;

namespace CL2.Controllers
{
    public class DiputadoController : BaseController
    {
        private readonly CL2Context _context;

        public DiputadoController(CL2Context context)
        {
            _context = context;
        }

        // GET: Diputado
        [Authorize]

        public async Task<IActionResult> Index()
        {
                 var diputado = from s in _context.Diputados
                           select s;

            return View(diputado);
        }

        // GET: Diputado/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var diputado = await _context.Diputados
                    .FirstOrDefaultAsync(m => m.DiputadoID == id);
                if (diputado == null)
                {
                    return NotFound();
                }

                List<ProyectoLey> listaProyectos = new List<ProyectoLey>();
                //consultar la lista de proyectos de ley en los que el diputado esté registrado
                var proyectos = (from x in _context.ProyectoDiputados where x.DiputadoID == id select x).ToList();
                if (proyectos.Count > 0)
                {
                    foreach (var item in proyectos)
                    {
                        var proyecto = (from x in _context.ProyectoLeys where x.ProyectoLeyID == item.ProyectoLeyID select x).FirstOrDefault();
                        listaProyectos.Add(proyecto);
                    }
                }
                ViewBag.listaProyectos = listaProyectos;

                List<objComisionesDiputado> listaObjComision = new List<objComisionesDiputado>();
                //consultar la lista de comisiones en las que el diputado esté registrado
                var comisiones = (from x in _context.ComisionDiputados where x.DiputadoID == id select x).ToList();
                if (comisiones.Count > 0)
                {
                    foreach (var item in comisiones)
                    {
                        var comisionLegislativo = (from x in _context.ComisionLegislativo
                                                   where x.ComisionLegislativoID == item.ComisionLegislativoID
                                                   select x).FirstOrDefault();
                        var comision = (from x in _context.Comisiones
                                        where x.ComisionID == comisionLegislativo.ComisionID
                                        select x).FirstOrDefault();
                        var tema = (from x in _context.Temas
                                    where x.TemaID == comision.TemaID
                                    select x).FirstOrDefault();
                        var legislativo = (from x in _context.Legislativos
                                           where x.LegislativoID == comisionLegislativo.LegislativoID
                                           select x).FirstOrDefault();

                        objComisionesDiputado obj = new objComisionesDiputado();
                        obj.NombreComision = comision.NombreComision;
                        obj.Tema = tema.TemaDesc;
                        obj.AnoLegislativo = legislativo.AnnoLegislativo;
                        listaObjComision.Add(obj);
                    }
                }
                ViewBag.listaComisiones = listaObjComision;

                return View(diputado);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // GET: Diputado/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diputado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiputadoID,NombreDiputado,PrimerApellido,SegundoApellido,Provincia,CorreoDiputado,TelefonoDiputado,TelefonoDiputado2,GeneroDiputado")] Diputado diputado)
        {
            try
            {
                int idDiputado = 0;
                if (ModelState.IsValid)
                {
                    var listaDiputados = (from x in _context.Diputados select x).ToList();
                    if (listaDiputados != null)
                    {
                        idDiputado = listaDiputados.Last().DiputadoID;
                    }
                    _context.Add(diputado);
                    await _context.SaveChangesAsync();

                    BasicNotification("", NotificationType.Success, "Diputado(a) Agregado Correctamente");
                    return RedirectToAction(nameof(Index));

                }
                BasicNotification("", NotificationType.Error, "No fue posible agregar el Diputado(a)");
                return View();
            }
            catch (Exception)
            {
                BasicNotification("", NotificationType.Error, "No fue posible agregar el Diputado(a)");
                return View();
            }
            
        }

        // GET: Diputado/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diputado = await _context.Diputados.FindAsync(id);
            if (diputado == null)
            {
                return NotFound();
            }
            return View(diputado);
        }

        // POST: Diputado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("DiputadoID,NombreDiputado,PrimerApellido,SegundoApellido,Provincia,CorreoDiputado,TelefonoDiputado,TelefonoDiputado2,GeneroDiputado")] Diputado diputado)
        {
            if (id != diputado.DiputadoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diputado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiputadoExists(diputado.DiputadoID))
                    {
                        BasicNotification("", NotificationType.Error, "No fue posible actualizar la información");
                        return View();
                    }
                    else
                    {
                        throw;
                    }
                }
                BasicNotification("", NotificationType.Success, "Diputado(a) Actualizado Correctamente");
                return RedirectToAction(nameof(Index));
            }
           
            return View(diputado);
        }

        // GET: Diputado/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diputado = await _context.Diputados
                .FirstOrDefaultAsync(m => m.DiputadoID == id);
            if (diputado == null)
            {
                return NotFound();
            }

            return View(diputado);
        }

        // POST: Diputado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var diputado = await _context.Diputados.FindAsync(id);
                _context.Diputados.Remove(diputado);
                await _context.SaveChangesAsync();
                //validacion de eliminar
                BasicNotification("", NotificationType.Success, "Diputado(a) Eliminado Correctamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                BasicNotification("", NotificationType.Error, "No fue posible eliminar el registro del Diputado(a)");
                return RedirectToAction(nameof(Index));
            }
            
        }

        private bool DiputadoExists(int id)
        {
            return _context.Diputados.Any(e => e.DiputadoID == id);
        }
    }
}
