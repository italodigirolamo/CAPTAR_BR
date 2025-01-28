using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAPTAR.Data;
using CAPTAR.Models;
using CAPTAR.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace CAPTAR.Controllers
{
     public class PropietarioController : Controller
    {
        private readonly AppDbContext _context;
        public PropietarioController(AppDbContext context)
        {
            _context = context;
        }

        //  [HttpGet]
        // GET: Propietarios
        [Authorize]
        public async Task<IActionResult> Index
            (string searchString, string ActualOrder, int? numpage, string Actualfilter, int? totalRecords, 
            int? ItemxPag  )
        {
            var propietarios = from propietario in _context.Propietario select propietario;
            ModelState.Clear();

            if (searchString != "" && searchString != null)
                numpage = 1;
            else
              searchString = Actualfilter;

            if (!string.IsNullOrEmpty(searchString))
            {
                propietarios = propietarios.Where(s => s.Nombre.Contains(searchString));
            }

            ViewData["FilterName"] = string.IsNullOrEmpty(ActualOrder) ? "NameDesc" : "";
            ViewData["ActualOrder"] = ActualOrder;
            ViewData["ActualFilter"] = Actualfilter;
            var Item = new[,] { { "2" }, { "3"}, { "5" }, { "20" } };
            ViewBag.ItemxPag = Item;

            switch (ActualOrder)
            {
                case "NameDesc":
                    propietarios = propietarios.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    propietarios = propietarios.OrderBy(s => s.Nombre);
                    break;
            }

            return View(await Pagination<Propietario>.CreatePagination(propietarios.AsNoTracking(), numpage ?? 1, ItemxPag ?? 5));
        }

        // GET: Propietarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propietario == null)
            {
                return NotFound();
            }

            return View(propietario);
        }

        // GET: Propietarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDocumento,Nombre,Direccion,Correo")] Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propietario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propietario);
        }

        // GET: Propietarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietario.FindAsync(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return View(propietario);
        }

        // POST: Propietarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDocumento,Nombre,Direccion,Correo")] Propietario propietario)
        {
            if (id != propietario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propietario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropietarioExists(propietario.Id))
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
            return View(propietario);
        }

        // GET: Propietarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propietario == null)
            {
                return NotFound();
            }

            return View(propietario);
        }

        // POST: Propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propietario = await _context.Propietario.FindAsync(id);
            if (propietario != null)
            {
                _context.Propietario.Remove(propietario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropietarioExists(int id)
        {
            return _context.Propietario.Any(e => e.Id == id);
        }
    }
}
