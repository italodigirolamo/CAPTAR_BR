using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAPTAR.Data;
using CAPTAR.Models;
using CAPTAR.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace CAPTAR.Controllers
{
    public class PropiedadController : Controller
    {
        private readonly AppDbContext _context;

        public PropiedadController(AppDbContext context)
        {
            _context = context;
        }


        //[HttpGet]
        // GET: Propiedad
        [Authorize]
        public async Task<IActionResult> Index
          //(string searchString,
          //string sortOrder,
          //int? pageNumber,
          //string currentFilter)
          (string searchString, string ActualOrder,
            int? numpage, string Actualfilter, int? totalRecords,
            int? ItemxPag = 5)


        {
            var propiedades = from propiedad in _context.Propiedad select propiedad;


            if (searchString != "" && searchString != null)
                //numpage= numpage ?? 1;
                numpage = 1;
            else
                searchString = Actualfilter;


            if (!string.IsNullOrEmpty(searchString))
            {
                propiedades = propiedades.Where(s => s.Nombre.Contains(searchString));
            }


            ViewData["FilterName"] = string.IsNullOrEmpty(ActualOrder) ? "NameDesc" : "";
            // ViewData["FilterName"] = string.IsNullOrEmpty(filter) ? "NameDesc" : "";
            ViewData["ActualOrder"] = ActualOrder;
            ViewData["ActualFilter"] = Actualfilter;
            ViewData["ItemxPag"] = ItemxPag;

            ViewData["CurrentFilter"] = searchString;


            switch (ActualOrder)
            {
                case "NameDesc":
                    propiedades = propiedades.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    propiedades = propiedades.OrderBy(s => s.Nombre);
                    break;
            }

            //int pageSize = 3;

            return View(await Pagination<Propiedad>.CreatePagination(propiedades.AsNoTracking(), numpage ?? 1, totalRecords ?? 3));


            // return View(await clientes.ToListAsync());
            //  return View(await propiedades.AsNoTracking().ToListAsync());
        }

        // GET: Propiedad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propiedad = await _context.Propiedad
                .FirstOrDefaultAsync(m => m.id == id);
            if (propiedad == null)
            {
                return NotFound();
            }

            return View(propiedad);
        }

        // GET: Propiedad/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Zonas = await _context.Zona.ToListAsync();
            // ViewBag.Clientes = await _dbContext.Clientes.ToListAsync();
            //ViewBag.Productos = await _dbContext.Productos.ToListAsync();
            return View();
        }

        // POST: Propiedad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nombre,Direccion,Zona,Tipo")] Propiedad propiedad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propiedad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propiedad);
        }

        // GET: Propiedad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propiedad = await _context.Propiedad.FindAsync(id);
            if (propiedad == null)
            {
                return NotFound();
            }
            return View(propiedad);
        }

        // POST: Propiedad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Nombre,Direccion,Zona,Tipo")] Propiedad propiedad)
        {
            if (id != propiedad.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propiedad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropiedadExists(propiedad.id))
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
            return View(propiedad);
        }

        // GET: Propiedad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propiedad = await _context.Propiedad
                .FirstOrDefaultAsync(m => m.id == id);
            if (propiedad == null)
            {
                return NotFound();
            }

            return View(propiedad);
        }

        // POST: Propiedad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propiedad = await _context.Propiedad.FindAsync(id);
            if (propiedad != null)
            {
                _context.Propiedad.Remove(propiedad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropiedadExists(int id)
        {
            return _context.Propiedad.Any(e => e.id == id);
        }
    }
}
