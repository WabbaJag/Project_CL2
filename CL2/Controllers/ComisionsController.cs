
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CL2.Data;
using CL2.Models;
using tema = CL2.Models.Tema;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using CL2.Extensions;

namespace CL2.Controllers
{
    public class ComisionsController : BaseController
    {
        private readonly CL2Context _context;

        public ComisionsController(CL2Context context)
        {
            _context = context;
        }

        // GET: Comisions
        [Authorize]
        public IActionResult Index()
        {     
                           
                var comision = from s in _context.Comisiones.Include(c => c.Tema) select s;
             
                return View(comision);
           
        }

        // GET: Comisions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                TempData["idCom"] = id;
                TempData.Keep("idCom");

                var comision = await _context.Comisiones
                    .Include(c => c.Tema)
                    .FirstOrDefaultAsync(m => m.ComisionID == id);
                if (comision == null)
                {
                    return NotFound();
                }

                //consulta los años legislativos asociados a esa comisión
                var legislativo = (from x in _context.ComisionLegislativo where x.ComisionID == comision.ComisionID select x).ToList();
                if (legislativo.Count > 0)
                {
                    int idLegislativo = legislativo.FirstOrDefault().LegislativoID;
                    ViewBag.idLegislativo = idLegislativo;
                    TempData["idLeg"] = idLegislativo;
                    TempData.Keep("idLeg");

                    List<Legislativo> listaAnos = new List<Legislativo>();
                    foreach (var item in legislativo)
                    {
                        var ano = (from x in _context.Legislativos
                                   where x.LegislativoID == item.LegislativoID
                                   select x).FirstOrDefault();
                        listaAnos.Add(ano);
                    }

                    List<SelectListItem> comboAnosLegislativo = new List<SelectListItem>();
                    foreach (var item in listaAnos)
                    {
                        comboAnosLegislativo.Add(new SelectListItem
                        {
                            Value = item.LegislativoID.ToString(),
                            Text = item.AnnoLegislativo
                        });
                    }
                    ViewBag.listaAnosLegislativo = comboAnosLegislativo;
                }

                //se consulta la lista de temas y se envía a la vista con Hashtable para mostrar el nombre del tema
                IEnumerable<tema> temas = (from x in _context.Temas select x).ToList();
                Hashtable arr_temas = new Hashtable();
                foreach (var item in temas)
                {
                    arr_temas[item.TemaID] = item.TemaDesc;
                }
                ViewData["temas"] = arr_temas;

                return View(comision);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // GET: Comisions/Create
        [Authorize]
        public IActionResult Create()
        {
            try
            {
                //almacena la lista de temas
                var respuesta = (from x in _context.Temas select x).ToList();
                List<SelectListItem> comboTemas = new List<SelectListItem>();
                foreach (var item in respuesta)
                {
                    comboTemas.Add(new SelectListItem
                    {
                        Value = item.TemaID.ToString(),
                        Text = item.TemaDesc
                    });
                }
                ViewBag.listaTemas = comboTemas;
                //var prueba = new SelectList(_context.Temas, "TemaID", "TemaID");            
                //ViewData["TemaID"] = new SelectList(_context.Temas, "TemaID", "TemaID");
                return View();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // POST: Comisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> Create([Bind("ComisionID,NombreComision,Detalle,Tipo,TemaID")] Comision comision)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (_context.Comisiones.Any(a => a.NombreComision.Equals(comision.NombreComision) && a.ComisionID != comision.ComisionID))
                    {
                        return Json(new { mensaje = "nombreExistente" });
                    }

                    _context.Add(comision);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "La comisión " + comision.NombreComision /*comision.Name*/ + " fue creada correctamente.";
                    return Json(new { mensaje = "agregado", idComision = comision.ComisionID });
                    //return RedirectToAction(nameof(Index));
                }

                catch (Exception)
                {
                    return Json(new { mensaje = "error" });
                }
            }
            else
            {
                return Json(new { mensaje = "error" });
            }
        }
        //GET
        [Authorize]
        public async Task<IActionResult> AddLegislativeYear(int ComisionID, int? volverAEditar)
        {
            try
            {
                //almacena la lista de años legislativos
                var respuesta = (from x in _context.Legislativos select x).ToList();
                List<SelectListItem> comboLegislativos = new List<SelectListItem>();
                foreach (var item in respuesta)
                {
                    comboLegislativos.Add(new SelectListItem
                    {
                        Value = item.LegislativoID.ToString(),
                        Text = item.AnnoLegislativo
                    });
                }
                ViewBag.listaLegislativo = comboLegislativos;
                ViewBag.IdComision = ComisionID;

                if (volverAEditar == 1)
                {
                    ViewData["VolverAEditar"] = "regrese";
                }
                else
                {
                    ViewData["VolverAEditar"] = "no ";
                }

                //Aqui hay que agarrar el Id de administracion a partir de lo que se selecciono en el dropdown (legislativo)
                return View();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        //POST
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddLegislativeYear(int ComisionID, int LegislativoID)
        {
            try
            {
                ComisionLegislativo comisionLegislativo = new ComisionLegislativo();
                comisionLegislativo.ComisionID = ComisionID;
                comisionLegislativo.LegislativoID = LegislativoID;
                //consulta si la comision ya tiene un año legislativo asignado
                var consulta = (from x in _context.ComisionLegislativo
                                where x.ComisionID == ComisionID && x.LegislativoID == LegislativoID
                                select x).FirstOrDefault();
                if (consulta == null)
                {
                    _context.Add(comisionLegislativo);
                    await _context.SaveChangesAsync();
                }

                TempData["idCom"] = ComisionID;
                TempData["idLeg"] = LegislativoID;
                TempData.Keep("idLeg");
                TempData.Keep("idCom");


                return Json(new { mensaje = "200" });
                // return RedirectToAction("AgregarDiputados", "Comisions",new {idC = ComisionID, idL = LegislativoID});
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }

        }

        //Elimina un año legislativo asginado a una comisión y a su vez a los diputados registrados en la tabla ComisionDiputados
        [HttpDelete]
        [Authorize]
        public async Task<JsonResult> DeleteLegislativeYear(int ComisionID, int LegislativoID)
        {
            try
            {
                Models.ComisionLegislativo comisionLegislativo = new Models.ComisionLegislativo();
                comisionLegislativo.ComisionID = ComisionID;
                comisionLegislativo.LegislativoID = LegislativoID;
                //consulta la comisión con ese año legislativo asignado
                var consulta = (from x in _context.ComisionLegislativo
                                where x.ComisionID == ComisionID && x.LegislativoID == LegislativoID
                                select x).FirstOrDefault();
                var comisionDiputados = (from x in _context.ComisionDiputados
                                         where x.ComisionLegislativoID == consulta.ComisionLegislativoID
                                         select x).ToList();
                if (comisionDiputados.Count > 0)
                {
                    foreach (var item in comisionDiputados)
                    {
                        _context.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                }
                if (consulta != null)
                {
                    _context.Remove(consulta);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Json(new { mensaje = "error" });
                }

                return Json(new { mensaje = "200" });
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }

        }


        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AgregarDiputados(int[] DiputadosID)
        {
            try
            {
                int idCom = (int)TempData["idCom"];
                int idLeg = (int)TempData["idLeg"];
                TempData.Keep("idCom");
                TempData.Keep("idLeg");
                var comisionLegislativo = await _context.ComisionLegislativo.FirstOrDefaultAsync(a => a.ComisionID == idCom && a.LegislativoID == idLeg);
                //se consulta la lista de diputados asignados a esa comisión
                var diputadosComision = (from x in _context.ComisionDiputados
                                         where x.ComisionLegislativoID == comisionLegislativo.ComisionLegislativoID
                                         select x).ToList();
                //si existen diputados asignados se eliminan
                if (diputadosComision.Count > 0)
                {
                    foreach (var diputado in diputadosComision)
                    {
                        _context.ComisionDiputados.Remove(diputado);
                        await _context.SaveChangesAsync();
                    }
                }
                //se registran los diputados recibidos por parámetro
                if (ModelState.IsValid)
                {
                    foreach (var id in DiputadosID)
                    {
                        var item = new ComisionDiputados()
                        {
                            ComisionLegislativoID = comisionLegislativo.ComisionLegislativoID,
                            DiputadoID = id

                        };
                        _context.ComisionDiputados.Add(item);
                        await _context.SaveChangesAsync();

                    }
                    return Json(new { mensaje = "200" });
                }
                else
                {
                    return Json(new { mensaje = "error" });
                }
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> EliminarDiputados()
        {
            try
            {
                int idCom = (int)TempData["idCom"];
                int idLeg = (int)TempData["idLeg"];
                TempData.Keep("idCom");
                TempData.Keep("idLeg");
                var comisionLegislativo = await _context.ComisionLegislativo.FirstOrDefaultAsync(a => a.ComisionID == idCom && a.LegislativoID == idLeg);
                //se consulta la lista de diputados asignados a esa comisión
                var diputadosComision = (from x in _context.ComisionDiputados
                                         where x.ComisionLegislativoID == comisionLegislativo.ComisionLegislativoID
                                         select x).ToList();
                //si existen diputados asignados se eliminan
                if (diputadosComision.Count > 0)
                {
                    foreach (var diputado in diputadosComision)
                    {
                        _context.ComisionDiputados.Remove(diputado);
                        await _context.SaveChangesAsync();
                    }
                }
                return Json(new { mensaje = "200" });
            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }

        [Authorize]
        public async Task<IActionResult> EditLegislativeYear(int ComisionID, int LegislativoID)
        {
            try
            {

                var legislativo = _context.Legislativos.Where(e => e.LegislativoID == LegislativoID).FirstOrDefault();
                ViewBag.legislativo = legislativo.AnnoLegislativo;
                ViewBag.IdComision = ComisionID;

                var comisionLegislativo = (from x in _context.ComisionLegislativo
                                           where x.ComisionID == ComisionID && x.LegislativoID == LegislativoID
                                           select x).FirstOrDefault();

                var diputadosComision = (from x in _context.ComisionDiputados
                                         where x.ComisionLegislativoID == comisionLegislativo.ComisionLegislativoID
                                         select x).ToList();
                ViewBag.listaDiputados = diputadosComision;
                TempData["idCom"] = ComisionID;
                TempData["idLeg"] = LegislativoID;
                TempData.Keep("idLeg");
                TempData.Keep("idCom");
                return View(comisionLegislativo);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // GET: Comisions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var comision = await _context.Comisiones.FindAsync(id);
                if (comision == null)
                {
                    return NotFound();
                }

                //consulta los años legislativos asociados a esa comisión
                var legislativo = (from x in _context.ComisionLegislativo where x.ComisionID == comision.ComisionID select x).ToList();
                if (legislativo.Count > 0)
                {
                    int idLegislativo = legislativo.FirstOrDefault().LegislativoID;
                    ViewBag.idLegislativo = idLegislativo;

                    List<Legislativo> listaAnos = new List<Legislativo>();
                    foreach (var item in legislativo)
                    {
                        var ano = (from x in _context.Legislativos
                                   where x.LegislativoID == item.LegislativoID
                                   select x).FirstOrDefault();
                        listaAnos.Add(ano);
                    }

                    List<SelectListItem> comboAnosLegislativo = new List<SelectListItem>();
                    foreach (var item in listaAnos)
                    {
                        comboAnosLegislativo.Add(new SelectListItem
                        {
                            Value = item.LegislativoID.ToString(),
                            Text = item.AnnoLegislativo
                        });
                    }
                    ViewBag.listaAnosLegislativo = comboAnosLegislativo;
                }

                //almacena la lista de temas
                var respuesta = (from x in _context.Temas select x).ToList();
                List<SelectListItem> comboTemas = new List<SelectListItem>();
                foreach (var item in respuesta)
                {
                    comboTemas.Add(new SelectListItem
                    {
                        Value = item.TemaID.ToString(),
                        Text = item.TemaDesc
                    });
                }
                ViewBag.listaTemas = comboTemas;
                return View(comision);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetDiputadosFromComision(int LegislativoID, int ComisionID)
        {
            try
            {
                var comisionLegislativo = await _context.ComisionLegislativo.FirstOrDefaultAsync(a => a.LegislativoID == LegislativoID && a.ComisionID == ComisionID);
                var comisionDiputados = (from x in _context.ComisionDiputados
                                         where x.ComisionLegislativoID == comisionLegislativo.ComisionLegislativoID
                                         select x).ToList();
                List<Diputado> listaDiputados = new List<Diputado>();
                if (comisionDiputados != null)
                {
                    foreach (var item in comisionDiputados)
                    {
                        var diputado = (from x in _context.Diputados
                                        where x.DiputadoID == item.DiputadoID
                                        select x).FirstOrDefault();
                        Diputado objDiputado = new Diputado();
                        objDiputado.DiputadoID = diputado.DiputadoID;
                        objDiputado.NombreDiputado = diputado.NombreDiputado;
                        objDiputado.PrimerApellido = diputado.PrimerApellido;
                        objDiputado.SegundoApellido = diputado.SegundoApellido;
                        listaDiputados.Add(objDiputado);
                    }
                }

                if (listaDiputados.Count > 0)
                {
                    return Json(new { mensaje = "1", datos = listaDiputados });
                }
                else
                {
                    return Json(new { mensaje = "2", datos = listaDiputados });
                }

            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }

        // POST: Comisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ComisionID,NombreComision,Detalle,Tipo,TemaID")] Comision comision)
        {
            try
            {
                if (id != comision.ComisionID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    if (_context.Comisiones.Any(a => a.NombreComision.Equals(comision.NombreComision) && a.ComisionID != comision.ComisionID))
                    {
                        ViewData["TemaID"] = new SelectList(_context.Temas, "TemaID", "TemaID", comision.TemaID);
                        TempData["Duplicate"] = "Ya existe una comisión con ese nombre.";
                        return RedirectToAction("Edit", new { id = id });
                    }

                    try
                    {
                        _context.Update(comision);
                        await _context.SaveChangesAsync();
                        BasicNotification("", NotificationType.Success, "Comisión Actualizada Correctamente");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ComisionExists(comision.ComisionID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                ViewData["TemaID"] = new SelectList(_context.Temas, "TemaID", "TemaID", comision.TemaID);
                TempData["SuccessMessage"] = "Comision" + comision.NombreComision /*comision.Name*/ + " Saved Successfully ";
                return View(comision);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // GET: Comisions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var comision = await _context.Comisiones
                    .Include(c => c.Tema)
                    .FirstOrDefaultAsync(m => m.ComisionID == id);
                if (comision == null)
                {
                    return NotFound();
                }

                return View(comision);
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        // POST: Comisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var comision = await _context.Comisiones.FindAsync(id);
                _context.Comisiones.Remove(comision);
                await _context.SaveChangesAsync();
                BasicNotification("", NotificationType.Success, "Comisión Eliminada Correctamente");
                //TempData["SuccessMessage"] = "Comision" + comision.NombreComision /*comision.Name*/ + " Deleted Successfully ";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        private bool ComisionExists(int id)
        {
            return _context.Comisiones.Any(e => e.ComisionID == id);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetDiputadosFromPlenario(int IdLegislativo)
        {
            try
            {
                var legislativo = await _context.Legislativos.FirstOrDefaultAsync(a => a.LegislativoID == IdLegislativo);
                var plenario = await _context.Plenarios.FirstOrDefaultAsync(a => a.AdministracionID == legislativo.AdministracionID);
                var integrantesPlenario = await _context.IntegrantesPlenarios.FirstOrDefaultAsync(a => a.PlenarioID == plenario.PlenarioID);

                var diputadosDelPlenario = await _context.IntegrantesPlenarios.Include(i => i.Plenario).Where(i => i.PlenarioID == plenario.PlenarioID)
                    .Select(i => i.Diputado).ToListAsync();

                List<Diputado> listaDiputados = new List<Diputado>();
                if (diputadosDelPlenario != null)
                {
                    foreach (var item in diputadosDelPlenario)
                    {
                        Diputado objDiputado = new Diputado();
                        objDiputado.DiputadoID = item.DiputadoID;
                        objDiputado.NombreDiputado = item.NombreDiputado;
                        objDiputado.PrimerApellido = item.PrimerApellido;
                        objDiputado.SegundoApellido = item.SegundoApellido;
                        listaDiputados.Add(objDiputado);
                    }
                }

                if (listaDiputados.Any())
                {
                    return Json(new { mensaje = "1", datos = listaDiputados });
                }
                else
                {
                    return Json(new { mensaje = "2", datos = listaDiputados });
                }

            }
            catch (Exception)
            {
                return Json(new { mensaje = "error" });
            }
        }
    }
}