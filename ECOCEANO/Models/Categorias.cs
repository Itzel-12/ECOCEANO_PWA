using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Models
{
    public class Categorias
    {
        //Constructor
        public Categorias()
        {
            //Valores por Default 
            this.nombre = "Categoria ";
            this.estatus = true;
        }
        //Propiedades 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo Categoria es obligatorio")]
        [StringLength(100, ErrorMessage = "Se excedio la longitud maxima permitida.")]
        [Remote(action: "ExisteCategoria", controller: "Categorias")]

        public String nombre { get; set; }
        public Boolean estatus { get; set; }

    }
}
