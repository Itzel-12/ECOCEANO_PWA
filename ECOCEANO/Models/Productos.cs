using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Models
{
    public class Productos
    {
        public Productos()
        {
            //Valores por Default 
            this.nombre = "Producto";
            this.descripcion = "Descripcion";
            this.estatus = true;
        }
        //Propiedades 
        //Propiedades 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo Producto es obligatorio")]
        [StringLength(100, ErrorMessage = "Se excedio la longitud maxima permitida.")]
        [Remote(action: "ExisteProducto", controller: "Productos")]

        public String nombre { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [StringLength(200, ErrorMessage = "Se excedio la longitud maxima permitida.")]
        public String descripcion { get; set; }
        public int Stock { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+(\\\\.\\\\d)?$", ErrorMessage = "0000.0000")]
        [Range(0.1, 200000)]
        public decimal Precio { get; set; }
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "El campo Imagen es obligatorio")]
        [StringLength(200, ErrorMessage = "Se excedio la longitud maxima permitida.")]
        public String Imagen { get; set; }
        public Boolean estatus { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }
        public Categorias categoria { get; set; }


    }
}
