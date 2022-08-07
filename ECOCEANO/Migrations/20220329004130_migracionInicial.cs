using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ECOCEANO.Migrations
{
    public partial class migracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(maxLength: 100, nullable: false),
                    estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(nullable: true),
                    pApellido = table.Column<string>(nullable: true),
                    sApellido = table.Column<string>(nullable: true),
                    correo = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    fecharegistro = table.Column<DateTime>(nullable: false),
                    rol = table.Column<string>(nullable: true),
                    estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(maxLength: 200, nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(nullable: false),
                    Imagen = table.Column<string>(maxLength: 200, nullable: false),
                    estatus = table.Column<bool>(nullable: false),
                    CategoriaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(maxLength: 200, nullable: false),
                    estatus = table.Column<bool>(nullable: false),
                    fecharegistro = table.Column<DateTime>(nullable: false),
                    UsuarioID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pedido_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(nullable: false),
                    estatus = table.Column<bool>(nullable: false),
                    ProductoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Articulos = table.Column<int>(nullable: false),
                    MontoTotal = table.Column<decimal>(nullable: false),
                    estatus = table.Column<bool>(nullable: false),
                    fecharegistro = table.Column<DateTime>(nullable: false),
                    ProductoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Venta_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(nullable: false),
                    MontoTotal = table.Column<decimal>(nullable: false),
                    descripcion = table.Column<string>(maxLength: 200, nullable: false),
                    estatus = table.Column<bool>(nullable: false),
                    ProductoID = table.Column<int>(nullable: false),
                    PedidoID = table.Column<int>(nullable: false),
                    fecharegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_Pedido_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedido",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_PedidoID",
                table: "DetallePedidos",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_ProductoID",
                table: "DetallePedidos",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoID",
                table: "DetalleVentas",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_UsuarioID",
                table: "Pedido",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaID",
                table: "Producto",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_ProductoID",
                table: "Venta",
                column: "ProductoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePedidos");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
