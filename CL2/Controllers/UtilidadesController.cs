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

namespace CL2.Controllers
{
    public class UtilidadesController : Controller
    {
        private readonly CL2Context _context;

        public UtilidadesController(CL2Context context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var tables = new Utilidades();
            tables.Administracions = await _context.Administraciones.ToListAsync();
            tables.Legislativos = await _context.Legislativos.Include(p=>p.Administracion).ToListAsync();
            tables.Temas = await _context.Temas.ToListAsync();
            tables.Fraccions = await _context.Fracciones.ToListAsync();


            return View(tables);

        }
    }
}
