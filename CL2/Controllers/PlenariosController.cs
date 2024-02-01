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
  
    public class PlenariosController : BaseController
    {
        private readonly CL2Context _context;     

        public PlenariosController(CL2Context context)
        {
            _context = context;
        }


        [Authorize]
        public IActionResult Index()
        {
            var plenarios = from s in _context.Plenarios.Include(p => p.Administracion) select s;
            return View(plenarios);
        }


        // GET: Plenarios/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plenario = await _context.Plenarios.Include(x => x.IntegrantesPlenarios).ThenInclude(l => l.Fraccion)
                .Include(p => p.Administracion).Include(i => i.IntegrantesPlenarios).ThenInclude(i => i.Diputado)
                .FirstOrDefaultAsync(m => m.PlenarioID == id);

            if (plenario == null)
            {
                return NotFound();
            }

            return View(plenario);
        }


        // GET: Plenarios/Create      
        [Authorize]
        public IActionResult Create(string searchString)


        {
            ViewBag.Diputados = _context.Diputados;
            ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo");
            ViewBag.DiputadoID = new SelectList(_context.Diputados, "DiputadoID", "NombreDiputado");
            ViewBag.FraccionID = new SelectList(_context.Fracciones, "FraccionID", "NombreFraccion");

            ViewData["CurrentFilter"] = searchString;
            var diputados = from s in _context.Diputados
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                diputados = diputados.Where(s => s.NombreDiputado.Contains(searchString)
                                       || s.PrimerApellido.Contains(searchString)
                                       || s.SegundoApellido.Contains(searchString));
            }
            return View();
        }

        // POST: Plenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("PlenarioID,AdministracionID")] Plenario plenario, int[] DiputadosID)
        {

            ViewBag.Diputados = _context.Diputados;

            var plenarioDiputados = new List<IntegrantesPlenario>();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(plenario);
                    await _context.SaveChangesAsync();

                    foreach (var id in DiputadosID)

                    {
                        var item = new IntegrantesPlenario()
                        {
                            PlenarioID = plenario.PlenarioID,
                            DiputadoID = id
                        };
                        plenarioDiputados.Add(item);

                    }
                    _context.AddRange(plenarioDiputados);
                    await _context.SaveChangesAsync();
                    TempData["idPlen"] = plenario.PlenarioID;                  
                    TempData.Keep("idPlen");

                    BasicNotification("", NotificationType.Success, "Plenario Creado Correctamente");
                    return RedirectToAction(nameof(Index));
                    //return RedirectToAction("AddFraccion","IntegrantesPlenario");
                }
            }
            catch
            {
                BasicNotification("Plenario no creado", NotificationType.Error, "Seleccione otra Administración");
             
                ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionPeriodo");
                return View();
            }
            
            
            return View(plenario);
        }


       

        // GET: Plenarios/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            //la lista de todos los diputados
             ViewBag.Diputados = _context.Diputados;

            //para mandarle a la vista los Diputados parte del Plenario
            var plenario = await _context.Plenarios
                .Include(p => p.Administracion).Include(i => i.IntegrantesPlenarios).
                ThenInclude(i => i.Diputado)
                .FirstOrDefaultAsync(m => m.PlenarioID == id);

            if (plenario == null)
            {
                return NotFound();
            }

                     
            return View(plenario);
        }



        // POST: Plenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("PlenarioID,AdministracionID")] Plenario plenario, int[] DiputadosID)
        {

           // ViewData["AdministracionID"] = new SelectList(_context.Administraciones, "AdministracionID", "AdministracionID", plenario.AdministracionID);

            if (id != plenario.PlenarioID)
            {
                return NotFound();
            }

          
            if (ModelState.IsValid)
            {
                try
                {
                    //un nueva lista tipo IntegrantesPlenario
                    var plenarioDiputados = new List<IntegrantesPlenario>();

                    // lista de IntegrantesPlenario donde el PlenarioID es el del plenario a Editar
                    var PDList = await _context.IntegrantesPlenarios.Where(td => td.PlenarioID == plenario.PlenarioID).ToListAsync();
                   
                    //se remueven todos los que pertenecen a la lista 
                    _context.RemoveRange(PDList);

                    //loop para agregar todos los Diputados selecionados al Plenario
                    foreach (var nuevoID in DiputadosID)
                    {

                        var item = new IntegrantesPlenario()
                        {
                            PlenarioID = plenario.PlenarioID,
                            DiputadoID = nuevoID

                        };
                        plenarioDiputados.Add(item);

                    }

                    _context.AddRange(plenarioDiputados);
                    BasicNotification("", NotificationType.Success, "Plenario Actualizado");
                    //_context.Update(plenario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    BasicNotification("", NotificationType.Error, "Plenario No Actualizado");
                }
              
                return RedirectToAction(nameof(Index));
            }

          
            return View(plenario);
        }

        // GET: Plenarios/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plenario = await _context.Plenarios
               .Include(p => p.Administracion).Include(i => i.IntegrantesPlenarios).ThenInclude(i => i.Diputado)
               .FirstOrDefaultAsync(m => m.PlenarioID == id);

            if (plenario == null)
            {
                return NotFound();
            }

            return View(plenario);
        }

        // POST: Plenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var plenario = await _context.Plenarios.FindAsync(id);
                _context.Plenarios.Remove(plenario);
                await _context.SaveChangesAsync();              
                BasicNotification("", NotificationType.Success, "Plenario Eliminado");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                BasicNotification("Plenario No Eliminado", NotificationType.Error, "Existen datos dependientes de este Plenario");
                return RedirectToAction(nameof(Index));
            }
          
        }

        private bool PlenarioExists(int id)
        {
            return _context.Plenarios.Any(e => e.PlenarioID == id);
        }




    }
}
