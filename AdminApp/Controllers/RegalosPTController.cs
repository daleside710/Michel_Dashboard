using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminApp.Data;

namespace AdminApp.Controllers
{
    public class RegalosPTController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegalosPTController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //20
        [Route("regalo-20-chuva-pt")]
        [Route("regalo-20-chuva-seleccion-pt")]
        public IActionResult Chuva20()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-corteingles-pt")]
        [Route("regalo-20-corteingles-seleccion-pt")]
        public IActionResult Corteingles20()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-decathlon-pt")]
        [Route("regalo-20-decathlon-seleccion-pt")]
        public IActionResult Decathlon20()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante20
        [Route("regalo-20-combustivel-pt")]
        [Route("regalo-20-combustivel-seleccion-pt")]
        public IActionResult Combustivel20()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-amazon-pt")]
        [Route("regalo-20-amazon-seleccion-pt")]
        public IActionResult Amazon20()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //40
        [Route("regalo-40-chaquetaultra-pt")]
        [Route("regalo-40-chaquetaultra-seleccion-pt")]
        public IActionResult Chaquetaultra40()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-corteingles-pt")]
        [Route("regalo-40-corteingles-seleccion-pt")]
        public IActionResult Corteingles40()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-decathlon-pt")]
        [Route("regalo-40-decathlon-seleccion-pt")]
        public IActionResult Decathlon40()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante40
        [Route("regalo-40-combustivel-pt")]
        [Route("regalo-40-combustivel-seleccion-pt")]
        public IActionResult Combustivel40()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }
        
        [Route("regalo-40-amazon-pt")]
        [Route("regalo-40-amazon-seleccion-pt")]
        public IActionResult Amazon40()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //30
        [Route("regalo-30-chaquetaligera-pt")]
        [Route("regalo-30-chaquetaligera-seleccion-pt")]
        public IActionResult Chaquetaligera30()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-corteingles-pt")]
        [Route("regalo-30-corteingles-seleccion-pt")]
        public IActionResult Corteingles30()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-decathlon-pt")]
        [Route("regalo-30-decathlon-seleccion-pt")]
        public IActionResult Decathlon30()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante30
        [Route("regalo-30-combustivel-pt")]
        [Route("regalo-30-combustivel-seleccion-pt")]
        public IActionResult Combustivel30()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }
        
        [Route("regalo-30-amazon-pt")]
        [Route("regalo-30-amazon-seleccion-pt")]
        public IActionResult Amazon30()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //60
        [Route("regalo-60-chaquetaplumon-pt")]
        [Route("regalo-60-chaquetaplumon-seleccion-pt")]
        public IActionResult Chaquetaplumon60()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-corteingles-pt")]
        [Route("regalo-60-corteingles-seleccion-pt")]
        public IActionResult Corteingles60()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-decathlon-pt")]
        [Route("regalo-60-decathlon-seleccion-pt")]
        public IActionResult Decathlon60()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        //carburante60
        [Route("regalo-60-combustivel-pt")]
        [Route("regalo-60-combustivel-seleccion-pt")]
        public IActionResult Combustivel60()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");

            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }
        
        [Route("regalo-60-amazon-pt")]
        [Route("regalo-60-amazon-seleccion-pt")]
        public IActionResult Amazon60()
        {
            var idsession = HttpContext.Session.GetString("idsession_pt");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }
    }
}
