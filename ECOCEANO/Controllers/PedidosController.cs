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
    public class PedidosController : Controller
    {
        private readonly EcoceanoContext con;
        //constructor 
        public PedidosController(EcoceanoContext _con)
        {
            con = _con;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var pedidosContexto = con.Pedido.Include(l => l.usuario);
            return View(await pedidosContexto.ToListAsync());

        }

        // GET: /<controller>/
        public IActionResult Registro()
        {
            ViewData["UsuarioID"] = new SelectList(con.Usuario, "ID", "nombre");


            return View();
        }


        //Metodo para registrar
        [HttpPost]
        public async Task<IActionResult> Add([Bind("ID,estatus, descripcion, UsuarioID")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                pedidos.fecharegistro = DateTime.Now;
                //Datos de prueba
                pedidos.estatus = true;
                con.Add(pedidos);
                //Guardamos cambios 
                await con.SaveChangesAsync();
                //Dirigimos al Index.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("ErrorProducto", "Los datos no fueron registrados");
                return View("../Pedidos", pedidos);
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
            var pedidos = await con.Pedido
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pedidos == null)
            {
                return NotFound();
            }
            return View(pedidos);
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
            var pedidos = await con.Pedido.FindAsync(id);
            //Eliminando
            con.Pedido.Remove(pedidos);
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
                var pedidos = await con.Pedido
                .FindAsync(id); //se busca en la base de datos la editorial
                if (pedidos == null)//si no se encuentra
                {
                    return NotFound();
                }
                else // Si se encuentra 
                {
                    return View(pedidos); //Se envia la editorial a la vista
                }
            }
        }
        /**
         * *Funcion para guardar cambios en la base de datos
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Editar(int id, [Bind("ID")] Pedidos pedidos)
        {
            if (id != pedidos.ID)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        con.Update(pedidos);
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
                    return View(pedidos);
                }
            }
        }

    }
}
