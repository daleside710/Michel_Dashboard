using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminApp.Data;

namespace AdminApp.Controllers
{
    public class RegalosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegalosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("regalos")]
        public IActionResult Index()
        {
            var regalos_16_2 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("16") && r.neumaticos.Equals("2") && r.mostrar.Equals(1)).ToList();
            var regalos_16_4 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("16") && r.neumaticos.Equals("4") && r.mostrar.Equals(1)).ToList();
            var regalos_17_2 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("17") && r.neumaticos.Equals("2") && r.mostrar.Equals(1)).ToList();
            var regalos_17_4 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("17") && r.neumaticos.Equals("4") && r.mostrar.Equals(1)).ToList();
            ViewBag.regalos_16_2 = regalos_16_2;
            ViewBag.regalos_16_4 = regalos_16_4;
            ViewBag.regalos_17_2 = regalos_17_2;
            ViewBag.regalos_17_4 = regalos_17_4;
            return View();
        }
        //20
        [Route("regalo-20-paraguas")]
        [Route("regalo-20-paraguas-seleccion")]
        public IActionResult Paraguas20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-corteingles")]
        [Route("regalo-20-corteingles-seleccion")]
        public IActionResult Corteingles20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-decathlon")]
        [Route("regalo-20-decathlon-seleccion")]
        public IActionResult Decathlon20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante20
        [Route("regalo-20-carburante")]
        [Route("regalo-20-carburante-seleccion")]
        public IActionResult Carburante20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-chequecarburant")]
        [Route("regalo-20-chequecarburant-seleccion")]
        public IActionResult ChequeCarburante20()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-andorracarburante")]
        [Route("regalo-20-andorracarburante-seleccion")]
        public IActionResult AndorraCarburante20()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-canariascarburante")]
        [Route("regalo-20-canariascarburante-seleccion")]
        public IActionResult CanariasCarburante20()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-amazon")]
        [Route("regalo-20-amazon-seleccion")]
        public IActionResult Amazon20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //40
        [Route("regalo-40-chaquetaultra")]
        [Route("regalo-40-chaquetaultra-seleccion")]
        public IActionResult Chaquetaultra40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-corteingles")]
        [Route("regalo-40-corteingles-seleccion")]
        public IActionResult Corteingles40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-decathlon")]
        [Route("regalo-40-decathlon-seleccion")]
        public IActionResult Decathlon40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante40
        [Route("regalo-40-carburante")]
        [Route("regalo-40-carburante-seleccion")]
        public IActionResult Carburante40()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-chequecarburant")]
        [Route("regalo-40-chequecarburant-seleccion")]
        public IActionResult ChequeCarburante40()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-andorracarburante")]
        [Route("regalo-40-andorracarburante-seleccion")]
        public IActionResult AndorraCarburante40()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-canariascarburante")]
        [Route("regalo-40-canariascarburante-seleccion")]
        public IActionResult CanariasCarburante40()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-amazon")]
        [Route("regalo-40-amazon-seleccion")]
        public IActionResult Amazon40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //30
        [Route("regalo-30-chaquetaligera")]
        [Route("regalo-30-chaquetaligera-seleccion")]
        public IActionResult Chaquetaligera30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-corteingles")]
        [Route("regalo-30-corteingles-seleccion")]
        public IActionResult Corteingles30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-decathlon")]
        [Route("regalo-30-decathlon-seleccion")]
        public IActionResult Decathlon30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante30
        [Route("regalo-30-carburante")]
        [Route("regalo-30-carburante-seleccion")]
        public IActionResult Carburante30()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-chequecarburant")]
        [Route("regalo-30-chequecarburant-seleccion")]
        public IActionResult ChequeCarburante30()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-andorracarburante")]
        [Route("regalo-30-andorracarburante-seleccion")]
        public IActionResult AndorraCarburante30()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-canariascarburante")]
        [Route("regalo-30-canariascarburante-seleccion")]
        public IActionResult CanariasCarburante30()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-amazon")]
        [Route("regalo-30-amazon-seleccion")]
        public IActionResult Amazon30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //60
        [Route("regalo-60-chaquetaplumon")]
        [Route("regalo-60-chaquetaplumon-seleccion")]
        public IActionResult Chaquetaplumon60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-corteingles")]
        [Route("regalo-60-corteingles-seleccion")]
        public IActionResult Corteingles60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-decathlon")]
        [Route("regalo-60-decathlon-seleccion")]
        public IActionResult Decathlon60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante60
        [Route("regalo-60-carburante")]
        [Route("regalo-60-carburante-seleccion")]
        public IActionResult Carburante60()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-chequecarburant")]
        [Route("regalo-60-chequecarburant-seleccion")]
        public IActionResult ChequeCarburante60()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-andorracarburante")]
        [Route("regalo-60-andorracarburante-seleccion")]
        public IActionResult AndorraCarburante60()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-canariascarburante")]
        [Route("regalo-60-canariascarburante-seleccion")]
        public IActionResult CanariasCarburante60()
        {
            var idsession = HttpContext.Session.GetString("idsession");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-amazon")]
        [Route("regalo-60-amazon-seleccion")]
        public IActionResult Amazon60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }
    }
}
