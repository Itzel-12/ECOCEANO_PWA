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
    public class VentasController : Controller
    {
        private readonly EcoceanoContext con;
        //constructor 
        public VentasController(EcoceanoContext _con)
        {
            con = _con;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var ventaContexto = con.Venta.Include(l => l.Producto);
            return View(await ventaContexto.ToListAsync());
           
        }

        // GET: /<controller>/
        public IActionResult Registro()
        {
            ViewData["ProductoID"] = new SelectList(con.Producto, "ID", "nombre");



            return View();
        }


        //Metodo para registrar
        [HttpPost]
        public async Task<IActionResult> Add([Bind("ID,Articulos,MontoTotal,estatus, ProductoID")] Ventas venta)
        {
            if (ModelState.IsValid)
            {
                venta.fecharegistro = DateTime.Now;
                //Datos de prueba
                venta.estatus = true;
                con.Add(venta);
                //Guardamos cambios 
                await con.SaveChangesAsync();
                //Dirigimos al Index.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("ErrorProducto", "Los datos no fueron registrados");
                return View("../Ventas", venta);
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
            var venta = await con.Venta
                .FirstOrDefaultAsync(m => m.ID == id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
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
            var venta = await con.Venta.FindAsync(id);
            //Eliminando
            con.Venta.Remove(venta);
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
                var venta = await con.Venta
                .FindAsync(id); //se busca en la base de datos la editorial
                if (venta == null)//si no se encuentra
                {
                    return NotFound();
                }
                else // Si se encuentra 
                {
                    return View(venta); //Se envia la editorial a la vista
                }
            }
        }
        /**
         * *Funcion para guardar cambios en la base de datos
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Editar(int id, [Bind("ID")] Ventas ventas)
        {
            if (id != ventas.ID)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        con.Update(ventas);
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
                    return View(ventas);
                }
            }
        }
    
    }
}


