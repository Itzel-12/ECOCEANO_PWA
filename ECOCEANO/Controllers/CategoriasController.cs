using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECOCEANO.Data;
using ECOCEANO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECOCEANO.Controllers
{
    public class CategoriasController : Controller
    {
        //1. Agregar el contexto
        private readonly EcoceanoContext con;

        //2. Constructor 
        public CategoriasController(EcoceanoContext _con)
        {
            con = _con;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await con.Categoria.ToListAsync());
        }

        // GET: /<controller>/
        public IActionResult Registro()
        {
            return View();
        }
        //Metodo para registrar
        [HttpPost]
        public async Task<IActionResult> Add([Bind("ID,nombre,estatus")] Categorias Categoria)
        {
            if (ModelState.IsValid)
            {
                //Datos de prueba
                Categoria.estatus = true;
                con.Add(Categoria);
                //Guardamos cambios 
                await con.SaveChangesAsync();
                //Dirigimos al Index.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("ErrorCategorias", "Los datos no fueron registrados");
                return View("../Categorias", Categoria);
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
            var Categoria = await con.Categoria
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Categoria == null)
            {
                return NotFound();
            }
            return View(Categoria);
        }
        /*
         * FUNCION PARA ELIMINAR UN EDITORIAL 
         * 
         */
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            try
            {
                //Buscar la editorial a eliminar 
                var Categoria = await con.Categoria.FindAsync(id);
                //Eliminando
                con.Categoria.Remove(Categoria);
                //Guardar cambios en BD 
                await con.SaveChangesAsync();
                //Enviar al Index de editoriales
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Errorcategoria", "La categoria no se puede eliminar porque esta en uso");
                return RedirectToAction(nameof(Index));
            }
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
                var Categoria = await con.Categoria
                .FindAsync(id); //se busca en la base de datos la editorial
                if (Categoria == null)//si no se encuentra
                {
                    return NotFound();
                }
                else // Si se encuentra 
                {
                    return View(Categoria); //Se envia la editorial a la vista
                }
            }
        }
        /**
         * *Funcion para guardar cambios en la base de datos
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Editar(int id, [Bind("ID, nombre")] Categorias Categoria)
        {
            if (id != Categoria.ID)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        con.Update(Categoria);
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
                    return View(Categoria);
                }
            }
        }
        /*
         * Validar si existe una editorial 
         */
        [AcceptVerbs("GET", "POST")]
        public IActionResult ExisteCategoria(string nombre)
        {
            var validarCategoria = con.Categoria.FirstOrDefault
                                     (e => e.nombre == nombre);

            if (validarCategoria != null)
            {
                return Json($"La Categoria {nombre} ya se encuentra registrada,");
            }
            return Json(true);
        }
    }
}

