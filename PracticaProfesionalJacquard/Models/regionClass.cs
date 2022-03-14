using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PracticaProfesionalJacquard.Models
{
    public class regionClass
    {
        [Key]

        public int IdRegion { get; set; }

        [Required(ErrorMessage="No puede ser Null")]
        public string NumeroRegion { get; set; }
        [Required(ErrorMessage="No puede ser Null")]
        public string Nombre { get; set; }
        [Required(ErrorMessage="No puede ser Null")]
        public int AreaKm { get; set; }
        [Required(ErrorMessage="No puede ser Null")]
        public int Poblacion { get; set; }



    }
}
