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
    public class DetallePedidoController : Controller
    {
        private readonly EcoceanoContext con;
        //constructor 
        public DetallePedidoController(EcoceanoContext _con)
        {
            con = _con;
        }

        // GET: /<controller>/

        public async Task<IActionResult> IndexAsync()
        {
            var DetallePedidosContexto = con.DetallePedidos.Include(l => l.producto).Include(l => l.pedido);
            return View(await DetallePedidosContexto.ToListAsync());

        }
        public IActionResult Registro()
        {
            ViewData["ProductoID"] = new SelectList(con.Producto, "ID", "nombre");
            ViewData["PedidoID"] = new SelectList(con.Pedido, "ID");



            return View();
        }

        //Metodo para registrar
        [HttpPost]
        public async Task<IActionResult> Add([Bind("ID,estatus,cantidad,montoTotal,descripcion,ProductoID,PedidoID")] DetallesPedidos detallepedidos)
        {
            if (ModelState.IsValid)
            {
                //Datos de prueba
                detallepedidos.estatus = true;
                con.Add(detallepedidos);
                //Guardamos cambios 
                await con.SaveChangesAsync();
                //Dirigimos al Index.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("ErrorProducto", "Los datos no fueron registrados");
                return View("../DetallesPedidos", detallepedidos);
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
            var detallepedidos = await con.DetallePedidos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detallepedidos == null)
            {
                return NotFound();
            }
            return View(detallepedidos);
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
            var detallepedidos = await con.DetallePedidos.FindAsync(id);
            //Eliminando
            con.DetallePedidos.Remove(detallepedidos);
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
                var detallepedidos = await con.DetallePedidos
                .FindAsync(id); //se busca en la base de datos la editorial
                if (detallepedidos == null)//si no se encuentra
                {
                    return NotFound();
                }
                else // Si se encuentra 
                {
                    return View(detallepedidos); //Se envia la editorial a la vista
                }
            }
        }
        /**
         * *Funcion para guardar cambios en la base de datos
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Editar(int id, [Bind("ID")] DetallesPedidos detallepedidos)
        {
            if (id != detallepedidos.ID)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        con.Update(detallepedidos);
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
                    return View(detallepedidos);
                }
            }
        }

    }
}
