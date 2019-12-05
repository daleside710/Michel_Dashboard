using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class ParticipationValidarEditViewModel
    {
        public int id_par { get; set; }

        [Display(Name = "Premio:")]
        public string producto { get; set; }
        
    }
}
