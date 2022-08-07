using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECOCEANO.Data;
using ECOCEANO.Helpers;
using ECOCEANO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECOCEANO.Controllers
{
    public class CartController : Controller
    {
        private readonly EcoceanoContext _context;
        public CartController(EcoceanoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.count = cart.Count; //Numero de elementos en el carrito. 
                ViewBag.cart = cart;//Elementos del carrito
                ViewBag.total = cart.Sum(item => item.Producto.Precio * item.Quantity);//Calculo total
                ViewBag.cantidad = cart.Sum(item => item.Quantity);//Calculo la cantidad
            }

            return View(await _context.Producto.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await _context.Producto.FirstOrDefaultAsync(m => m.ID == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        public async Task<IActionResult> ShoppingCart()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.count = cart.Count; //Numero de elementos en el carrito. 
                ViewBag.cart = cart;//Elementos del carrito
                ViewBag.total = cart.Sum(item => item.Producto.Precio * item.Quantity);//Calculo total
                ViewBag.cantidad = cart.Sum(item => item.Quantity);//Calculo la cantidad
            }

            return View(await _context.Producto.ToListAsync());
        }
        public async Task<IActionResult> AddToCart(int? id)
        {
            Productos productos = new Productos();
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>(); //Creo mi carrito
                var producto = await _context.Producto.FirstOrDefaultAsync(m => m.ID == id);//Busco el producto y lo guardo.
                cart.Add(new CartItem { Producto = producto, Quantity = 1 });//Agrego el elemento al carrito, es decir el prorducto y la cantidad.
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);//El elemento del carrito lo guardo en la sesion. 
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    var producto = await _context.Producto.FirstOrDefaultAsync(m => m.ID == id);//Busco el producto y lo guardo.
                    cart.Add(new CartItem { Producto = producto, Quantity = 1 });//Agrego el elemento al carrito, es decir el prorducto y la cantidad.

                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);//El elemento del carrito
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, int quantity)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity = quantity;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("ShoppingCart");
        }
        public async Task<IActionResult> Remove(int? id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart.RemoveAt(index);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("ShoppingCart");
        }
        private int isExist(int? id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Producto.ID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }

}
