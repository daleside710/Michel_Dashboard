using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Options;

namespace AdminApp.Controllers
{
    [Authorize]
    public class ParticipacionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        private string uploadRootFolder;

        public ParticipacionesController(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            string[] lines = System.IO.File.ReadAllLines(@"config.txt");
            uploadRootFolder = lines[6].Replace("UPLOAD_URL: ", "");
        }

        // GET: Workshop
        public async Task<ActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var role = currentUser.tipo_usu;

            if (role == "2")
            {
                return Redirect("/");
            }


            /*
            ViewBag.total = _context.Participation.Count();
            ViewBag.pend = _context.Participation.Where(p => p.id_est.Equals(1) && p.solicitar_adjunto.Equals(0)).Count();
            ViewBag.modi = _context.Participation.Where(p => p.id_est.Equals(1) && p.solicitar_adjunto.Equals(1) && p.adjunto_adjunto.Equals(0)).Count();
            ViewBag.actu = _context.Participation.Where(p => p.id_est.Equals(1) && p.solicitar_adjunto.Equals(1) && p.adjunto_adjunto.Equals(1)).Count();
            ViewBag.vali = _context.Participation.Where(p => p.id_est.Equals(2)).Count();
            ViewBag.recha = _context.Participation.Where(p => p.id_est.Equals(3)).Count();
            */

            //20191126
            var country = currentUser.pais_usu;
            if (country == "0")
            {
                country = "ES";
            }
            else if (country == "1")
            {
                country = "PT";
            }
            else
            {
                country = "";
            }
            if (country == "")
            {
                ViewBag.total = _context.Participation.Count();
                ViewBag.pend = _context.Participation.Where(p => p.id_est.Equals(1) && p.solicitar_adjunto.Equals(0)).Count();
                ViewBag.modi = _context.Participation.Where(p => p.id_est.Equals(1) && p.solicitar_adjunto.Equals(1) && p.adjunto_adjunto.Equals(0)).Count();
                ViewBag.actu = _context.Participation.Where(p => p.id_est.Equals(1) && p.solicitar_adjunto.Equals(1) && p.adjunto_adjunto.Equals(1)).Count();
                ViewBag.vali = _context.Participation.Where(p => p.id_est.Equals(2)).Count();
                ViewBag.recha = _context.Participation.Where(p => p.id_est.Equals(3)).Count();
            }
            else
            {
                ViewBag.total = _context.Participation.Where(p => p.pais_par.Equals(country)).Count();
                ViewBag.pend = _context.Participation.Where(p => p.id_est.Equals(1) && p.pais_par.Equals(country) && p.solicitar_adjunto.Equals(0)).Count();
                ViewBag.modi = _context.Participation.Where(p => p.id_est.Equals(1) && p.pais_par.Equals(country) && p.solicitar_adjunto.Equals(1) && p.adjunto_adjunto.Equals(0)).Count();
                ViewBag.actu = _context.Participation.Where(p => p.id_est.Equals(1) && p.pais_par.Equals(country) && p.solicitar_adjunto.Equals(1) && p.adjunto_adjunto.Equals(1)).Count();
                ViewBag.vali = _context.Participation.Where(p => p.id_est.Equals(2) && p.pais_par.Equals(country)).Count();
                ViewBag.recha = _context.Participation.Where(p => p.id_est.Equals(3) && p.pais_par.Equals(country)).Count();            
            }
            /* FIN 20191126 */
        

            return View();
        }
        public IActionResult DownloadFile(int id, string filename)
        {
            string folder = Path.Combine(uploadRootFolder, id.ToString());
            string fullPath = Path.Combine(folder, filename);

            if (System.IO.File.Exists(fullPath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
                return File(fileBytes, "application/force-download", filename);
            }

            return View("Error");

            //string[] lines = System.IO.File.ReadAllLines(@"config.txt");
            //byte[] fileBytes = System.IO.File.ReadAllBytes(lines[2].Replace("URL_BASE: ", "") + "/" + id + "/" + filename);

            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
        public IActionResult Edit(int id)
        {
            string[] lines = System.IO.File.ReadAllLines(@"config.txt");

            Participation participation = _context.Participation.Where(x => x.id_par.Equals(id)).FirstOrDefault();

            ParticipationEditViewModel model = new ParticipationEditViewModel();

            //Group 2
            model.id_par = participation.id_par;
            ViewBag.url_par = participation.url_par;
            if (participation.adjunto1_par == null)
            {
                model.adjunto1_par = "";
            }
            else
            {
                model.adjunto1_par = participation.adjunto1_par.Replace(participation.url_par + "_1_", "");
            }

            if (participation.adjunto2_par == null)
            {
                model.adjunto2_par = "";
            }
            else
            {
                model.adjunto2_par = participation.adjunto2_par.Replace(participation.url_par + "_2_", "");
            }
            if (participation.adjunto3_par == null)
            {
                model.adjunto3_par = "";
            }
            else
            {
                model.adjunto3_par = participation.adjunto3_par.Replace(participation.url_par + "_3_", "");
            }
            if (participation.adjunto4_par == null)
            {
                model.adjunto4_par = "";
            }
            else
            {
                model.adjunto4_par = participation.adjunto4_par.Replace(participation.url_par + "_4_", "");
            }
            if (participation.adjunto5_par == null)
            {
                model.adjunto5_par = "";
            }
            else
            {
                model.adjunto5_par = participation.adjunto5_par.Replace(participation.url_par + "_5_", "");
            }
            ViewBag.url = lines[2].Replace("URL_BASE: ", "");
            model.pais_par = participation.pais_par;

            // Group 3
            var id_tall = participation.id_tall;
            Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).FirstOrDefault();
            var taller = workshop.razonsocial_tall + "(" + workshop.direccion_tall + ", " + workshop.poblacion_tall + ", " + workshop.poblacion_tall + ", " + workshop.cp_tall + ", " + workshop.provincia_tall + ")";

            ViewBag.taller = taller;

            if (workshop != null)
            {
                model.hpdv_tall = workshop.HPDV_tall;
                model.ensena_tall = workshop.ENSENA_tall;
                model.fechaDesde_tall = workshop.fechaDesde_tall;
                model.razonsocial_tall = workshop.razonsocial_tall;
                model.direccion_tall = workshop.direccion_tall;
                model.poblacion_tall = workshop.poblacion_tall;
            }
            else
            {
                model.hpdv_tall = "";
                model.ensena_tall = "";
                model.fechaDesde_tall = null;
                model.razonsocial_tall = "";
                model.direccion_tall = "";
                model.poblacion_tall = "";
            }

            // Group 4
            model.registrofecha_par = participation.registrofecha_par;
            model.registrohora_par = participation.registrohora_par;
            model.nombre_par = participation.nombre_par;
            model.apellidos_par = participation.apellidos_par;

            if (participation.comercial_par == 1)
            {
                model.comercial_par = "Acepta";
            }
            else
            {
                model.comercial_par = "No acepta";
            }

            // Group 5
            model.direccion_par = participation.direccion_par;
            model.localidad_par = participation.localidad_par;
            model.provincia_par = participation.provincia_par;
            model.codigopostal_par = participation.codigopostal_par;

            // Group 6
            model.tipovehiculo_par = participation.tipovehiculo_par;
            ViewBag.tipovehiculo_par = participation.tipovehiculo_par;
            model.Numero_ruedas = participation.Numero_ruedas;
            var id_regalo = participation.premioSelFrnt_par;
            Regalo regalo = _context.Regalo.Where(x => x.id_regalo.Equals(id_regalo)).FirstOrDefault();
            if (regalo != null)
            {
                if (participation.valorpremio_par == null)
                {
                    model.regalo = regalo.producto;
                }
                else
                {
                    model.regalo = participation.valorpremio_par;
                }
            }
            else
            {
                model.regalo = "";
            }

            model.url_par = lines[1].Replace("URL: ", "") + participation.url_par;

            // Group 7
            model.motivo_adj_factura = participation.motivo_adj_factura;
            model.fecha_adj_factura = participation.fecha_adj_factura;
            model.fechaEnv_adj_factura = participation.fechaEnv_adj_factura;

            model.motivo_adj_datos = participation.motivo_adj_datos;
            model.fecha_adj_datos = participation.fecha_adj_datos;
            model.fechaEnv_adj_datos = participation.fechaEnv_adj_datos;


            // Right side
            model.email_par = participation.email_par.ToLower();
            model.dni_par = participation.dni_par;
            model.telefono_par = participation.telefono_par;
            model.factura1_par = participation.factura1_par;
            model.factura2_par = participation.factura2_par;
            model.fechaCompra1_par = participation.fechaCompra1_par;
            model.fechaCompra2_par = participation.fechaCompra2_par;

            //SITCOM 20191025
            model.usuariolocalidad_par = participation.usuariolocalidad_par;
            model.usuarioprovincia_par = participation.usuarioprovincia_par;

            model.llanta = participation.llanta;
            ViewBag.llanta = _context.Llantas.OrderBy(l => l.orden_marca).ThenBy(l => l.literal_marca).GroupBy(b => new { b.literal_marca }).Select(a => new SelectListItem()
            {
                Value = a.Key.literal_marca,
                Text = a.Key.literal_marca
            }).ToList();

            var TamanoRueda_List = new List<SelectListItem>();
            if (participation.pais_par == "ES")
            {
                TamanoRueda_List.Add(
                    new SelectListItem { Value = "MENOR O IGUAL A 16", Text = "MENOR O IGUAL A 16" }
                );
                TamanoRueda_List.Add(
                    new SelectListItem { Value = "MAYOR O IGUAL A 17", Text = "MAYOR O IGUAL A 17" }
                );
            }
            else
            {
                TamanoRueda_List.Add(
                    new SelectListItem { Value = "MENOR O IGUAL A 16", Text = "MENOR O IGUAL A 16" }
                );
                TamanoRueda_List.Add(
                    new SelectListItem { Value = "MAYOR O IGUAL A 17", Text = "MAYOR O IGUAL A 17" }
                );
            }
            ViewBag.TamanoRueda_List = TamanoRueda_List;

            model.TamanoRueda = participation.TamanoRueda;
            if (participation.medidallanta_par == null)
            {
                model.medidallanta_par = "16";
            }
            else
            {
                model.medidallanta_par = participation.medidallanta_par;
            }
            model.numllantas_par = participation.numllantas_par;

            var id_est = participation.id_est;
            ViewBag.id_est = id_est;

            //model.fecha_caducidad = _configuration["FechaCaducidad"];
            model.fecha_caducidad = lines[0].Replace("FechaCaducidad: ", "");

            model.fechaValidacion_par = participation.fechaValidacion_par;

            if (participation.motivo_par == "Otros" || participation.motivo_par == "Outros")
            {
                model.motivo_par = participation.comentarios_par;
            }
            else
            {
                model.motivo_par = participation.motivo_par;
            }

            model.rechazoFecha_par = participation.rechazoFecha_par;
            model.rechazoHora_par = participation.rechazoHora_par;

            model.id_tall = participation.id_tall;

            return View(model);
        }

        public IActionResult EditRechazar(int id)
        {
            ParticipationRechazarEditViewModel model = new ParticipationRechazarEditViewModel();
            Participation participation = _context.Participation.Where(x => x.id_par.Equals(id)).FirstOrDefault();
            model.id_par = participation.id_par;
            ViewBag.pais_par = participation.pais_par;
            return View(model);
        }

        public IActionResult EditValidar(int id)
        {
            ParticipationValidarEditViewModel model = new ParticipationValidarEditViewModel();

            Participation participation = _context.Participation.Where(x => x.id_par.Equals(id)).FirstOrDefault();
            var valorpremio_par = participation.valorpremio_par;
            if (valorpremio_par == null)
            {
                model.producto = _context.Regalo.Where(r => r.id_regalo.Equals(participation.premioSelFrnt_par) && r.pais.Equals(participation.pais_par)).FirstOrDefault().producto;
            }
            else
            {
                model.producto = valorpremio_par;
            }

            model.id_par = participation.id_par;
            var tipo = _context.Regalo.Where(r => r.producto.Equals(model.producto) && r.pais.Equals(participation.pais_par)).FirstOrDefault().tipo;
            if (tipo == "VIRTUAL")
            {
                ViewBag.producto = _context.Regalo.Where(x => x.pais.Equals(participation.pais_par) && x.tipo.Equals("VIRTUAL")).GroupBy(b => new { b.producto }).Select(a => new SelectListItem()
                {
                    Value = a.Key.producto,
                    Text = a.Key.producto
                }).ToList();
            }
            else
            {
                ViewBag.producto = _context.Regalo.Where(x => x.pais.Equals(participation.pais_par)).GroupBy(b => new { b.producto }).Select(a => new SelectListItem()
                {
                    Value = a.Key.producto,
                    Text = a.Key.producto
                }).ToList();
            }

            return View(model);
        }

        public IActionResult EditReenviar(int id)
        {
            ParticipationReenviarEditViewModel model = new ParticipationReenviarEditViewModel();
            Participation participation = _context.Participation.Where(x => x.id_par.Equals(id)).FirstOrDefault();
            model.id_par = participation.id_par;
            return View(model);
        }

        public async Task<IActionResult> PendienteCSV()
        {
            var comlumHeadrs = new string[]
            {
                "NUM;REGISTRO FECHA;REGISTRO HORA;NOMBRE;APELLIDOS;EMAIL;TELÉFONO;DNI;ENSEÑA;LC;HPDV;RAZON SOCIAL;NOMBRE COMERCIAL;POBLACION;PROVINCIA;REGIÒN;PROMOCIÓN;NUM NEUMATICOS SELECCIONADOS;PREMIO SELECCIONADO;NUM NEUMATICOS ENTREGADOS;PREMIO ENTREGADO;COMERCIAL;DONDE NOS CONOCISTE;PAIS;VEHICULO;TIPO;FACTURA 1;FECHA COMPRA 1;FACTURA 2;FECHA COMPRA 2;MODELO;TAMAÑO RUEDA;ESTADO;ULT. MODF;RECHAZO;COMENTARIOS"
            };

            var currentUser = await _userManager.GetUserAsync(User);
            var country = currentUser.pais_usu;
            if (country == "0")
            {
                country = "ES";
            }
            else if (country == "1")
            {
                country = "PT";
            }
            else
            {
                country = "";
            }

            // get all Participationes
            var participationes = (from tempuser in _context.Participation.Where(x => country != "" ? x.pais_par.Equals(country) : true)
                                   select tempuser);
            participationes = participationes.Where(
                 m => m.id_est.Equals(1) && !m.solicitar_adjunto.Equals(1)
                );
            var data = participationes.ToList();
            // Build the file content
            var participationescsv = new StringBuilder();

            data.ForEach(line =>
            {
                var id_tall = line.id_tall;
                Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).FirstOrDefault();
                var estado = "";
                if (line.id_est == 3)
                {
                    estado = "Rechazado";
                }
                else if (line.id_est == 2)
                {
                    estado = "Validado";
                }
                else if (line.id_est == 1)
                {
                    if (line.adjunto_adjunto == 1 && line.solicitar_adjunto == 1)
                    {
                        estado = "Actualizando";
                    }
                    else if (line.adjunto_adjunto == 0 && line.solicitar_adjunto == 1)
                    {
                        estado = "Modificando";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                }
                var comercial_par = "";
                if (line.comercial_par == 0)
                {
                    comercial_par = "No Acepta";
                }
                else
                {
                    comercial_par = "Acepta";
                }

                var vehiculo = "";
                var descripcion_pre = "";
                var id_regalo = line.premioSelFrnt_par;
                Regalo regalo = _context.Regalo.Where(x => x.id_regalo.Equals(id_regalo)).FirstOrDefault();
                if (regalo != null)
                {
                    vehiculo = regalo.vehiculo;
                    descripcion_pre = regalo.producto;
                }


                var tipovehiculo = "MOTO";
                if (regalo != null)
                {
                    if (regalo.vehiculo == "COCHE")
                    {
                        tipovehiculo = "TURISMO";
                        var llanta = line.llanta;
                        if (llanta == "MICHELIN AGILIS" || llanta == "MICHELIN AGILIS 51 SNOW-ICE" || llanta == "MICHELIN AGILIS ALPIN"
                        || llanta == "MICHELIN AGILIS CAMPING" || llanta == "MICHELIN AGILIS CROSSCLIMATE" || llanta == "MICHELIN AGILIS+"
                        || llanta == "MICHELIN AGILIS51" || llanta == "MICHELIN XC4S")
                        {
                            tipovehiculo = "CAMIONETA";
                        }
                        else if (llanta == "MICHELIN 4X4 DIAMARIS" || llanta == "MICHELIN 4X4 O/R XZL" || llanta == "MICHELIN CROSSCLIMATE SUV" ||
                      llanta == "MICHELIN LATITUDE ALPIN" || llanta == "MICHELIN LATITUDE ALPIN LA2" || llanta == "MICHELIN LATITUDE CROSS" ||
                      llanta == "MICHELIN LATITUDE SPORT" ||
                      llanta == "MICHELIN LATITUDE SPORT 3" || llanta == "MICHELIN LATITUDE TOUR" || llanta == "MICHELIN LATITUDE TOUR HP" || llanta == "MICHELIN LATITUDE X-ICE XI2" ||
                      llanta == "MICHELIN PILOT ALPIN 5 SUV" || llanta == "MICHELIN PILOT SPORT 4 SUV" || llanta == "MICHELIN PREMIER LTX" || llanta == "MICHELIN PRIMACY 3" || llanta == "MICHELIN X-ICE XI3")
                        {
                            tipovehiculo = "4X4";
                        }
                    }
                }
                else
                {
                    tipovehiculo = "";
                }

                var numruedas = line.Numero_ruedas;

                var fechaM_par = "";
                var registrofecha_par = "";
                var fechaCompra1_par = "";
                var fechaCompra2_par = "";
                if (line.fechaM_par != null)
                {
                    fechaM_par = line.fechaM_par.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    if (estado == "Pendiente") fechaM_par = "";
                }
                if (line.registrofecha_par != null)
                {
                    registrofecha_par = line.registrofecha_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra1_par != null)
                {
                    fechaCompra1_par = line.fechaCompra1_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra2_par != null)
                {
                    fechaCompra2_par = line.fechaCompra2_par.Value.ToString("dd/MM/yyyy");
                }

                participationescsv.AppendLine(string.Join(";",
                    line.id_par, // NUM
                    registrofecha_par, // REGISTRO FECHA
                    line.registrohora_par, // REGISTRO HORA
                    line.nombre_par, // NOMBRE
                    line.apellidos_par, // APELLIDOS
                    line.email_par, // EMAIL
                    line.telefono_par, // TELÉFONO
                    line.dni_par, // DNI
                    workshop.ENSENA_tall, // ENSEÑA
                    workshop.LC_tall, // LC
                    workshop.HPDV_tall, // HPDV
                    workshop.razonsocial_tall, // RAZON SOCIAL
                    workshop.alias_tall, // NOMBRE COMERCIAL
                    workshop.poblacion_tall, // POBLACION
                    workshop.provincia_tall, // PROVINCIA
                    workshop.REGION_tall, // REGIÒN
                    "VUELTAALCOLE19", // PROMOCIÓN
                    line.Numero_ruedas_int, // NUM NEUMATICOS SELECCIONADOS
                    descripcion_pre, // PREMIO SELECCIONADO
                    line.Numero_ruedas_int, //line.numllantas_par, // NUM NEUMATICOS ENTREGADOS
                    descripcion_pre,   // PREMIO ENTREGADO
                    comercial_par,  // COMERCIAL
                    line.dondeconociste_par, // DONDE NOS CONOCISTE
                    line.pais_par,     // PAIS
                    vehiculo,    // VEHICULO
                    tipovehiculo,    // TIPO
                    line.factura1_par,  // FACTURA 1
                    fechaCompra1_par,   // FECHA COMPRA 1
                    line.factura2_par,    // FACTURA 2
                    fechaCompra2_par, // FECHA COMPRA 2
                    line.llanta, // MODELO
                    line.medidallanta_par,   // TAMAÑO RUEDA
                    estado,   // ESTADO
                    fechaM_par,   // ULT. MODF
                    line.motivo_par,    // RECHAZO
                    line.comentarios_par  // COMENTARIOS
                    ));
            });

            byte[] buffer = Encoding.UTF8.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            var stream = new MemoryStream();
            var csvWriter = new StreamWriter(stream, Encoding.UTF8);
            csvWriter.WriteLine($"{string.Join(";", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            csvWriter.Flush();
            return File(stream.ToArray(), "text/csv", $"participaciones_pendiente.csv");
        }

        public async Task<IActionResult> ModificandoCSV()
        {
            var comlumHeadrs = new string[]
            {
                "NUM;REGISTRO FECHA;REGISTRO HORA;NOMBRE;APELLIDOS;EMAIL;TELÉFONO;DNI;ENSEÑA;LC;HPDV;RAZON SOCIAL;NOMBRE COMERCIAL;POBLACION;PROVINCIA;REGIÒN;PROMOCIÓN;NUM NEUMATICOS SELECCIONADOS;PREMIO SELECCIONADO;NUM NEUMATICOS ENTREGADOS;PREMIO ENTREGADO;COMERCIAL;DONDE NOS CONOCISTE;PAIS;VEHICULO;TIPO;FACTURA 1;FECHA COMPRA 1;FACTURA 2;FECHA COMPRA 2;MODELO;TAMAÑO RUEDA;ESTADO;ULT. MODF;RECHAZO;COMENTARIOS"
            };

            var currentUser = await _userManager.GetUserAsync(User);
            var country = currentUser.pais_usu;
            if (country == "0")
            {
                country = "ES";
            }
            else if (country == "1")
            {
                country = "PT";
            }
            else
            {
                country = "";
            }

            // get all Participationes
            var participationes = (from tempuser in _context.Participation.Where(x => country != "" ? x.pais_par.Equals(country) : true)
                                   select tempuser);
            participationes = participationes.Where(
                 m => m.id_est.Equals(1) && m.solicitar_adjunto.Equals(1) && m.adjunto_adjunto.Equals(0)
                );
            var data = participationes.ToList();
            // Build the file content
            var participationescsv = new StringBuilder();

            data.ForEach(line =>
            {
                var id_tall = line.id_tall;
                Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).FirstOrDefault();
                var estado = "";
                if (line.id_est == 3)
                {
                    estado = "Rechazado";
                }
                else if (line.id_est == 2)
                {
                    estado = "Validado";
                }
                else if (line.id_est == 1)
                {
                    if (line.adjunto_adjunto == 1 && line.solicitar_adjunto == 1)
                    {
                        estado = "Actualizando";
                    }
                    else if (line.adjunto_adjunto == 0 && line.solicitar_adjunto == 1)
                    {
                        estado = "Modificando";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                }
                var comercial_par = "";
                if (line.comercial_par == 0)
                {
                    comercial_par = "No Acepta";
                }
                else
                {
                    comercial_par = "Acepta";
                }

                var vehiculo = "";
                var descripcion_pre = "";
                var id_regalo = line.premioSelFrnt_par;
                Regalo regalo = _context.Regalo.Where(x => x.id_regalo.Equals(id_regalo)).FirstOrDefault();
                if (regalo != null)
                {
                    vehiculo = regalo.vehiculo;
                    descripcion_pre = regalo.producto;
                }


                var tipovehiculo = "MOTO";
                if (regalo != null)
                {
                    if (regalo.vehiculo == "COCHE")
                    {
                        tipovehiculo = "TURISMO";
                        var llanta = line.llanta;
                        if (llanta == "MICHELIN AGILIS" || llanta == "MICHELIN AGILIS 51 SNOW-ICE" || llanta == "MICHELIN AGILIS ALPIN"
                        || llanta == "MICHELIN AGILIS CAMPING" || llanta == "MICHELIN AGILIS CROSSCLIMATE" || llanta == "MICHELIN AGILIS+"
                        || llanta == "MICHELIN AGILIS51" || llanta == "MICHELIN XC4S")
                        {
                            tipovehiculo = "CAMIONETA";
                        }
                        else if (llanta == "MICHELIN 4X4 DIAMARIS" || llanta == "MICHELIN 4X4 O/R XZL" || llanta == "MICHELIN CROSSCLIMATE SUV" ||
                      llanta == "MICHELIN LATITUDE ALPIN" || llanta == "MICHELIN LATITUDE ALPIN LA2" || llanta == "MICHELIN LATITUDE CROSS" ||
                      llanta == "MICHELIN LATITUDE SPORT" ||
                      llanta == "MICHELIN LATITUDE SPORT 3" || llanta == "MICHELIN LATITUDE TOUR" || llanta == "MICHELIN LATITUDE TOUR HP" || llanta == "MICHELIN LATITUDE X-ICE XI2" ||
                      llanta == "MICHELIN PILOT ALPIN 5 SUV" || llanta == "MICHELIN PILOT SPORT 4 SUV" || llanta == "MICHELIN PREMIER LTX" || llanta == "MICHELIN PRIMACY 3" || llanta == "MICHELIN X-ICE XI3")
                        {
                            tipovehiculo = "4X4";
                        }
                    }
                }
                else
                {
                    tipovehiculo = "";
                }

                var numruedas = line.Numero_ruedas;

                var fechaM_par = "";
                var registrofecha_par = "";
                var fechaCompra1_par = "";
                var fechaCompra2_par = "";
                if (line.fechaM_par != null)
                {
                    fechaM_par = line.fechaM_par.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    if (estado == "Pendiente") fechaM_par = "";
                }
                if (line.registrofecha_par != null)
                {
                    registrofecha_par = line.registrofecha_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra1_par != null)
                {
                    fechaCompra1_par = line.fechaCompra1_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra2_par != null)
                {
                    fechaCompra2_par = line.fechaCompra2_par.Value.ToString("dd/MM/yyyy");
                }

                participationescsv.AppendLine(string.Join(";",
                    line.id_par, // NUM
                    registrofecha_par, // REGISTRO FECHA
                    line.registrohora_par, // REGISTRO HORA
                    line.nombre_par, // NOMBRE
                    line.apellidos_par, // APELLIDOS
                    line.email_par, // EMAIL
                    line.telefono_par, // TELÉFONO
                    line.dni_par, // DNI
                    workshop.ENSENA_tall, // ENSEÑA
                    workshop.LC_tall, // LC
                    workshop.HPDV_tall, // HPDV
                    workshop.razonsocial_tall, // RAZON SOCIAL
                    workshop.alias_tall, // NOMBRE COMERCIAL
                    workshop.poblacion_tall, // POBLACION
                    workshop.provincia_tall, // PROVINCIA
                    workshop.REGION_tall, // REGIÒN
                    "OTOÑO19", // PROMOCIÓN
                    line.Numero_ruedas_int, // NUM NEUMATICOS SELECCIONADOS
                    descripcion_pre, // PREMIO SELECCIONADO
                    line.Numero_ruedas_int, //line.numllantas_par, // NUM NEUMATICOS ENTREGADOS
                    descripcion_pre,   // PREMIO ENTREGADO
                    comercial_par,  // COMERCIAL
                    line.dondeconociste_par, // DONDE NOS CONOCISTE
                    line.pais_par,     // PAIS
                    vehiculo,    // VEHICULO
                    tipovehiculo,    // TIPO
                    line.factura1_par,  // FACTURA 1
                    fechaCompra1_par,   // FECHA COMPRA 1
                    line.factura2_par,    // FACTURA 2
                    fechaCompra2_par, // FECHA COMPRA 2
                    line.llanta, // MODELO
                    line.medidallanta_par,   // TAMAÑO RUEDA
                    estado,   // ESTADO
                    fechaM_par,   // ULT. MODF
                    line.motivo_par,    // RECHAZO
                    line.comentarios_par  // COMENTARIOS
                    ));
            });

            byte[] buffer = Encoding.UTF8.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            var stream = new MemoryStream();
            var csvWriter = new StreamWriter(stream, Encoding.UTF8);
            csvWriter.WriteLine($"{string.Join(";", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            csvWriter.Flush();
            return File(stream.ToArray(), "text/csv", $"participaciones_modificando.csv");
        }

        public async Task<IActionResult> ActualizandoCSV()
        {
            var comlumHeadrs = new string[]
            {
                "NUM;REGISTRO FECHA;REGISTRO HORA;NOMBRE;APELLIDOS;EMAIL;TELÉFONO;DNI;ENSEÑA;LC;HPDV;RAZON SOCIAL;NOMBRE COMERCIAL;POBLACION;PROVINCIA;REGIÒN;PROMOCIÓN;NUM NEUMATICOS SELECCIONADOS;PREMIO SELECCIONADO;NUM NEUMATICOS ENTREGADOS;PREMIO ENTREGADO;COMERCIAL;DONDE NOS CONOCISTE;PAIS;VEHICULO;TIPO;FACTURA 1;FECHA COMPRA 1;FACTURA 2;FECHA COMPRA 2;MODELO;TAMAÑO RUEDA;ESTADO;ULT. MODF;RECHAZO;COMENTARIOS"
            };

            var currentUser = await _userManager.GetUserAsync(User);
            var country = currentUser.pais_usu;
            if (country == "0")
            {
                country = "ES";
            }
            else if (country == "1")
            {
                country = "PT";
            }
            else
            {
                country = "";
            }

            // get all Participationes
            var participationes = (from tempuser in _context.Participation.Where(x => country != "" ? x.pais_par.Equals(country) : true)
                                   select tempuser);
            participationes = participationes.Where(
                 m => m.id_est.Equals(1) && m.solicitar_adjunto.Equals(1) && m.adjunto_adjunto.Equals(1)
                );
            var data = participationes.ToList();
            // Build the file content
            var participationescsv = new StringBuilder();

            data.ForEach(line =>
            {
                var id_tall = line.id_tall;
                Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).FirstOrDefault();
                var estado = "";
                if (line.id_est == 3)
                {
                    estado = "Rechazado";
                }
                else if (line.id_est == 2)
                {
                    estado = "Validado";
                }
                else if (line.id_est == 1)
                {
                    if (line.adjunto_adjunto == 1 && line.solicitar_adjunto == 1)
                    {
                        estado = "Actualizando";
                    }
                    else if (line.adjunto_adjunto == 0 && line.solicitar_adjunto == 1)
                    {
                        estado = "Modificando";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                }
                var comercial_par = "";
                if (line.comercial_par == 0)
                {
                    comercial_par = "No Acepta";
                }
                else
                {
                    comercial_par = "Acepta";
                }

                var vehiculo = "";
                var descripcion_pre = "";
                var id_regalo = line.premioSelFrnt_par;
                Regalo regalo = _context.Regalo.Where(x => x.id_regalo.Equals(id_regalo)).FirstOrDefault();
                if (regalo != null)
                {
                    vehiculo = regalo.vehiculo;
                    descripcion_pre = regalo.producto;
                }


                var tipovehiculo = "MOTO";
                if (regalo != null)
                {
                    if (regalo.vehiculo == "COCHE")
                    {
                        tipovehiculo = "TURISMO";
                        var llanta = line.llanta;
                        if (llanta == "MICHELIN AGILIS" || llanta == "MICHELIN AGILIS 51 SNOW-ICE" || llanta == "MICHELIN AGILIS ALPIN"
                        || llanta == "MICHELIN AGILIS CAMPING" || llanta == "MICHELIN AGILIS CROSSCLIMATE" || llanta == "MICHELIN AGILIS+"
                        || llanta == "MICHELIN AGILIS51" || llanta == "MICHELIN XC4S")
                        {
                            tipovehiculo = "CAMIONETA";
                        }
                        else if (llanta == "MICHELIN 4X4 DIAMARIS" || llanta == "MICHELIN 4X4 O/R XZL" || llanta == "MICHELIN CROSSCLIMATE SUV" ||
                      llanta == "MICHELIN LATITUDE ALPIN" || llanta == "MICHELIN LATITUDE ALPIN LA2" || llanta == "MICHELIN LATITUDE CROSS" ||
                      llanta == "MICHELIN LATITUDE SPORT" ||
                      llanta == "MICHELIN LATITUDE SPORT 3" || llanta == "MICHELIN LATITUDE TOUR" || llanta == "MICHELIN LATITUDE TOUR HP" || llanta == "MICHELIN LATITUDE X-ICE XI2" ||
                      llanta == "MICHELIN PILOT ALPIN 5 SUV" || llanta == "MICHELIN PILOT SPORT 4 SUV" || llanta == "MICHELIN PREMIER LTX" || llanta == "MICHELIN PRIMACY 3" || llanta == "MICHELIN X-ICE XI3")
                        {
                            tipovehiculo = "4X4";
                        }
                    }
                }
                else
                {
                    tipovehiculo = "";
                }

                var numruedas = line.Numero_ruedas;

                var fechaM_par = "";
                var registrofecha_par = "";
                var fechaCompra1_par = "";
                var fechaCompra2_par = "";
                if (line.fechaM_par != null)
                {
                    fechaM_par = line.fechaM_par.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    if (estado == "Pendiente") fechaM_par = "";
                }
                if (line.registrofecha_par != null)
                {
                    registrofecha_par = line.registrofecha_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra1_par != null)
                {
                    fechaCompra1_par = line.fechaCompra1_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra2_par != null)
                {
                    fechaCompra2_par = line.fechaCompra2_par.Value.ToString("dd/MM/yyyy");
                }

                participationescsv.AppendLine(string.Join(";",
                    line.id_par, // NUM
                    registrofecha_par, // REGISTRO FECHA
                    line.registrohora_par, // REGISTRO HORA
                    line.nombre_par, // NOMBRE
                    line.apellidos_par, // APELLIDOS
                    line.email_par, // EMAIL
                    line.telefono_par, // TELÉFONO
                    line.dni_par, // DNI
                    workshop.ENSENA_tall, // ENSEÑA
                    workshop.LC_tall, // LC
                    workshop.HPDV_tall, // HPDV
                    workshop.razonsocial_tall, // RAZON SOCIAL
                    workshop.alias_tall, // NOMBRE COMERCIAL
                    workshop.poblacion_tall, // POBLACION
                    workshop.provincia_tall, // PROVINCIA
                    workshop.REGION_tall, // REGIÒN
                    "OTOÑO19", // PROMOCIÓN
                    line.Numero_ruedas_int, // NUM NEUMATICOS SELECCIONADOS
                    descripcion_pre, // PREMIO SELECCIONADO
                    line.Numero_ruedas_int, //line.numllantas_par, // NUM NEUMATICOS ENTREGADOS
                    descripcion_pre,   // PREMIO ENTREGADO
                    comercial_par,  // COMERCIAL
                    line.dondeconociste_par, // DONDE NOS CONOCISTE
                    line.pais_par,     // PAIS
                    vehiculo,    // VEHICULO
                    tipovehiculo,    // TIPO
                    line.factura1_par,  // FACTURA 1
                    fechaCompra1_par,   // FECHA COMPRA 1
                    line.factura2_par,    // FACTURA 2
                    fechaCompra2_par, // FECHA COMPRA 2
                    line.llanta, // MODELO
                    line.medidallanta_par,   // TAMAÑO RUEDA
                    estado,   // ESTADO
                    fechaM_par,   // ULT. MODF
                    line.motivo_par,    // RECHAZO
                    line.comentarios_par  // COMENTARIOS
                    ));
            });

            byte[] buffer = Encoding.UTF8.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            var stream = new MemoryStream();
            var csvWriter = new StreamWriter(stream, Encoding.UTF8);
            csvWriter.WriteLine($"{string.Join(";", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            csvWriter.Flush();
            return File(stream.ToArray(), "text/csv", $"participaciones_actualizando.csv");
        }

        public async Task<IActionResult> ValidadoCSV()
        {
            var comlumHeadrs = new string[]
            {
                "NUM;REGISTRO FECHA;REGISTRO HORA;NOMBRE;APELLIDOS;EMAIL;TELÉFONO;DNI;ENSEÑA;LC;HPDV;RAZON SOCIAL;NOMBRE COMERCIAL;POBLACION;PROVINCIA;REGIÒN;PROMOCIÓN;NUM NEUMATICOS SELECCIONADOS;PREMIO SELECCIONADO;NUM NEUMATICOS ENTREGADOS;PREMIO ENTREGADO;COMERCIAL;DONDE NOS CONOCISTE;PAIS;VEHICULO;TIPO;FACTURA 1;FECHA COMPRA 1;FACTURA 2;FECHA COMPRA 2;MODELO;TAMAÑO RUEDA;ESTADO;ULT. MODF;RECHAZO;COMENTARIOS"
            };

            var currentUser = await _userManager.GetUserAsync(User);
            var country = currentUser.pais_usu;
            if (country == "0")
            {
                country = "ES";
            }
            else if (country == "1")
            {
                country = "PT";
            }
            else
            {
                country = "";
            }

            // get all Participationes
            var participationes = (from tempuser in _context.Participation.Where(x => country != "" ? x.pais_par.Equals(country) : true)
                                   select tempuser);
            participationes = participationes.Where(
                 m => m.id_est.Equals(2)
                );
            var data = participationes.ToList();
            // Build the file content
            var participationescsv = new StringBuilder();

            data.ForEach(line =>
            {
                var id_tall = line.id_tall;
                Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).FirstOrDefault();
                var estado = "";
                if (line.id_est == 3)
                {
                    estado = "Rechazado";
                }
                else if (line.id_est == 2)
                {
                    estado = "Validado";
                }
                else if (line.id_est == 1)
                {
                    if (line.adjunto_adjunto == 1 && line.solicitar_adjunto == 1)
                    {
                        estado = "Actualizando";
                    }
                    else if (line.adjunto_adjunto == 0 && line.solicitar_adjunto == 1)
                    {
                        estado = "Modificando";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                }
                var comercial_par = "";
                if (line.comercial_par == 0)
                {
                    comercial_par = "No Acepta";
                }
                else
                {
                    comercial_par = "Acepta";
                }

                var vehiculo = "";
                var descripcion_pre = "";
                var id_regalo = _context.Regalo.Where(r => r.pais.Equals(line.pais_par) && r.producto.Equals(line.valorpremio_par)).FirstOrDefault().id_regalo;
                Regalo regalo = _context.Regalo.Where(x => x.id_regalo.Equals(id_regalo)).FirstOrDefault();
                if (regalo != null)
                {
                    vehiculo = regalo.vehiculo;
                    descripcion_pre = regalo.producto;
                }


                var tipovehiculo = "MOTO";
                if (regalo != null)
                {
                    if (regalo.vehiculo == "COCHE")
                    {
                        tipovehiculo = "TURISMO";
                        var llanta = line.llanta;
                        if (llanta == "MICHELIN AGILIS" || llanta == "MICHELIN AGILIS 51 SNOW-ICE" || llanta == "MICHELIN AGILIS ALPIN"
                        || llanta == "MICHELIN AGILIS CAMPING" || llanta == "MICHELIN AGILIS CROSSCLIMATE" || llanta == "MICHELIN AGILIS+"
                        || llanta == "MICHELIN AGILIS51" || llanta == "MICHELIN XC4S")
                        {
                            tipovehiculo = "CAMIONETA";
                        }
                        else if (llanta == "MICHELIN 4X4 DIAMARIS" || llanta == "MICHELIN 4X4 O/R XZL" || llanta == "MICHELIN CROSSCLIMATE SUV" ||
                      llanta == "MICHELIN LATITUDE ALPIN" || llanta == "MICHELIN LATITUDE ALPIN LA2" || llanta == "MICHELIN LATITUDE CROSS" ||
                      llanta == "MICHELIN LATITUDE SPORT" ||
                      llanta == "MICHELIN LATITUDE SPORT 3" || llanta == "MICHELIN LATITUDE TOUR" || llanta == "MICHELIN LATITUDE TOUR HP" || llanta == "MICHELIN LATITUDE X-ICE XI2" ||
                      llanta == "MICHELIN PILOT ALPIN 5 SUV" || llanta == "MICHELIN PILOT SPORT 4 SUV" || llanta == "MICHELIN PREMIER LTX" || llanta == "MICHELIN PRIMACY 3" || llanta == "MICHELIN X-ICE XI3")
                        {
                            tipovehiculo = "4X4";
                        }
                    }
                }
                else
                {
                    tipovehiculo = "";
                }

                var numruedas = line.Numero_ruedas;

                var fechaM_par = "";
                var registrofecha_par = "";
                var fechaCompra1_par = "";
                var fechaCompra2_par = "";
                if (line.fechaM_par != null)
                {
                    fechaM_par = line.fechaM_par.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    if (estado == "Pendiente") fechaM_par = "";
                }
                if (line.registrofecha_par != null)
                {
                    registrofecha_par = line.registrofecha_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra1_par != null)
                {
                    fechaCompra1_par = line.fechaCompra1_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra2_par != null)
                {
                    fechaCompra2_par = line.fechaCompra2_par.Value.ToString("dd/MM/yyyy");
                }

                participationescsv.AppendLine(string.Join(";",
                    line.id_par, // NUM
                    registrofecha_par, // REGISTRO FECHA
                    line.registrohora_par, // REGISTRO HORA
                    line.nombre_par, // NOMBRE
                    line.apellidos_par, // APELLIDOS
                    line.email_par, // EMAIL
                    line.telefono_par, // TELÉFONO
                    line.dni_par, // DNI
                    workshop.ENSENA_tall, // ENSEÑA
                    workshop.LC_tall, // LC
                    workshop.HPDV_tall, // HPDV
                    workshop.razonsocial_tall, // RAZON SOCIAL
                    workshop.alias_tall, // NOMBRE COMERCIAL
                    workshop.poblacion_tall, // POBLACION
                    workshop.provincia_tall, // PROVINCIA
                    workshop.REGION_tall, // REGIÒN
                    "OTOÑO19", // PROMOCIÓN
                    line.Numero_ruedas_int, // NUM NEUMATICOS SELECCIONADOS
                    descripcion_pre, // PREMIO SELECCIONADO
                    line.Numero_ruedas_int, //line.numllantas_par, // NUM NEUMATICOS ENTREGADOS
                    descripcion_pre,   // PREMIO ENTREGADO
                    comercial_par,  // COMERCIAL
                    line.dondeconociste_par, // DONDE NOS CONOCISTE
                    line.pais_par,     // PAIS
                    vehiculo,    // VEHICULO
                    tipovehiculo,    // TIPO
                    line.factura1_par,  // FACTURA 1
                    fechaCompra1_par,   // FECHA COMPRA 1
                    line.factura2_par,    // FACTURA 2
                    fechaCompra2_par, // FECHA COMPRA 2
                    line.llanta, // MODELO
                    line.medidallanta_par,   // TAMAÑO RUEDA
                    estado,   // ESTADO
                    fechaM_par,   // ULT. MODF
                    line.motivo_par,    // RECHAZO
                    line.comentarios_par  // COMENTARIOS
                    ));
            });

            byte[] buffer = Encoding.UTF8.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            var stream = new MemoryStream();
            var csvWriter = new StreamWriter(stream, Encoding.UTF8);
            csvWriter.WriteLine($"{string.Join(";", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            csvWriter.Flush();
            return File(stream.ToArray(), "text/csv", $"participaciones_validado.csv");
        }

        public async Task<IActionResult> RechazadoCSV()
        {
            var comlumHeadrs = new string[]
            {
                "NUM;REGISTRO FECHA;REGISTRO HORA;NOMBRE;APELLIDOS;EMAIL;TELÉFONO;DNI;ENSEÑA;LC;HPDV;RAZON SOCIAL;NOMBRE COMERCIAL;POBLACION;PROVINCIA;REGIÒN;PROMOCIÓN;NUM NEUMATICOS SELECCIONADOS;PREMIO SELECCIONADO;NUM NEUMATICOS ENTREGADOS;PREMIO ENTREGADO;COMERCIAL;DONDE NOS CONOCISTE;PAIS;VEHICULO;TIPO;FACTURA 1;FECHA COMPRA 1;FACTURA 2;FECHA COMPRA 2;MODELO;TAMAÑO RUEDA;ESTADO;ULT. MODF;RECHAZO;COMENTARIOS"
            };

            var currentUser = await _userManager.GetUserAsync(User);
            var country = currentUser.pais_usu;
            if (country == "0")
            {
                country = "ES";
            }
            else if (country == "1")
            {
                country = "PT";
            }
            else
            {
                country = "";
            }

            // get all Participationes
            var participationes = (from tempuser in _context.Participation.Where(x => country != "" ? x.pais_par.Equals(country) : true)
                                   select tempuser);
            participationes = participationes.Where(
                 m => m.id_est.Equals(3)
                );
            var data = participationes.ToList();
            // Build the file content
            var participationescsv = new StringBuilder();

            data.ForEach(line =>
            {
                var id_tall = line.id_tall;
                Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).FirstOrDefault();
                var estado = "";
                if (line.id_est == 3)
                {
                    estado = "Rechazado";
                }
                else if (line.id_est == 2)
                {
                    estado = "Validado";
                }
                else if (line.id_est == 1)
                {
                    if (line.adjunto_adjunto == 1 && line.solicitar_adjunto == 1)
                    {
                        estado = "Actualizando";
                    }
                    else if (line.adjunto_adjunto == 0 && line.solicitar_adjunto == 1)
                    {
                        estado = "Modificando";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                }
                var comercial_par = "";
                if (line.comercial_par == 0)
                {
                    comercial_par = "No Acepta";
                }
                else
                {
                    comercial_par = "Acepta";
                }

                var vehiculo = "";
                var descripcion_pre = "";
                var id_regalo = line.premioSelFrnt_par;
                Regalo regalo = _context.Regalo.Where(x => x.id_regalo.Equals(id_regalo)).FirstOrDefault();
                if (regalo != null)
                {
                    vehiculo = regalo.vehiculo;
                    descripcion_pre = regalo.producto;
                }


                var tipovehiculo = "MOTO";
                if (regalo != null)
                {
                    if (regalo.vehiculo == "COCHE")
                    {
                        tipovehiculo = "TURISMO";
                        var llanta = line.llanta;
                        if (llanta == "MICHELIN AGILIS" || llanta == "MICHELIN AGILIS 51 SNOW-ICE" || llanta == "MICHELIN AGILIS ALPIN"
                        || llanta == "MICHELIN AGILIS CAMPING" || llanta == "MICHELIN AGILIS CROSSCLIMATE" || llanta == "MICHELIN AGILIS+"
                        || llanta == "MICHELIN AGILIS51" || llanta == "MICHELIN XC4S")
                        {
                            tipovehiculo = "CAMIONETA";
                        }
                        else if (llanta == "MICHELIN 4X4 DIAMARIS" || llanta == "MICHELIN 4X4 O/R XZL" || llanta == "MICHELIN CROSSCLIMATE SUV" ||
                      llanta == "MICHELIN LATITUDE ALPIN" || llanta == "MICHELIN LATITUDE ALPIN LA2" || llanta == "MICHELIN LATITUDE CROSS" ||
                      llanta == "MICHELIN LATITUDE SPORT" ||
                      llanta == "MICHELIN LATITUDE SPORT 3" || llanta == "MICHELIN LATITUDE TOUR" || llanta == "MICHELIN LATITUDE TOUR HP" || llanta == "MICHELIN LATITUDE X-ICE XI2" ||
                      llanta == "MICHELIN PILOT ALPIN 5 SUV" || llanta == "MICHELIN PILOT SPORT 4 SUV" || llanta == "MICHELIN PREMIER LTX" || llanta == "MICHELIN PRIMACY 3" || llanta == "MICHELIN X-ICE XI3")
                        {
                            tipovehiculo = "4X4";
                        }
                    }
                }
                else
                {
                    tipovehiculo = "";
                }

                var numruedas = line.Numero_ruedas;

                var fechaM_par = "";
                var registrofecha_par = "";
                var fechaCompra1_par = "";
                var fechaCompra2_par = "";
                if (line.fechaM_par != null)
                {
                    fechaM_par = line.fechaM_par.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    if (estado == "Pendiente") fechaM_par = "";
                }
                if (line.registrofecha_par != null)
                {
                    registrofecha_par = line.registrofecha_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra1_par != null)
                {
                    fechaCompra1_par = line.fechaCompra1_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra2_par != null)
                {
                    fechaCompra2_par = line.fechaCompra2_par.Value.ToString("dd/MM/yyyy");
                }

                participationescsv.AppendLine(string.Join(";",
                    line.id_par, // NUM
                    registrofecha_par, // REGISTRO FECHA
                    line.registrohora_par, // REGISTRO HORA
                    line.nombre_par, // NOMBRE
                    line.apellidos_par, // APELLIDOS
                    line.email_par, // EMAIL
                    line.telefono_par, // TELÉFONO
                    line.dni_par, // DNI
                    workshop.ENSENA_tall, // ENSEÑA
                    workshop.LC_tall, // LC
                    workshop.HPDV_tall, // HPDV
                    workshop.razonsocial_tall, // RAZON SOCIAL
                    workshop.alias_tall, // NOMBRE COMERCIAL
                    workshop.poblacion_tall, // POBLACION
                    workshop.provincia_tall, // PROVINCIA
                    workshop.REGION_tall, // REGIÒN
                    "OTOÑO19", // PROMOCIÓN
                    line.Numero_ruedas_int, // NUM NEUMATICOS SELECCIONADOS
                    descripcion_pre, // PREMIO SELECCIONADO
                    line.Numero_ruedas_int, //line.numllantas_par, // NUM NEUMATICOS ENTREGADOS
                    descripcion_pre,   // PREMIO ENTREGADO
                    comercial_par,  // COMERCIAL
                    line.dondeconociste_par, // DONDE NOS CONOCISTE
                    line.pais_par,     // PAIS
                    vehiculo,    // VEHICULO
                    tipovehiculo,    // TIPO
                    line.factura1_par,  // FACTURA 1
                    fechaCompra1_par,   // FECHA COMPRA 1
                    line.factura2_par,    // FACTURA 2
                    fechaCompra2_par, // FECHA COMPRA 2
                    line.llanta, // MODELO
                    line.medidallanta_par,   // TAMAÑO RUEDA
                    estado,   // ESTADO
                    fechaM_par,   // ULT. MODF
                    line.motivo_par,    // RECHAZO
                    line.comentarios_par  // COMENTARIOS
                    ));
            });

            byte[] buffer = Encoding.UTF8.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            var stream = new MemoryStream();
            var csvWriter = new StreamWriter(stream, Encoding.UTF8);
            csvWriter.WriteLine($"{string.Join(";", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            csvWriter.Flush();
            return File(stream.ToArray(), "text/csv", $"participaciones_rechazado.csv");
        }

        public async Task<IActionResult> TodasCSV()
        {
            var comlumHeadrs = new string[]
            {
                "NUM;REGISTRO FECHA;REGISTRO HORA;NOMBRE;APELLIDOS;EMAIL;TELÉFONO;DNI;ENSEÑA;LC;HPDV;RAZON SOCIAL;NOMBRE COMERCIAL;POBLACION;PROVINCIA;REGIÒN;PROMOCIÓN;NUM NEUMATICOS SELECCIONADOS;PREMIO SELECCIONADO;NUM NEUMATICOS ENTREGADOS;PREMIO ENTREGADO;COMERCIAL;DONDE NOS CONOCISTE;PAIS;VEHICULO;TIPO;FACTURA 1;FECHA COMPRA 1;FACTURA 2;FECHA COMPRA 2;MODELO;TAMAÑO RUEDA;ESTADO;ULT. MODF;RECHAZO;COMENTARIOS"
            };

            var currentUser = await _userManager.GetUserAsync(User);
            var country = currentUser.pais_usu;
            if (country == "0")
            {
                country = "ES";
            }
            else if (country == "1")
            {
                country = "PT";
            }
            else
            {
                country = "";
            }

            // get all Participationes
            var participationes = (from tempuser in _context.Participation.Where(x => country != "" ? x.pais_par.Equals(country) : true)
                                   select tempuser);

            var data = participationes.ToList();
            // Build the file content
            var participationescsv = new StringBuilder();

            data.ForEach(line =>
            {
                var id_tall = line.id_tall;
                Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).FirstOrDefault();
                var estado = "";
                if (line.id_est == 3)
                {
                    estado = "Rechazado";
                }
                else if (line.id_est == 2)
                {
                    estado = "Validado";
                }
                else if (line.id_est == 1)
                {
                    if (line.adjunto_adjunto == 1 && line.solicitar_adjunto == 1)
                    {
                        estado = "Actualizando";
                    }
                    else if (line.adjunto_adjunto == 0 && line.solicitar_adjunto == 1)
                    {
                        estado = "Modificando";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                }
                var comercial_par = "";
                if (line.comercial_par == 0)
                {
                    comercial_par = "No Acepta";
                }
                else
                {
                    comercial_par = "Acepta";
                }

                var vehiculo = "";
                var descripcion_pre = "";

                int? id_regalo = 0;
                if (line.valorpremio_par == null)
                {
                    id_regalo = line.premioSelFrnt_par;
                }
                else
                {
                    id_regalo = _context.Regalo.Where(r => r.pais.Equals(line.pais_par) && r.producto.Equals(line.valorpremio_par)).FirstOrDefault().id_regalo;
                }

                Regalo regalo = _context.Regalo.Where(x => x.id_regalo.Equals(id_regalo)).FirstOrDefault();
                if (regalo != null)
                {
                    vehiculo = regalo.vehiculo;
                    descripcion_pre = regalo.producto;
                }


                var tipovehiculo = "MOTO";
                if (regalo != null)
                {
                    if (regalo.vehiculo == "COCHE")
                    {
                        tipovehiculo = "TURISMO";
                        var llanta = line.llanta;
                        if (llanta == "MICHELIN AGILIS" || llanta == "MICHELIN AGILIS 51 SNOW-ICE" || llanta == "MICHELIN AGILIS ALPIN"
                        || llanta == "MICHELIN AGILIS CAMPING" || llanta == "MICHELIN AGILIS CROSSCLIMATE" || llanta == "MICHELIN AGILIS+"
                        || llanta == "MICHELIN AGILIS51" || llanta == "MICHELIN XC4S")
                        {
                            tipovehiculo = "CAMIONETA";
                        }
                        else if (llanta == "MICHELIN 4X4 DIAMARIS" || llanta == "MICHELIN 4X4 O/R XZL" || llanta == "MICHELIN CROSSCLIMATE SUV" ||
                      llanta == "MICHELIN LATITUDE ALPIN" || llanta == "MICHELIN LATITUDE ALPIN LA2" || llanta == "MICHELIN LATITUDE CROSS" ||
                      llanta == "MICHELIN LATITUDE SPORT" ||
                      llanta == "MICHELIN LATITUDE SPORT 3" || llanta == "MICHELIN LATITUDE TOUR" || llanta == "MICHELIN LATITUDE TOUR HP" || llanta == "MICHELIN LATITUDE X-ICE XI2" ||
                      llanta == "MICHELIN PILOT ALPIN 5 SUV" || llanta == "MICHELIN PILOT SPORT 4 SUV" || llanta == "MICHELIN PREMIER LTX" || llanta == "MICHELIN PRIMACY 3" || llanta == "MICHELIN X-ICE XI3")
                        {
                            tipovehiculo = "4X4";
                        }
                    }
                }
                else
                {
                    tipovehiculo = "";
                }

                var numruedas = line.Numero_ruedas;

                var fechaM_par = "";
                var registrofecha_par = "";
                var fechaCompra1_par = "";
                var fechaCompra2_par = "";
                if (line.fechaM_par != null)
                {
                    fechaM_par = line.fechaM_par.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    if (estado == "Pendiente") fechaM_par = "";
                }
                if (line.registrofecha_par != null)
                {
                    registrofecha_par = line.registrofecha_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra1_par != null)
                {
                    fechaCompra1_par = line.fechaCompra1_par.Value.ToString("dd/MM/yyyy");
                }
                if (line.fechaCompra2_par != null)
                {
                    fechaCompra2_par = line.fechaCompra2_par.Value.ToString("dd/MM/yyyy");
                }

                participationescsv.AppendLine(string.Join(";",
                    line.id_par, // NUM
                    registrofecha_par, // REGISTRO FECHA
                    line.registrohora_par, // REGISTRO HORA
                    line.nombre_par, // NOMBRE
                    line.apellidos_par, // APELLIDOS
                    line.email_par, // EMAIL
                    line.telefono_par, // TELÉFONO
                    line.dni_par, // DNI
                    workshop.ENSENA_tall, // ENSEÑA
                    workshop.LC_tall, // LC
                    workshop.HPDV_tall, // HPDV
                    workshop.razonsocial_tall, // RAZON SOCIAL
                    workshop.alias_tall, // NOMBRE COMERCIAL
                    workshop.poblacion_tall, // POBLACION
                    workshop.provincia_tall, // PROVINCIA
                    workshop.REGION_tall, // REGIÒN
                    "OTOÑO19", // PROMOCIÓN
                    line.Numero_ruedas_int, // NUM NEUMATICOS SELECCIONADOS
                    descripcion_pre, // PREMIO SELECCIONADO
                    line.Numero_ruedas_int, //line.numllantas_par, // NUM NEUMATICOS ENTREGADOS
                    descripcion_pre,   // PREMIO ENTREGADO
                    comercial_par,  // COMERCIAL
                    line.dondeconociste_par, // DONDE NOS CONOCISTE
                    line.pais_par,     // PAIS
                    vehiculo,    // VEHICULO
                    tipovehiculo,    // TIPO
                    line.factura1_par,  // FACTURA 1
                    fechaCompra1_par,   // FECHA COMPRA 1
                    line.factura2_par,    // FACTURA 2
                    fechaCompra2_par, // FECHA COMPRA 2
                    line.llanta, // MODELO
                    line.medidallanta_par,   // TAMAÑO RUEDA
                    estado,   // ESTADO
                    fechaM_par,   // ULT. MODF
                    line.motivo_par,    // RECHAZO
                    line.comentarios_par  // COMENTARIOS
                    ));
            });

            byte[] buffer = Encoding.UTF8.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            var stream = new MemoryStream();
            var csvWriter = new StreamWriter(stream, Encoding.UTF8);
            csvWriter.WriteLine($"{string.Join(";", comlumHeadrs)}\r\n{participationescsv.ToString()}");
            csvWriter.Flush();
            return File(stream.ToArray(), "text/csv", $"participaciones_todas.csv");
        }

    }
}
