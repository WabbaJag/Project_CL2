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
using Newtonsoft.Json;
using Nancy.Json;

namespace CL2.Controllers
{
    public class IntegrantesPlenarioController : BaseController
    {
        private readonly CL2Context _context;
        public IntegrantesPlenarioController(CL2Context context)
        {
            _context = context;
        }

        // GET: Plenarios/AddFraccion
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddFraccionAsync()
        {
            int idPlen = (int)TempData["idPlen"];
            TempData.Keep("idPlen");           
            var integrantes = await _context.IntegrantesPlenarios.Include(i => i.Diputado).Where(s => s.PlenarioID == idPlen).ToListAsync();
            ViewBag.FraccionID = new SelectList(_context.Fracciones, "FraccionID", "NombreFraccion");
            ViewBag.DiputadoID = new SelectList(_context.Diputados, "DiputadoID", "NombreDiputado");

            var lista = new List<IntegrantesPlenario>();
            if (integrantes?.Any() == true)
            {
                foreach (var item in integrantes)
                {
                    lista.Add(new IntegrantesPlenario()
                    {
                        PlenarioID = idPlen,
                        DiputadoID = item.DiputadoID,
                        FraccionID = item.FraccionID
                    });
                }
            }


            return View(lista.ToList());
        }


        [HttpPost]
        [Authorize]
        public IActionResult AddFraccion(string listofusers)
        {

            var jsonModel = new JavaScriptSerializer().Deserialize<object>(listofusers);

            //foreach (var item in integrantesPlenariosLista)
            //    {
            //    IntegrantesPlenario integrantesPlenario = new()
            //    {
            //        PlenarioID = item.PlenarioID,
            //        DiputadoID = item.DiputadoID,
            //        FraccionID = item.FraccionID
            //    };
            //    _context.Add(integrantesPlenario);
            //    _context.SaveChangesAsync();

        //};


            return Json(new { msg = "Successfully added " });

            //BasicNotification("", NotificationType.Success, "Plenario Creado Correctamente");
            //return RedirectToAction(nameof(Index));
            //return RedirectToAction("Index", "Plenarios");

        }
           


         




        // GET: IntegrantesPlenario
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cL2Context = _context.IntegrantesPlenarios.Include(i => i.Diputado)
                //.Include(i => i.Fraccion)
                .Include(i => i.Plenario);
            return View(await cL2Context.ToListAsync());
        }



        // GET: IntegrantesPlenario/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integrantesPlenario = await _context.IntegrantesPlenarios
                .Include(i => i.Diputado)
                //.Include(i => i.Fraccion)
                .Include(i => i.Plenario)
                .FirstOrDefaultAsync(m => m.PlenarioID == id);
            if (integrantesPlenario == null)
            {
                return NotFound();
            }

            return View(integrantesPlenario);
        }

        // GET: IntegrantesPlenario/Create
        [Authorize]
        public IActionResult Create()
        {


            //para la checkbox list de Diputados
            ViewBag.Diputados = _context.Diputados;

            ViewData["PlenarioID"] = _context.Plenarios.Select(a => new SelectListItem()
            {
                Value = a.PlenarioID.ToString(),
                Text = a.Administracion.AdministracionPeriodo
            })
                          .ToList();





            //para que muestre los años de administración en vez de los PlenarioID
            ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo");

            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID");
            ViewData["FraccionID"] = new SelectList(_context.Fracciones, "FraccionID", "FraccionID");
            //ViewData["PlenarioID"] = new SelectList(_context.Plenarios, "PlenarioID", "PlenarioID");

            return View();
        }

        //POST: IntegrantesPlenario/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DiputadoID,PlenarioID,FraccionID,Detalle_Puesto")] int[] DiputadosID, int PlenarioID)
        {
           

            foreach (int dipID in DiputadosID)
            {

                
                IntegrantesPlenario integrantesPlenario = new IntegrantesPlenario();
                integrantesPlenario.PlenarioID = PlenarioID;
                integrantesPlenario.DiputadoID = dipID;

            

                //crear Integrantes Plenaro - el Fraccion ID está en el código ya que aún no está el método
                //_context.Add(new IntegrantesPlenario { PlenarioID = PlenaID, DiputadoID = dipID, FraccionID = 1 });
                //_context.SaveChanges();

                _context.Add(integrantesPlenario);
                await _context.SaveChangesAsync();

            }


            return RedirectToAction("Index", "Plenarios");
        }





        //public async Task<IActionResult> Create([Bind("DiputadoID,PlenarioID,FraccionID,Detalle_Puesto")] IntegrantesPlenario integrantesPlenario)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(integrantesPlenario);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID", integrantesPlenario.DiputadoID);
        //    ViewData["FraccionID"] = new SelectList(_context.Fracciones, "FraccionID", "FraccionID", integrantesPlenario.FraccionID);
        //    ViewData["PlenarioID"] = new SelectList(_context.Plenarios, "PlenarioID", "PlenarioID", integrantesPlenario.PlenarioID);
        //    return View(integrantesPlenario);
        //}

        // GET: IntegrantesPlenario/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plenario = await _context.Plenarios.Include(i => i.IntegrantesPlenarios).ThenInclude(i => i.Diputado)
                .Where(s => s.PlenarioID == id).AsNoTracking()
                .FirstOrDefaultAsync();

            if (plenario == null)
            {
                return NotFound();
            }

        





            //ViewData["DiputadoID"] = _context.IntegrantesPlenarios.Select(a => new SelectListItem()
            // {
            //     Value = a.DiputadoID.ToString(),
            //     Text = a.Diputado.NombreDiputado
            // })
            //          .ToList();


            //MostrarDiputadosAsignadosPlenario(plenario);


            //var integrantesPlenario = await _context.IntegrantesPlenarios.FindAsync(id);
            //if (integrantesPlenario == null)
            //{
            //    return NotFound();
            //}
            //ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID", integrantesPlenario.DiputadoID);
            //ViewData["FraccionID"] = new SelectList(_context.Fracciones, "FraccionID", "FraccionID", integrantesPlenario.FraccionID);
            //ViewData["PlenarioID"] = new SelectList(_context.Plenarios, "PlenarioID", "PlenarioID", integrantesPlenario.PlenarioID);
            return View(plenario);
        }

        //private void MostrarDiputadosAsignadosPlenario(Plenario plenario)
        //{
        //    var todosDiputados = _context.Diputados;
        //    var plenarioDiputados = new HashSet<int>(plenario.IntegrantesPlenarios.Select(c => c.DiputadoID));
        //    var viewModel = new List<IntegrantesPlenario>();
        //    foreach (var diputado in todosDiputados)
        //    {
        //        viewModel.Add(new IntegrantesPlenario
        //        {
        //            DiputadoID = diputado.DiputadoID,
                 
                
        //        });

        //    }
        //    ViewBag["Diputados"] = viewModel;
        //}

        // POST: IntegrantesPlenario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiputadoID,PlenarioID,FraccionID,Detalle_Puesto")] IntegrantesPlenario integrantesPlenario)
        {
            if (id != integrantesPlenario.PlenarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(integrantesPlenario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntegrantesPlenarioExists(integrantesPlenario.PlenarioID))
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
            ViewData["DiputadoID"] = new SelectList(_context.Diputados, "DiputadoID", "DiputadoID", integrantesPlenario.DiputadoID);
            //ViewData["FraccionID"] = new SelectList(_context.Fracciones, "FraccionID", "FraccionID", integrantesPlenario.FraccionID);
            ViewData["PlenarioID"] = new SelectList(_context.Plenarios, "PlenarioID", "PlenarioID", integrantesPlenario.PlenarioID);
            return View(integrantesPlenario);
        }

        // GET: IntegrantesPlenario/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integrantesPlenario = await _context.IntegrantesPlenarios
                .Include(i => i.Diputado)
                //.Include(i => i.Fraccion)
                .Include(i => i.Plenario)
                .FirstOrDefaultAsync(m => m.PlenarioID == id);
            if (integrantesPlenario == null)
            {
                return NotFound();
            }

            return View(integrantesPlenario);
        }

        // POST: IntegrantesPlenario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var integrantesPlenario = await _context.IntegrantesPlenarios.FindAsync(id);
            _context.IntegrantesPlenarios.Remove(integrantesPlenario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntegrantesPlenarioExists(int id)
        {
            return _context.IntegrantesPlenarios.Any(e => e.PlenarioID == id);
        }
    }
}
