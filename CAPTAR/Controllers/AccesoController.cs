using CAPTAR.Data;
using CAPTAR.DTO;
using CAPTAR.Models;
using CAPTAR.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using CAPTAR.Services;


namespace CAPTAR.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly IEmailService emailService;
        public AccesoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Registrarse()
        {
            _context.Usuario.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(UsuarioDto usuarioDto)
        { 
            if (usuarioDto.Password != usuarioDto.ConfirmPass)
            {
                ViewBag.Nombre = usuarioDto.NombreCompleto;
                ViewBag.Email = usuarioDto.Email;
                ViewData["Message"] = "Contraseñas No coinciden";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                NombreCompleto = usuarioDto.NombreCompleto,
                Email = (usuarioDto.Email).ToLower(),
                //Password = (usuarioDto.Password).ToLower()
                Password = Services.UtilitisService.ConvertSHA256((usuarioDto.Password).ToLower()),
                //Rol = usuarioDto.Rol
                Rol = "User"

                ///ConvertSHA256
            };

            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            if(usuario.Id != 0)
            {
                return RedirectToAction("Login", "Acceso");
            }
            ViewData["Message"] = "No se pudo crear el Usuario ";
            return RedirectToAction("Login", "Acceso");
                }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            
            _context.Usuario.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto login, EmailDto emailDto )
        {

            if (login.Email != null)
            { 

            Usuario? _usuario = await _context.Usuario
                                .Where(u => u.Email == (login.Email) &&
                                // (u.Password)  == login.Password)
                                 (u.Password) == Services.UtilitisService.ConvertSHA256(login.Password))
                                    .FirstOrDefaultAsync();
            if (_usuario == null)
            {
                ViewData["Message"]= "Verique sus datos, No se encontraron coincidencias! o Regístrese";
                return View();
            }

                ViewBag.Message = "Usted ha ingresado al sistema ..!";

                List<Claim> claims = new List<Claim>();
        //{ 
        //   new Claim(ClaimTypes.Name, _usuario.NombreCompleto)
        //};
            claims.Add(new Claim(ClaimTypes.Name, _usuario.NombreCompleto));
            claims.Add(new Claim(ClaimTypes.Email, _usuario.Email));
            claims.Add(new Claim("Name", _usuario.ToString() ));


            // Claim claims = new Claim(ClaimTypes.Email, login.Email);


            //((ClaimsIdentity)_usuario.NombreCompleto).AddClaims(
            // new[] { new Claim("Nombre", login.NombreCompleto) });


            //var identity = await _context.Usuario(login);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties()
            {

                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authenticationProperties);

          return  RedirectToAction("Index", "Home");
        }
            else
            {
                ViewData["Message"] = "Verique sus datos ";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Salir(LoginDto login)
        {
            // _context.Usuario.ToListAsync();

            Usuario? _usuario = await _context.Usuario
                             .Where(u => u.Email == (login.Email) &&
                                 (u.Password) == login.Password)
                                 .FirstOrDefaultAsync();

            if (_usuario == null)
            {
                ViewData["Message"] = "Verique sus datos, No se encontraron coincidencias!";
                return View();
            }

            List<Claim> claims = new List<Claim>()
        {
           new Claim(ClaimTypes.Name, _usuario.NombreCompleto)

        };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties()
            {

                AllowRefresh = true,
            };

            //await HttpContext.SignOutAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(claimsIdentity), authenticationProperties);

            return RedirectToAction("Index", "Home");
        }
    }
    }

