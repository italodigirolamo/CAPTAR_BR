using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAPTAR.Data;
using CAPTAR.Models;

namespace CAPTAR.Controllers
{
    public class SoliDetalleController : Controller
    {
        private readonly AppDbContext _context;

        public SoliDetalleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SoliDetalle
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SoliDetalles.Include(s => s.propietario).Include(s => s.Solicitud);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SoliDetalle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soliDetalle = await _context.SoliDetalles
                .Include(s => s.propietario)
                .Include(s => s.Solicitud)
                .FirstOrDefaultAsync(m => m.SoliDetalleId == id);
            if (soliDetalle == null)
            {
                return NotFound();
            }

            return View(soliDetalle);
        }

        // GET: SoliDetalle/Create
        public IActionResult Create()
        {
            ViewBag["PropietarioId"] = new SelectList(_context.Zona, "Id", "Nombre");
            //ViewBag["SolicitudId"] = new SelectList(_context.Solicitud, "id", "Direccion");
            return View();
        }

        // POST: SoliDetalle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoliDetalleId,Nombre,Direccion,Zona,Tipo,PropietarioId,SolicitudId")] SoliDetalle soliDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soliDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropietarioId"] = new SelectList(_context.Propietario, "Id", "Correo", soliDetalle.PropietarioId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "id", "Direccion", soliDetalle.SolicitudId);
            return View(soliDetalle);
        }

        // GET: SoliDetalle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soliDetalle = await _context.SoliDetalles.FindAsync(id);
            if (soliDetalle == null)
            {
                return NotFound();
            }
            ViewData["PropietarioId"] = new SelectList(_context.Propietario, "Id", "Correo", soliDetalle.PropietarioId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "id", "Direccion", soliDetalle.SolicitudId);
            return View(soliDetalle);
        }

        // POST: SoliDetalle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoliDetalleId,Nombre,Direccion,Zona,Tipo,PropietarioId,SolicitudId")] SoliDetalle soliDetalle)
        {
            if (id != soliDetalle.SoliDetalleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soliDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoliDetalleExists(soliDetalle.SoliDetalleId))
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
            ViewData["PropietarioId"] = new SelectList(_context.Propietario, "Id", "Correo", soliDetalle.PropietarioId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "id", "Direccion", soliDetalle.SolicitudId);
            return View(soliDetalle);
        }

        // GET: SoliDetalle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soliDetalle = await _context.SoliDetalles
                .Include(s => s.propietario)
                .Include(s => s.Solicitud)
                .FirstOrDefaultAsync(m => m.SoliDetalleId == id);
            if (soliDetalle == null)
            {
                return NotFound();
            }

            return View(soliDetalle);
        }

        // POST: SoliDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soliDetalle = await _context.SoliDetalles.FindAsync(id);
            if (soliDetalle != null)
            {
                _context.SoliDetalles.Remove(soliDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoliDetalleExists(int id)
        {
            return _context.SoliDetalles.Any(e => e.SoliDetalleId == id);
        }
    }
}
