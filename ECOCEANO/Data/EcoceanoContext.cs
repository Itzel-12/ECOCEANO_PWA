using ECOCEANO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Data
{
    public class EcoceanoContext : DbContext
    {
            public EcoceanoContext(DbContextOptions<EcoceanoContext> options) : base(options)
            {

            }
            public DbSet<Usuarios> Usuario { get; set; }
            public DbSet<Productos> Producto { get; set; }
            public DbSet<Categorias> Categoria { get; set; }
            public DbSet<Pedidos> Pedido { get; set; }
            public DbSet<Ventas> Venta { get; set; }
            public DbSet<DetallesVentas> DetalleVentas { get; set; }
            public DbSet<DetallesPedidos> DetallePedidos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                options.UseMySQL("server=localhost; database=ecoceanodb; user=root; password =;");
            }
        
    }
}
