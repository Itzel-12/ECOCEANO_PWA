using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Models
{
    public class Pedidos
    {
        internal DateTime fechaRegistro;
        public Pedidos()
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

      

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [StringLength(200, ErrorMessage = "Se excedio la longitud maxima permitida.")]
        public String descripcion { get; set; }
        
        public Boolean estatus { get; set; }
        public DateTime fecharegistro { get; set; }

        [Display(Name = "Usuario")]
        public int UsuarioID { get; set; }
        public Usuarios usuario { get; set; }


    }
}
