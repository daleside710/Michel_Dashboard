using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class WorkshopEditViewModel
    {
        public int id_tall { get; set; }

        [Display(Name = "Razón social")]
        [Required(ErrorMessage = "Razón social es obligatorio")]
        [StringLength(255)]
        public string razonsocial_tall { get; set; }

        [Display(Name = "Alias")]
        [Required(ErrorMessage = "Alias es obligatorio")]
        [StringLength(255)]
        public string alias_tall { get; set; }

        [Display(Name = "LC")]
        [StringLength(255)]
        public string LC_tall { get; set; }

        [Display(Name = "HPDV")]
        [StringLength(255)]
        public string HPDV_tall { get; set; }

        [Display(Name = "ENSEÑA")]
        [Required(ErrorMessage = "ENSEÑA es obligatorio")]
        [StringLength(255)]
        public string ENSENA_tall { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Dirección es obligatorio")]
        [StringLength(255)]
        public string direccion_tall { get; set; }

        [Display(Name = "Población")]
        [Required(ErrorMessage = "Población es obligatorio")]
        [StringLength(255)]
        public string poblacion_tall { get; set; }

        [Display(Name = "CP")]
        [Required(ErrorMessage = "CP es obligatorio")]
        [StringLength(255)]
        public string cp_tall { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Provincia es obligatorio")]
        [StringLength(255)]
        public string provincia_tall { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "Pais es obligatorio")]
        [StringLength(255)]
        public string pais_tall { get; set; }

        [Display(Name = "Desde")]
        [Required(ErrorMessage = "Desde es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? fechaDesde_tall { get; set; }

        [Display(Name = "Región")]
        [Required(ErrorMessage = "Región es obligatorio")]
        [StringLength(255)]
        public string REGION_tall { get; set; }
    }
}
