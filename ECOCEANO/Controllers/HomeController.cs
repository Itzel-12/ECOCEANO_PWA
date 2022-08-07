using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECOCEANO.Models;

namespace ECOCEANO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tienda()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View(); //Retorna la vista que se llama Login 
        }
        public IActionResult Registro()
        {
            return View();
        }
        public IActionResult Creaturas()
        {
            return View();
        }
        public IActionResult QuienesSomos()
        {
            return View();
        }
        public IActionResult Descargas()
        {
            return View();
        }
        public IActionResult CreaturaDetalles()
        {
            return View();
        }
        
        public IActionResult CreaturaDetalle2()
        {
            return View();
        }
        public IActionResult CreaturaDetalle3()
        {
            return View();
        }
        public IActionResult CreaturaDetalles4()
        {
            return View();
        }
        public IActionResult CreaturaDetalles5()
        {
            return View();
        }
        public IActionResult CreaturaDetalles6()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            return View();
        }
        public IActionResult Login2()
        {
            return View(); //Retorna la vista que se llama Login 
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
