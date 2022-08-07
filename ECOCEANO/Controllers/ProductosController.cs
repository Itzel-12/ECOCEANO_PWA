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
    public class ProductosController : Controller
    {
            private readonly EcoceanoContext con;
            //constructor 
            public ProductosController(EcoceanoContext _con)
            {
                con = _con;
            }
            public async Task<IActionResult> IndexAsync()
            {
                var productosContexto = con.Producto.Include(l => l.categoria);
                return View(await productosContexto.ToListAsync());

            }

            // GET: /<controller>/
            public IActionResult Registro()
            {
                ViewData["CategoriaID"] = new SelectList(con.Categoria, "ID", "nombre");
     



                return View();
            }
            //Metodo para registrar
            [HttpPost]
            public async Task<IActionResult> Add([Bind("ID,nombre,descripcion,Stock,Precio,Imagen,CategoriaID,estatus")] Productos producto)
            {
                if (ModelState.IsValid)
                {
                    //Datos de prueba
                    producto.estatus = true;
                    con.Add(producto);
                    //Guardamos cambios 
                    await con.SaveChangesAsync();
                    //Dirigimos al Index.
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("ErrorProducto", "Los datos no fueron registrados");
                    return View("../Productos", producto);
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
                var producto = await con.Producto
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (producto == null)
                {
                    return NotFound();
                }
                return View(producto);
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
                var producto = await con.Producto.FindAsync(id);
                //Eliminando
                con.Producto.Remove(producto);
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
                    var producto = await con.Producto
                    .FindAsync(id); //se busca en la base de datos la editorial
                    if (producto == null)//si no se encuentra
                    {
                        return NotFound();
                    }
                    else // Si se encuentra 
                    {
                        return View(producto); //Se envia la editorial a la vista
                    }
                }
            }
            /**
             * *Funcion para guardar cambios en la base de datos
             */
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult>
                Editar(int id, [Bind("ID, nombre, descripcion")] Productos productos)
            {
                if (id != productos.ID)
                {
                    return NotFound();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            con.Update(productos);
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
                        return View(productos);
                    }
                }
            }
            /*
             * Validar si existe una editorial 
             */
            [AcceptVerbs("GET", "POST")]
            public IActionResult ExisteProducto(string nombre)
            {
                var validarProducto = con.Producto.FirstOrDefault
                                         (e => e.nombre == nombre);

                if (validarProducto != null)
                {
                    return Json($"La Producto {nombre} ya se encuentra registrada,");
                }
                return Json(true);
             } 
    }
}
