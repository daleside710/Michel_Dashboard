﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminApp.Data;

namespace AdminApp.Controllers
{
    public class SeleccioneumaticoPTController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public SeleccioneumaticoPTController(ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        [Route("seleccion-participa-pt")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("seleccion-select-form-pt")]
        public IActionResult SelectForm()  // idsession and 3 select options updated
        {
            var sessionID = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("idsession_pt", sessionID); // create idsession

            CookieOptions option = new CookieOptions();
            var llanta_model = HttpContext.Request.Form["llanta_model"].FirstOrDefault();
            var llanta = HttpContext.Request.Form["llanta"].FirstOrDefault();
            if (llanta.Contains("16"))
            {
                llanta = "MENOR O IGUAL A 16";
            }
            else
            {
                llanta = "MAYOR O IGUAL A 17";
            }
            
            var neumatico = HttpContext.Request.Form["neumatico"].FirstOrDefault();
            if (neumatico.Contains("2"))
            {
                neumatico = "2 NEUMÁTICOS";
            }
            else
            {
                neumatico = "4 NEUMÁTICOS";
            }
            Response.Cookies.Append("llanta_model_pt", llanta_model, option);
            Response.Cookies.Append("llanta_pt", llanta, option);
            Response.Cookies.Append("neumatico_pt", neumatico, option);

            // log for 3 selects 
            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
            {
                sw.WriteLine(
                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                    "llanta_model: " + llanta_model + ", " +
                    "llanta: " + llanta + ", " +
                    "neumatico: " + neumatico + ", " +
                    "idsession: " + sessionID + ", " + 
                    "action: " + "3 selects"
                    );
            }
            //
            return Redirect("seleccion-regalo-pt");
        }

        [Route("seleccion-regalo-pt")]
        public IActionResult Regalo()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            
            var llanta_model = Request.Cookies["llanta_model_pt"];
            var llanta = Request.Cookies["llanta_pt"];
            var neumatico = Request.Cookies["neumatico_pt"];

            if (llanta.Contains("16"))
            {
                llanta = "16";
            }
            else
            {
                llanta = "17";
            }
            if (neumatico.Contains("2"))
            {
                neumatico = "2";
            }
            else
            {
                neumatico = "4";
            }
            var regalos = _context.Regalo.Where(r=>r.llantas.Equals(llanta) && 
                                                    r.neumaticos.Equals(neumatico) && 
                                                    r.mostrar.Equals(1) &&
                                                    r.pais.Equals("PT")).ToList();
            ViewBag.regalos = regalos;
            ViewBag.hiddenidsession = idsession; // send idsession to regalos page
            return View();
        }

        [Route("seleccion-regalo-form-pt")]
        public IActionResult RegaloForm() // id_regalo updated
        {
            CookieOptions option = new CookieOptions();
            //option.Expires = new DateTimeOffset(DateTime.UtcNow.AddMinutes(30));            

            var idsession = HttpContext.Session.GetString("idsession_pt");
            var hiddenidsession = HttpContext.Request.Form["hiddenidsession"].FirstOrDefault();
            if(hiddenidsession != idsession)
            {
                return Redirect("seleccion-participa-pt");
            }
            
            var tipo = HttpContext.Request.Form["tipo"].FirstOrDefault();
            var link = HttpContext.Request.Form["link"].FirstOrDefault();
            var id_regalo = HttpContext.Request.Form["id"].FirstOrDefault();
            var descripcion = HttpContext.Request.Form["descripcion"].FirstOrDefault();
            Response.Cookies.Append("tipo_pt", tipo, option);
            Response.Cookies.Append("id_regalo_pt", id_regalo, option);

            // log for regalo
            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
            {
                sw.WriteLine(
                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                    "id_regalo: " + id_regalo + ", " +
                    "descripcion: " + descripcion + ", " +
                    "idsession: " + idsession + ", " +
                    "action: " + "select regalo"
                    );
            }
            //

            return Redirect(link + "-pt");
        }
    }
}
