using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class ParticipationEnviarResendViewModel
    {
        public string url_par { get; set; }

        public string adjunto_par { get; set; }    
    }
    public class ParticipationEnviarResendViewModel_info
    {
        public string url_par { get; set; }
        public string taller { get; set; }
        public string nombre_par { get; set; }
        public string apellidos_par { get; set; }
        public string email_par { get; set; }
        public string nacionalidad_par { get; set; }
        public string telefono_par { get; set; }
        public int id_tall { get; set; }

        public string dni_par { get; set; }

        public string direccion_par  { get; set; }
        public string localidad_par { get; set; }
        public string codigopostal_par { get; set; }
        public string provincia_par { get; set; }

        public DateTime fecha_adj_factura { get; set; }
        public DateTime fecha_adj_datos { get; set; }
    }
}
