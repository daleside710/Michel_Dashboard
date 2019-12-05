using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class FormularioPTController : Controller
    {
        private string[] lines;

        public FormularioPTController()
        {
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
        }

        [Route("seleccion-regalo-detail-form-pt")]
        public IActionResult RegaloDetailForm()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            var hiddenidsession = HttpContext.Request.Form["hiddenidsession"].FirstOrDefault();

            var talla_par = HttpContext.Request.Form["talla_par"].FirstOrDefault();
            CookieOptions option = new CookieOptions();
            if(talla_par == null)
            {
                talla_par = "";
            }
            Response.Cookies.Append("talla_pt", talla_par, option);

            if (hiddenidsession != idsession)
            {
                return Redirect("seleccion-participa-pt");
            }
            return Redirect("registro-participacion-pt");
        }

        [Route("registro-participacion-pt")]
        public IActionResult Formulario()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            if (idsession == null)
            {
                return Redirect("seleccion-participa-pt");
            }

            var llanta_model = Request.Cookies["llanta_model_pt"];
            var llanta = Request.Cookies["llanta_pt"];
            var neumatico = Request.Cookies["neumatico_pt"];
            var id_regalo = Request.Cookies["id_regalo_pt"];
            if(llanta_model == null || llanta == null || neumatico == null || id_regalo == null)
            {
                return Redirect("seleccion-participa-pt");
            }
            var tipo = Request.Cookies["tipo_pt"];
            if(tipo == "FISICO")
            {
                return Redirect("registro-participacion-dir-pt");
            }
            string captcha_key = lines[7].Replace("CAPTCHA_KEY: ", "");
            ViewBag.captcha_key = captcha_key;
            return View();
        }

        [Route("registro-participacion-dir-pt")]
        public IActionResult FormularioFisico()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            if (idsession == null)
            {
                return Redirect("seleccion-participa-pt");
            }

            var llanta_model = Request.Cookies["llanta_model_pt"];
            var llanta = Request.Cookies["llanta_pt"];
            var neumatico = Request.Cookies["neumatico_pt"];
            var id_regalo = Request.Cookies["id_regalo_pt"];
            if (llanta_model == null || llanta == null || neumatico == null || id_regalo == null)
            {
                return Redirect("seleccion-participa-pt");
            }
            string captcha_key = lines[7].Replace("CAPTCHA_KEY: ", "");
            ViewBag.captcha_key = captcha_key;
            return View();
        }
    }
}
