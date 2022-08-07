using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Models
{
    public class DetallesVentas
    {

        public DetallesVentas()
        {
            //Valores por Default 
            this.estatus = true;
        }
        //Propiedades 
        //Propiedades 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Cantidad { get; set; }
        public Boolean estatus { get; set; }
     
        [Display(Name = "Producto")]
        public int ProductoID { get; set; }
        public Productos producto { get; set; }
       


    }
}
