using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECOCEANO.Data;
using ECOCEANO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECOCEANO.Controllers
{
    public class DetalleVentaController : Controller
    {

        private readonly EcoceanoContext con;
        //constructor 
        public DetalleVentaController(EcoceanoContext _con)
        {
            con = _con;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var detalleVentaContexto = con.DetalleVentas.Include(l => l.producto);
            return View(await detalleVentaContexto.ToListAsync());

        }


        // GET: /<controller>/
        public IActionResult Registro()
        {
            ViewData["ProductoID"] = new SelectList(con.Producto, "ID", "nombre");



            return View();
        }


        //Metodo para registrar
        [HttpPost]
        public async Task<IActionResult> Add([Bind("ID,estatus,cantidad,ProductoID")] DetallesVentas detalleventas)
        {
            if (ModelState.IsValid)
            {
                //Datos de prueba
                detalleventas.estatus = true;
                con.Add(detalleventas);
                //Guardamos cambios 
                await con.SaveChangesAsync();
                //Dirigimos al Index.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("ErrorProducto", "Los datos no fueron registrados");
                return View("../DetallesVentas", detalleventas);
            }

        }
        /* 
       * INVOCAR LA VISTA DE ELIMINAR
       * */
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var detalleventas = await con.DetalleVentas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detalleventas == null)
            {
                return NotFound();
            }
            return View(detalleventas);
        }
        /*
         * FUNCION PARA ELIMINAR UN EDITORIAL 
         * 
         */
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            //Buscar la editorial a eliminar 
            var detalleventas = await con.DetalleVentas.FindAsync(id);
            //Eliminando
            con.DetalleVentas.Remove(detalleventas);
            //Guardar cambios en BD 
            await con.SaveChangesAsync();
            //Enviar al Index de editoriales
            return RedirectToAction(nameof(Index));
        }

        /*
         * Invocar la vista Editar
         * localhost:5001/Editoriales/Editar/2
         */
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) // Si el id es nulo
            {
                return NotFound(); // se envia la pagina indicando que no se encuentra el id
            }
            else
            {
                var detalleventas = await con.DetalleVentas
                .FindAsync(id); //se busca en la base de datos la editorial
                if (detalleventas == null)//si no se encuentra
                {
                    return NotFound();
                }
                else // Si se encuentra 
                {
                    return View(detalleventas); //Se envia la editorial a la vista
                }
            }
        }
        /**
         * *Funcion para guardar cambios en la base de datos
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Editar(int id, [Bind("ID")] DetallesVentas detalleventas)
        {
            if (id != detalleventas.ID)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        con.Update(detalleventas);
                        await con.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(detalleventas);
                }
            }
        }

    }
}
