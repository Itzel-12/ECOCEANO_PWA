using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Models
{
    public class DetallesPedidos
    {
        internal DateTime fechaRegistro;
        public DetallesPedidos()
        {
            //Valores por Default 
            this.descripcion = "Descripcion";
            this.estatus = true;
        }
        //Propiedades 
        //Propiedades 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
     
        public int Cantidad { get; set; }

        [Display(Name = "MontoTotal")]
        [RegularExpression(@"^\d+(\\\\.\\\\d)?$", ErrorMessage = "0000.0000")]
        [Range(0.1, 200000)]
        public decimal MontoTotal { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [StringLength(200, ErrorMessage = "Se excedio la longitud maxima permitida.")]
        public String descripcion { get; set; }
        public Boolean estatus { get; set; }

        [Display(Name = "Producto")]
        public int ProductoID { get; set; }
        public Productos producto { get; set; }

        [Display(Name = "Pedido")]
        public int PedidoID { get; set; }
        public Pedidos pedido { get; set; }
        public DateTime fecharegistro { get; set; }



    }
}
