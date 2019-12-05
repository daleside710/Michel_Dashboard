using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class FormularioController : Controller
    {
        private string[] lines;

        public FormularioController()
        {
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
        }
        [Route("seleccion-regalo-detail-form")]
        public IActionResult RegaloDetailForm()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            var hiddenidsession = HttpContext.Request.Form["hiddenidsession"].FirstOrDefault();

            var talla_par = HttpContext.Request.Form["talla_par"].FirstOrDefault();
            CookieOptions option = new CookieOptions();
            if(talla_par == null)
            {
                talla_par = "";
            }
            Response.Cookies.Append("talla", talla_par, option);

            if (hiddenidsession != idsession)
            {
                return Redirect("seleccion-participa");
            }
            return Redirect("registro-participacion");
        }

        [Route("registro-participacion")]
        public IActionResult Formulario()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            if (idsession == null)
            {
                return Redirect("seleccion-participa");
            }

            var llanta_model = Request.Cookies["llanta_model"];
            var llanta = Request.Cookies["llanta"];
            var neumatico = Request.Cookies["neumatico"];
            var id_regalo = Request.Cookies["id_regalo"];
            if(llanta_model == null || llanta == null || neumatico == null || id_regalo == null)
            {
                return Redirect("seleccion-participa");
            }
            var tipo = Request.Cookies["tipo"];
            if(tipo == "FISICO")
            {
                return Redirect("registro-participacion-dir");
            }
            string captcha_key = lines[7].Replace("CAPTCHA_KEY: ", "");
            ViewBag.captcha_key = captcha_key;
            return View();
        }

        [Route("registro-participacion-dir")]
        public IActionResult FormularioFisico()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            if (idsession == null)
            {
                return Redirect("seleccion-participa");
            }

            var llanta_model = Request.Cookies["llanta_model"];
            var llanta = Request.Cookies["llanta"];
            var neumatico = Request.Cookies["neumatico"];
            var id_regalo = Request.Cookies["id_regalo"];
            if (llanta_model == null || llanta == null || neumatico == null || id_regalo == null)
            {
                return Redirect("seleccion-participa");
            }
            string captcha_key = lines[7].Replace("CAPTCHA_KEY: ", "");
            ViewBag.captcha_key = captcha_key;
            return View();
        }

    }
}
