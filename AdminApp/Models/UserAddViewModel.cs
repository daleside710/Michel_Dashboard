using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class UserAddViewModel
    {
        
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

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña es obligatorio")]
        public string Password { get; set; }

        [Display(Name = "Pais")]
        [StringLength(255)]
        [Required(ErrorMessage = "Pais es obligatorio")]
        public string pais_usu { get; set; }
    }
}
