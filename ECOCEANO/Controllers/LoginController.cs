using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECOCEANO.Data;
using ECOCEANO.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECOCEANO.Controllers
{
    public class LoginController : Controller
    {
        private readonly EcoceanoContext con;

        public LoginController( EcoceanoContext _con)
        {
            this.con = _con;
        }
        //Cerrar Sesion 
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuarios usu)
        {

            if (ModelState.IsValid)
            {

                var usuario = await con.Usuario.FirstOrDefaultAsync(u => u.correo == usu.correo && u.password == usu.password);
                if (usuario == null)
                {
                    ModelState.AddModelError("ErrorLogin", "Las credenciales no son validas.");
                    return View("../Home/Login", usu);

                }
                else
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usu.correo),
                    new Claim(ClaimTypes.Role, "Administrador"),
                };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Catalogos");
                }
            }
            else
            {
                ModelState.AddModelError("ErrorLogin", "Los campos son obligatorios");
                return View("../Home/Login", usu);
            }
        }
    }
}

