using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(255)]
        public string nombre_usu { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Apellidos es obligatorio")]
        [StringLength(255)]
        public string apellidos_usu { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Rol es obligatorio")]
        [StringLength(255)]
        public string tipo_usu { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email es obligatorio")]
        public string Email { get; set; }


        [Display(Name = "Pais")]
        [StringLength(255)]
        [Required(ErrorMessage = "Pais es obligatorio")]
        public string pais_usu { get; set; }
    }
}
