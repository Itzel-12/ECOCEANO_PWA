using ECOCEANO.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Services
{
    public class PayPalPaymentService
    {
        public static Payment CreatePayment(string baseUrl, string intent, List<CartItem> cart)
        {
            var apiContext = PayPalConfiguration.GetAPIContext();
            var payment = new Payment()
            {
                intent = intent,
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(cart),
                redirect_urls = GetReturnUrls(baseUrl, intent)
            };
            var createdPayment = payment.Create(apiContext);
            return createdPayment;
        }

        private static List<Transaction> GetTransactionsList(List<CartItem> cart) //recibe carrito de compras en estructura de sesion 
        {
            decimal subt = 0;
            decimal tasaImpuesto = 20;
            decimal envio = 30;
            decimal totalProducto = 0;

            List<Item> items = new List<Item>(); //crea lista de articulos Paypal
            foreach (var item in cart) // Ayuda a recorrer mi carrito de sesion 
            {
                items.Add( // Se encaarga de agregar cada articulo a la lista 
                    new Item() //Nuevo articulo de Paypal
                    {
                        name = item.Producto.nombre,
                        currency = "MXN",
                        price = item.Producto.Precio.ToString(),
                        quantity = item.Quantity.ToString(),
                        sku = item.Producto.ID.ToString()
                    });
                totalProducto = item.Quantity * item.Producto.Precio; //calculo el total por producto
                subt += totalProducto; //subt=subt + totalProducto; --Acumulo el total de todos los productos
            }
            ItemList itemList = new ItemList();
            itemList.items = items; //Guardo mi lista de articulos 

            var details = new Details() //Detalles de la compra: tasa de impuesto, envio y subtotal
            {
                tax = tasaImpuesto.ToString(),
                shipping = envio.ToString(),
                subtotal = subt.ToString()
            };

            decimal total = tasaImpuesto + envio + subt;
            var amount = new Amount
            {
                currency = "MXN",
                total = total.ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();
            transactionList.Add(

                new Transaction()
                {
                    description = "Compra en TECNO IERF.com",
                    invoice_number = GetRandomInvoiceNumber(),
                    amount = amount,
                    item_list = itemList
                }
            );
            return transactionList;

        }
        public static Payment ExecutePayment(string paymentId, string payerId)
        {
            Console.WriteLine("ExecutePayment");
            var apiContext = PayPalConfiguration.GetAPIContext();

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };


            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }
        private static RedirectUrls GetReturnUrls(string baseUrl, string intent)
        {
            Console.WriteLine("GetReturnUrls");
            var returnUrl = intent == "sale" ? "/Payment/PaymentSuccessful" : "/Home/AuthorizeSuccessful";


            return new RedirectUrls()
            {
                cancel_url = baseUrl + "/Home/PaymentCancelled",
                return_url = baseUrl + returnUrl
            };
        }

        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }

    }
}
