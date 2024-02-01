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
    public class ProyectoLeyController : BaseController
    {
        private readonly CL2Context _context;

        public ProyectoLeyController(CL2Context context)
        {
            _context = context;
        }

        // GET: ProyectoLey
        [Authorize]
        public IActionResult Index()
        {
            try
            {
                var proyecto = from s in _context.ProyectoLeys
                               select s;

                return View(proyecto);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // GET: ProyectoLey/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var proyectoLey = await _context.ProyectoLeys
                .FirstOrDefaultAsync(m => m.ProyectoLeyID == id);
                if (proyectoLey == null)
                {
                    return NotFound();
                }

                //consulta los temas asociados a ese proyecto de ley
                var temas = (from x in _context.ProyectoTemas where x.ProyectoLeyID == id select x).ToList();
                if (temas.Count > 0)
                {

                    List<Tema> listaTemas = new List<Tema>();
                    foreach (var item in temas)
                    {
                        var tema = (from x in _context.Temas
                                    where x.TemaID == item.TemaID
                                    select x).FirstOrDefault();
                        listaTemas.Add(tema);
                    }

                    List<SelectListItem> comboTemas = new List<SelectListItem>();
                    foreach (var item in listaTemas)
                    {
                        comboTemas.Add(new SelectListItem
                        {
                            Value = item.TemaID.ToString(),
                            Text = item.TemaDesc
                        });
                    }
                    ViewBag.listaTemas = comboTemas;
                }

                //consulta los diputados asociados a ese proyecto de ley
                var diputados = (from x in _context.ProyectoDiputados where x.ProyectoLeyID == id select x).ToList();
                if (diputados.Count > 0)
                {
                    List<Diputado> listaDiputados = new List<Diputado>();
                    foreach (var item in diputados)
                    {
                        var diputado = (from x in _context.Diputados
                                        where x.DiputadoID == item.DiputadoID
                                        select x).FirstOrDefault();
                        listaDiputados.Add(diputado);
                    }
                    ViewBag.listaDiputados = listaDiputados;
                }
                return View(proyectoLey);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        [Authorize]
        // GET: ProyectoLey/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // POST: ProyectoLey/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("ProyectoLeyID,NumeroExpediente,TituloCorto,TituloCompleto,FechaPresentacion,CantidadFirmas")] ProyectoLey proyectoLey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var listaProyectos = (from x in _context.ProyectoLeys select x).ToList();
                    if (listaProyectos.Count > 0)
                    {
                        foreach (var proyecto in listaProyectos)
                        {
                            if (proyecto.NumeroExpediente == proyectoLey.NumeroExpediente)
                            {
                                return Json(new { mensaje = "expedienteExistente" });
                            }
                        }
                        _context.Add(proyectoLey);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Add(proyectoLey);
                        await _context.SaveChangesAsync();
                    }
                    var proyectoAgregado = (from x in _context.ProyectoLeys select x).ToList().Last();
                    return Json(new { mensaje = "1", idProyecto = proyectoAgregado.ProyectoLeyID });
                }
                return Json(new { mensaje = "error" });
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }

        public IActionResult AddTemaAndDiputados(int id)
        {
            try
            {
                //almacena la lista de temas
                var temas = (from x in _context.Temas select x).ToList();
                List<SelectListItem> comboTemas = new List<SelectListItem>();
                foreach (var item in temas)
                {
                    comboTemas.Add(new SelectListItem
                    {
                        Value = item.TemaID.ToString(),
                        Text = item.TemaDesc
                    });
                }
                ViewBag.listaTemas = comboTemas;

                //almacena la lista de diputados
                var diputados = (from x in _context.Diputados select x).ToList();
                ViewBag.listaDiputados = diputados;

                ViewBag.idProyecto = id;
                return View();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        public async Task<JsonResult> AddTemaToProyecto(int ProyectoID, int TemaID)
        {
            try
            {
                ProyectoTema proyectoTema = new ProyectoTema();
                proyectoTema.ProyectoLeyID = ProyectoID;
                proyectoTema.TemaID = TemaID;

                var temasDelProyecto = (from x in _context.ProyectoTemas
                                        where x.ProyectoLeyID == ProyectoID
                                        select x).ToList();
                if (temasDelProyecto.Count > 0)
                {
                    foreach (var tema in temasDelProyecto)
                    {
                        if (tema.TemaID == TemaID)
                        {
                            return Json(new { mensaje = "temaExistente" });
                        }
                    }
                    _context.Add(proyectoTema);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Add(proyectoTema);
                    await _context.SaveChangesAsync();
                }
                return Json(new { mensaje = "1" });
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }

        public async Task<JsonResult> AddDiputadosToProyecto(int ProyectoID, int[] DiputadosID)
        {
            try
            {
                ProyectoDiputado proyectoDiputado = new ProyectoDiputado();
                proyectoDiputado.ProyectoLeyID = ProyectoID;

                var diputadosDelProyecto = (from x in _context.ProyectoDiputados
                                            where x.ProyectoLeyID == ProyectoID
                                            select x).ToList();
                if (diputadosDelProyecto.Count > 0)
                {
                    foreach (var diputado in diputadosDelProyecto)
                    {
                        _context.ProyectoDiputados.Remove(diputado);
                        await _context.SaveChangesAsync();
                    }
                }
                foreach (var id in DiputadosID)
                {
                    var item = new ProyectoDiputado()
                    {
                        ProyectoLeyID = ProyectoID,
                        DiputadoID = id
                    };
                    _context.ProyectoDiputados.Add(item);
                    await _context.SaveChangesAsync();
                }
                return Json(new { mensaje = "1" });
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }

        // GET: ProyectoLey/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var proyectoDeLey = await _context.ProyectoLeys.FindAsync(id);
                if (proyectoDeLey == null)
                {
                    return NotFound();
                }

                //consulta los temas asociados a ese proyecto de ley
                var temas = (from x in _context.ProyectoTemas where x.ProyectoLeyID == id select x).ToList();
                if (temas.Count > 0)
                {

                    List<Tema> listaTemas = new List<Tema>();
                    foreach (var item in temas)
                    {
                        var tema = (from x in _context.Temas
                                    where x.TemaID == item.TemaID
                                    select x).FirstOrDefault();
                        listaTemas.Add(tema);
                    }

                    List<SelectListItem> comboTemas = new List<SelectListItem>();
                    foreach (var item in listaTemas)
                    {
                        comboTemas.Add(new SelectListItem
                        {
                            Value = item.TemaID.ToString(),
                            Text = item.TemaDesc
                        });
                    }
                    ViewBag.listaTemas = comboTemas;
                }

                //consulta los diputados asociados a ese proyecto de ley
                var diputados = (from x in _context.ProyectoDiputados where x.ProyectoLeyID == id select x).ToList();
                if (diputados.Count > 0)
                {
                    List<Diputado> listaDiputados = new List<Diputado>();
                    foreach (var item in diputados)
                    {
                        var diputado = (from x in _context.Diputados
                                        where x.DiputadoID == item.DiputadoID
                                        select x).FirstOrDefault();
                        listaDiputados.Add(diputado);
                    }
                    ViewBag.listaDiputados = listaDiputados;
                }
                return View(proyectoDeLey);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        [Authorize]
        public async Task<IActionResult> EditTemaAndDiputados(int id)
        {
            try
            {
                //almacena la lista de temas
                var temas = (from x in _context.Temas select x).ToList();
                List<SelectListItem> comboTemas = new List<SelectListItem>();
                foreach (var item in temas)
                {
                    comboTemas.Add(new SelectListItem
                    {
                        Value = item.TemaID.ToString(),
                        Text = item.TemaDesc
                    });
                }
                ViewBag.listaTemas = comboTemas;
                ViewBag.idProyecto = id;

                var proyectoDiputados = (from x in _context.ProyectoDiputados
                                         where x.ProyectoLeyID == id
                                         select x).ToList();
                ViewBag.diputadosProyecto = proyectoDiputados;

                //almacena la lista de diputados
                var diputados = (from x in _context.Diputados select x).ToList();
                ViewBag.listaDiputados = diputados;

                var temasDelProyecto = (from x in _context.ProyectoTemas
                                        where x.ProyectoLeyID == id
                                        select x).ToList();

                List<Tema> listaTemas = new List<Tema>();
                if (temasDelProyecto.Count > 0)
                {
                    foreach (var item in temasDelProyecto)
                    {
                        var tema = (from x in _context.Temas
                                    where x.TemaID == item.TemaID
                                    select x).FirstOrDefault();
                        listaTemas.Add(tema);
                    }
                }
                ViewBag.temasAgregados = listaTemas;
                return View();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // POST: ProyectoLey/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ProyectoLeyID,NumeroExpediente,TituloCorto,TituloCompleto,FechaPresentacion,CantidadFirmas")] ProyectoLey proyectoLey)
        {
            try
            {
                if (id != proyectoLey.ProyectoLeyID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(proyectoLey);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProyectoLeyExists(proyectoLey.ProyectoLeyID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    BasicNotification("", NotificationType.Success, "Proyecto de Ley Actualizado Correctamente");
                    return RedirectToAction(nameof(Index));
                }
                return View(proyectoLey);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // GET: ProyectoLey/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var proyectoLey = await _context.ProyectoLeys
                    .FirstOrDefaultAsync(m => m.ProyectoLeyID == id);
                if (proyectoLey == null)
                {
                    return NotFound();
                }

                return View(proyectoLey);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }

        }

        // POST: ProyectoLey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var proyectoLey = await _context.ProyectoLeys.FindAsync(id);
                _context.ProyectoLeys.Remove(proyectoLey);
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

        //Elimina un año legislativo asginado a una comisión y a su vez a los diputados registrados en la tabla ComisionDiputados
        [HttpDelete]
        [Authorize]
        public async Task<JsonResult> DeleteTemaFromProyecto(int ProyectoID, int TemaID)
        {
            try
            {
                Models.ProyectoTema proyectoTema = new Models.ProyectoTema();
                proyectoTema.ProyectoLeyID = ProyectoID;
                proyectoTema.TemaID = TemaID;
                //consulta el proyecto con ese tema
                var consulta = (from x in _context.ProyectoTemas
                                where x.ProyectoLeyID == ProyectoID && x.TemaID == TemaID
                                select x).FirstOrDefault();
                if (consulta != null)
                {
                    _context.Remove(consulta);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Json(new { mensaje = "error" });
                }

                return Json(new { mensaje = "1" });
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }

        }
        private bool ProyectoLeyExists(int id)
        {
            return _context.ProyectoLeys.Any(e => e.ProyectoLeyID == id);
        }
    }
}
