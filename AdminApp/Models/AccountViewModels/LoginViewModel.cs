using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Introduzca Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduzca Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
