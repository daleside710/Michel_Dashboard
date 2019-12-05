using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class PasswordEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])).{6,}$", ErrorMessage = "Las contraseñas deben tener al menos 6 caracteres y contener 3 de 4 de los siguientes: mayúscula (A-Z), minúscula (a-z), número (0-9) y carácter especial (por ejemplo! @ # $% ^ & *)")]
        [DataType(DataType.Password)]
        [Display(Name = "Nuevo contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repetir contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
