using System;
using System.Collections.Generic;
//Dependencia para todas las restricciones
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.API.Datos.Entidades
{
    public class VehicleType
    {
        public int Id { get; set; }


        [Display(Name ="Tipo de vehiculo.")]
        [MaxLength(50,ErrorMessage ="El campo{0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage ="El campo {0} es obligatorio.")] 
        public string Description { get; set; }
    }
}
