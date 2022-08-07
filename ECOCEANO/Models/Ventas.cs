using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Models
{
    public class Ventas
    {
        internal DateTime fechaRegistro;

        public Ventas()
        {
            //Valores por Default 
            this.estatus = true;
        }
        //Propiedades 
        //Propiedades 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        public int Articulos { get; set; }

        [Display(Name = "MontoTotal")]
        [RegularExpression(@"^\d+(\\\\.\\\\d)?$", ErrorMessage = "0000.0000")]
        [Range(0.1, 200000)]
        public decimal MontoTotal { get; set; }
        public Boolean estatus { get; set; }
        public DateTime fecharegistro { get; set; }

        [Display(Name = "Productos")]
        public int ProductoID { get; set; }
        public Productos Producto { get; set; }



    }
}
