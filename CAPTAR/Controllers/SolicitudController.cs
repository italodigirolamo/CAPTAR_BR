﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAPTAR.Data;
using CAPTAR.DTO;
using CAPTAR.Models;
using Microsoft.AspNetCore.Authorization;
using MimeKit;

namespace CAPTAR.Controllers
{
    public class SolicitudController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        public IWebHostEnvironment _env;
        public SolicitudController(AppDbContext context, IEmailService emailService, IWebHostEnvironment env)
        {
            _context = context;
            _emailService = emailService;
            _env = env;
        }

        // GET: Solicitud
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await _context.Solicitud.ToListAsync());
        }

        // GET: Solicitud/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .FirstOrDefaultAsync(m => m.id == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Solicitud/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Zonas = await _context.Zona.ToListAsync();
           
            //ViewBag.Propiedades = await _context.Propiedad.ToListAsync();
            return View();
        }

        // POST: Solicitud/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create(  EmailDto email, Solicitud solicitud)
        // public async Task<IActionResult> Create(SolicitudDto solicitud, EmailDto email)
        {
                if (ModelState.IsValid)
            {
                _context.Add(solicitud);
                //var result = await _context.SaveChangesAsync();
                await _context.SaveChangesAsync();

                var zonaN = _context.Zona.FirstOrDefault(s => s.id.ToString() == solicitud.Zona) ;
                //_context.Propietario.Any(e => e.Id == id);
                var zonaName = _context.Zona.Where(z => z.id == solicitud.id);


                //if (_context.Solicitud.Find(s => s. solicitud.Email)..count > 5) ;

                var webRoot = _env.WebRootPath; // Get wwwroot Folder
                                                // Get TemplateFile located at wwwroot/Templates/EmailTemplate/Confirm_Account_Registration.html
                var pathToFile = _env.WebRootPath
                     + Path.DirectorySeparatorChar.ToString()
                     + "Templates"
                    + Path.DirectorySeparatorChar.ToString()
                    + "EmailTemplate.html";
                    //+ Path.DirectorySeparatorChar.ToString()
                    //+ "Confirm_Account_Registration.html";

                var builder = new BodyBuilder();

                ViewBag.Message = "Se ha enviado su solicitud, pronto nos pondremos en contacto usted..!";
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }

                string messageBody = string.Format(builder.HtmlBody,
                    solicitud.NombreCompleto,
                    solicitud.Direccion,
                    zonaN.Nombre,
                    //solicitud.Zona,
                    solicitud.Numero,
                    solicitud.Dormitorios,
                    solicitud.Banos,
                     solicitud.Estacionamiento,
                    solicitud.Valor,
                    solicitud.Contacto,
                     solicitud.Telefono,
                    solicitud.Fecha
                    );

                email.Body = messageBody;

                //await _emailService.SendEmail(email);

                await _emailService.SendEmail(new EmailDto
                {
                    To = solicitud.Email,
                    Subject = "CAPTAR - Solicitud de Informacion y Propiedades",
                    Body = email.Body

                });


                ViewBag.Message = "Se ha enviado su solicitud, pronto nos pondremos le estaremos contactando..!";
            return RedirectToAction(nameof(Index));
                  //return View(solicitud);
               // return View();
            }
            // return View(solicitud);
            return View();
        }

        // GET: Solicitud/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        // POST: Solicitud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,NombreCompleto,Direccion,Email,Contacto,Fecha")] Solicitud solicitud)
        {
            if (id != solicitud.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.id))
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
            return View(solicitud);
        }

        // GET: Solicitud/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .FirstOrDefaultAsync(m => m.id == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // POST: Solicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud != null)
            {
                _context.Solicitud.Remove(solicitud);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudExists(int id)
        {
            return _context.Solicitud.Any(e => e.id == id);
        }
    }
}
