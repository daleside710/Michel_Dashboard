using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class ParticipationEditViewModel
    {
        [Display(Name = "ID:")]
        public int id_par { get; set; }

        [Display(Name = "Factura:")]
        public string adjunto1_par { get; set; }

        [Display(Name = "Factura:")]
        public string adjunto2_par { get; set; }

        public string adjunto3_par { get; set; }

        public string adjunto4_par { get; set; }

        public string adjunto5_par { get; set; }


        [Display(Name = "País:")]        
        public string pais_par { get; set; }

        [Display(Name = "HPDV:")]
        public string hpdv_tall { get; set; }

        public string ensena_tall { get; set; }

        [Display(Name = "Desde:")]       
        [DataType(DataType.Date)]
        public DateTime? fechaDesde_tall { get; set; }

        [Display(Name = "Razón:")]
        public string razonsocial_tall { get; set; }

        public string direccion_tall { get; set; }

        public string poblacion_tall { get; set; }

        [Display(Name = "Registro:")]
        public DateTime? registrofecha_par { get; set; }

        [Display(Name = "Hora:")]
        public TimeSpan? registrohora_par { get; set; }

        [Display(Name = "Apellidos:")]
        public string apellidos_par { get; set; }

        [Display(Name = "Nombre:")]
        public string nombre_par { get; set; }

        [Display(Name = "Comercial:")]
        public string comercial_par { get; set; }

        [Display(Name = "Dirección:")]
        public string direccion_par { get; set; }

        [Display(Name = "Localidd:")]
        public string localidad_par { get; set; }

        [Display(Name = "Provincia:")]
        public string provincia_par { get; set; }

        [Display(Name = "CP:")]
        public string codigopostal_par { get; set; }

        [Display(Name = "Vehículo:")]
        public string tipovehiculo_par { get; set; }

        [Display(Name = "Neumático:")]
        public string Numero_ruedas { get; set; }

        [Display(Name = "Regalo:")]
        public string regalo { get; set; }

        [Display(Name = "Url:")]
        public string url_par { get; set; }

        [Display(Name = "Email:")]
        [StringLength(255)]
        public string email_par { get; set; }

        [Display(Name = "DNI/CIF:")]
        [StringLength(255)]
        public string dni_par { get; set; }

        [Display(Name = "Teléfono:")]
        [StringLength(255)]
        public string telefono_par { get; set; }

        [Display(Name = "Nº Factura #1:")]
        [StringLength(255)]
        public string factura1_par { get; set; }

        [Display(Name = "Nº Factura #2:")]
        [StringLength(255)]
        public string factura2_par { get; set; }

        [Display(Name = " Fecha Factura #1:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fechaCompra1_par { get; set; }

        [Display(Name = " Fecha Factura #2:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fechaCompra2_par { get; set; }

        [Display(Name = "Modelo Neumático:")]
        public string llanta { get; set; }

        [Display(Name = "Comentarios:")]
        public string motivo_adj_factura { get; set; }

        [Display(Name = "Envío:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fechaEnv_adj_factura { get; set; }

        [Display(Name = "Actualización:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fecha_adj_factura { get; set; }

        [Display(Name = "Comentarios:")]
        public string motivo_adj_datos { get; set; }

        [Display(Name = "Envío:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fechaEnv_adj_datos { get; set; }

        [Display(Name = "Actualización:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fecha_adj_datos { get; set; }

        [Display(Name = "Población:")]
        public string usuariolocalidad_par { get; set; } 

        [Display(Name = "Provincia:")]
        public string usuarioprovincia_par { get; set; }


        public List<SelectListItem> Llanta_Moto_List { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "ANAKEE ADVENTURE", Text = "ANAKEE ADVENTURE" },
            new SelectListItem { Value = "COMMANDER II", Text = "COMMANDER II" },
            new SelectListItem { Value = "PILOT POWER 3", Text = "PILOT POWER 3" },
            new SelectListItem { Value = "PILOT POWER 3 SCOOTER", Text = "PILOT POWER 3 SCOOTER" },
            new SelectListItem { Value = "PILOT ROAD 4 SCOOTER", Text = "PILOT ROAD 4 SCOOTER" },
            new SelectListItem { Value = "POWER RS", Text = "POWER RS" },
            new SelectListItem { Value = "POWER RS+", Text = "POWER RS+" },
            new SelectListItem { Value = "ROAD 5", Text = "ROAD 5" },
            new SelectListItem { Value = "ROAD 5 TRAIL", Text = "ROAD 5 TRAIL" },
            new SelectListItem { Value = "SCORCHER 11", Text = "SCORCHER 11" },
            new SelectListItem { Value = "SCORCHER 11F", Text = "SCORCHER 11F" },
            new SelectListItem { Value = "SCORCHER 11T", Text = "SCORCHER 11T" },
            new SelectListItem { Value = "SCORCHER 21", Text = "SCORCHER 21" },
            new SelectListItem { Value = "SCORCHER 31", Text = "SCORCHER 31" },
            new SelectListItem { Value = "SCORCHER 32", Text = "SCORCHER 32" },
        };

        public string TamanoRueda { get; set; }

        [Display(Name = "Medida Llanta:")]
        public string medidallanta_par { get; set; }

        public List<SelectListItem> M_Llanta { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "12", Text = "12" },
            new SelectListItem { Value = "13", Text = "13" },
            new SelectListItem { Value = "14", Text = "14" },
            new SelectListItem { Value = "15", Text = "15" },
            new SelectListItem { Value = "16", Text = "16" },
            new SelectListItem { Value = "17", Text = "17" },
            new SelectListItem { Value = "18", Text = "18" },
            new SelectListItem { Value = "19", Text = "19" },
            new SelectListItem { Value = "20", Text = "20" },
            new SelectListItem { Value = "21", Text = "21" },
            new SelectListItem { Value = "22", Text = "22" },
        };

        [Display(Name = "Nº Llanta:")]
        public string numllantas_par { get; set; }

        public List<SelectListItem> N_Llanta { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "motox1", Text = "motox1" },
            new SelectListItem { Value = "motox2", Text = "motox2" },
            new SelectListItem { Value = "cochex2", Text = "cochex2"  },
            new SelectListItem { Value = "cochex4", Text = "cochex4"  },
        };

        [DataType(DataType.DateTime)]
        public DateTime? fechaValidacion_par { get; set; }

        public string motivo_par { get; set; }

        //public string comentarios_par { get; set; }

        [DataType(DataType.Date)]
        public DateTime? rechazoFecha_par { get; set; }

        public TimeSpan? rechazoHora_par { get; set; }

        public string fecha_caducidad { get; set; }

        public int? id_tall { get; set; }


        public List<SelectListItem> N_ProvinciaPT { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "Açores", Text = "Açores" },
            new SelectListItem { Value = "Aveiro", Text = "Aveiro" },
            new SelectListItem { Value = "Beja", Text = "Beja"  },
            new SelectListItem { Value = "Braga", Text = "Braga"  },
            new SelectListItem { Value = "Bragança", Text = "Bragança"  },
            new SelectListItem { Value = "Castelo Branco", Text = "Castelo Branco"  },
            new SelectListItem { Value = "Coimbra", Text = "Coimbra"  },
            new SelectListItem { Value = "Évora", Text = "Évora"  },
            new SelectListItem { Value = "Faro", Text = "Faro"  },
            new SelectListItem { Value = "Guarda", Text = "Guarda"  },
            new SelectListItem { Value = "Leiria", Text = "Leiria"  },
            new SelectListItem { Value = "Lisboa", Text = "Lisboa"  },
            new SelectListItem { Value = "Madeira", Text = "Madeira"  },
            new SelectListItem { Value = "Portalegre", Text = "Portalegre"  },
            new SelectListItem { Value = "Porto", Text = "Porto"  },
            new SelectListItem { Value = "Santarém", Text = "Santarém"  },
            new SelectListItem { Value = "Setúbal", Text = "Setúbal"  },
            new SelectListItem { Value = "Viana do Castel", Text = "Viana do Castel"  },
            new SelectListItem { Value = "Vila Real", Text = "Vila Real"  },
            new SelectListItem { Value = "Viseu", Text = "Viseu"  },
        };
        public List<SelectListItem> N_ProvinciaES { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "Andorra", Text = "Andorra"  },
            new SelectListItem { Value = "Albacete", Text = "Albacete" },
            new SelectListItem { Value = "A Coruña", Text = "A Coruña" },
            new SelectListItem { Value = "Alicante", Text = "Alicante" },
            new SelectListItem { Value = "Almería", Text = "Almería"  },
            new SelectListItem { Value = "Araba", Text = "Araba"  },
            new SelectListItem { Value = "Asturias", Text = "Asturias"  },
            new SelectListItem { Value = "Ávila", Text = "Ávila"  },
            new SelectListItem { Value = "Badajoz", Text = "Badajoz"  },
            new SelectListItem { Value = "Illes Balears", Text = "Illes Balears"  },
            new SelectListItem { Value = "Barcelona", Text = "Barcelona"  },
            new SelectListItem { Value = "Bizkaia", Text = "Bizkaia"  },
            new SelectListItem { Value = "Burgos", Text = "Burgos"  },
            new SelectListItem { Value = "Cádiz", Text = "Cádiz"  },
            new SelectListItem { Value = "Cantabria", Text = "Cantabria"  },
            new SelectListItem { Value = "Castellón", Text = "Castellón"  },
            new SelectListItem { Value = "Ceuta", Text = "Ceuta"  },
            new SelectListItem { Value = "Ciudad Real", Text = "Ciudad Real"  },
            new SelectListItem { Value = "Córdoba", Text = "Córdoba"  },
            new SelectListItem { Value = "Cuenca", Text = "Cuenca"  },
            new SelectListItem { Value = "Granada", Text = "Granada"  },
            new SelectListItem { Value = "Guadalajara", Text = "Guadalajara"  },
            new SelectListItem { Value = "Gipuzkoa", Text = "Gipuzkoa"  },
            new SelectListItem { Value = "Huesca", Text = "Huesca"  },
            new SelectListItem { Value = "Huelva", Text = "Huelva"  },
            new SelectListItem { Value = "Jaén", Text = "Jaén"  },
            new SelectListItem { Value = "La Rioja", Text = "La Rioja"  },
            new SelectListItem { Value = "León", Text = "León"  },
            new SelectListItem { Value = "Lleida", Text = "Lleida"  },
            new SelectListItem { Value = "Lugo", Text = "Lugo"  },
            new SelectListItem { Value = "Las Palmas de Gran Canaria", Text = "Las Palmas de Gran Canaria"  },
            new SelectListItem { Value = "Madrid", Text = "Madrid"  },
            new SelectListItem { Value = "Málaga", Text = "Málaga"  },
            new SelectListItem { Value = "Melilla", Text = "Melilla"  },
            new SelectListItem { Value = "Murcia", Text = "Murcia"  },
            new SelectListItem { Value = "Navarra", Text = "Navarra"  },
            new SelectListItem { Value = "Ourense", Text = "Ourense"  },
            new SelectListItem { Value = "Palencia", Text = "Palencia"  },
            new SelectListItem { Value = "Pontevedra", Text = "Pontevedra"  },
            new SelectListItem { Value = "Salamanca", Text = "Salamanca"  },
            new SelectListItem { Value = "Santa Cruz de Tenerife", Text = "Santa Cruz de Tenerife"  },
            new SelectListItem { Value = "Segovia", Text = "Segovia"  },
            new SelectListItem { Value = "Sevilla", Text = "Sevilla"  },
            new SelectListItem { Value = "Soria", Text = "Soria"  },
            new SelectListItem { Value = "Tarragona", Text = "Tarragona"  },
            new SelectListItem { Value = "Teruel", Text = "Teruel"  },
            new SelectListItem { Value = "Toledo", Text = "Toledo"  },
            new SelectListItem { Value = "Valencia", Text = "Valencia"  },
            new SelectListItem { Value = "Valladolid", Text = "Valladolid"  },
            new SelectListItem { Value = "Zamora", Text = "Zamora"  },
            new SelectListItem { Value = "Zaragoza", Text = "Zaragoza"  },
            new SelectListItem { Value = "No Detallado", Text = "No Detallado"  },
        };
    }
}