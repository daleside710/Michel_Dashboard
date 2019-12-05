using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    [Table("regalosalert_ale")]
    public class Regalosalert
    {
        [Key]
        public int id_ale { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string producto_ale { get; set; }

        public int stock_ale { get; set; }

        public int isGrupo_ale { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string talla_ale { get; set; }        
    }
}
