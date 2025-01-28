using CAPTAR.Data;
using CAPTAR.Models;
using CAPTAR.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAPTAR.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
   
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task <ActionResult> Index(string searchString, string ActualOrder, int? numpage, string Actualfilter, int? totalRecords,
            int? ItemxPag)
        {
            var usuarios = from usuario in _context.Usuario select usuario;
            ModelState.Clear();

            if (searchString != "" && searchString != null)
                numpage = 1;
            else
                searchString = Actualfilter;

            if (!string.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.NombreCompleto.Contains(searchString));
            }


            ViewData["FilterName"] = string.IsNullOrEmpty(ActualOrder) ? "NameDesc" : "";
            ViewData["ActualOrder"] = ActualOrder;
            ViewData["ActualFilter"] = Actualfilter;
            var Item = new[,] { { "2" }, { "3" }, { "5" }, { "20" } };
            ViewBag.ItemxPag = Item;

            switch (ActualOrder)
            {
                case "NameDesc":
                    usuarios = usuarios.OrderByDescending(s => s.NombreCompleto);
                    break;
                default:
                    usuarios = usuarios.OrderBy(s => s.NombreCompleto);
                    break;
            }

            return View(await Pagination<Usuario>.CreatePagination(usuarios.AsNoTracking(), numpage ?? 1, ItemxPag ?? 5));

            //return View(await _context.Usuario.ToListAsync());
        }

    }
}
