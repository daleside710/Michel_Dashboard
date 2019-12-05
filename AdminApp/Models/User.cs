using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdminApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [Table("usuarios_usu")]
    public class User : IdentityUser<int>
    {
        [Display(Name = "Name:")]
        [StringLength(255)]
        public string nombre_usu { get; set; }

        [Display(Name = "Surname:")]
        [StringLength(255)]
        public string apellidos_usu { get; set; }

        [Display(Name = "Role:")]
        [StringLength(255)]
        public string tipo_usu { get; set; }

        public int activo_usu { get; set; }

        public DateTime? fechaC_usu { get; set; }

        public int usuC_usu { get; set; }

        public DateTime? fechaM_usu { get; set; }

        public int? usuM_usu { get; set; }

        public int borrado_usu { get; set; }

        public DateTime? fechaUltAcceso_usu { get; set; }

        [Display(Name = "Country:")]
        [StringLength(255)]
        public string pais_usu { get; set; }

        public User()
        {
            fechaM_usu = DateTime.UtcNow;
            fechaC_usu = DateTime.UtcNow;
        }
    }

}
