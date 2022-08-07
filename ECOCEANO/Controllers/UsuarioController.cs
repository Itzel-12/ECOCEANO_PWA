using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECOCEANO.Data;
using ECOCEANO.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECOCEANO.Controllers
{
    public class UsuarioController : Controller
    {
        //1. Agregar el contexto
        private readonly EcoceanoContext con;

        //2. Constructor 
        public UsuarioController(EcoceanoContext _con)
        {
            con = _con;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Metodo para registrar
        [HttpPost]
        public async Task<IActionResult> Add([Bind("nombre,pApellido,sApellido, correo, password, estatus")] Usuarios usu)
        {
            if (ModelState.IsValid)
            {
                //Datos de prueba
                usu.fecharegistro = DateTime.Now;
                usu.estatus = true;
                usu.rol = "cliente";
                //Agregamos el usuario 
                con.Add(usu);
                //Guardamos cambios 
                await con.SaveChangesAsync();
                //Dirigimos al Index.
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("Registro", "Home");
            //Metodo para eliminar 
            //Metodo para actualizar 
        }

    }
}

