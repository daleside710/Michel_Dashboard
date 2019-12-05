using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    [Authorize]
    public class TalleresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TalleresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Workshop
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            WorkshopAddViewModel model = new WorkshopAddViewModel { fechaDesde_tall = DateTime.Now };
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Workshop workshop = _context.Workshop.Where(x => x.id_tall.Equals(id)).FirstOrDefault();
            WorkshopEditViewModel model = new WorkshopEditViewModel();
            model.id_tall = workshop.id_tall;
            model.razonsocial_tall = workshop.razonsocial_tall;
            model.alias_tall = workshop.alias_tall;
            model.LC_tall = workshop.LC_tall;
            model.HPDV_tall = workshop.HPDV_tall;
            model.ENSENA_tall = workshop.ENSENA_tall;
            model.direccion_tall = workshop.direccion_tall;
            model.poblacion_tall = workshop.poblacion_tall;
            model.cp_tall = workshop.cp_tall;
            model.provincia_tall = workshop.provincia_tall;
            if(workshop.pais_tall == "ESPAÑA")
            {
                model.pais_tall = "0";
            }else if(workshop.pais_tall == "PORTUGAL")
            {
                model.pais_tall = "1";
            }else if(workshop.pais_tall == "PENINSULA")
            {
                model.pais_tall = "2";
            }
            else
            {
                model.pais_tall = "0";
            }
            
            model.fechaDesde_tall = workshop.fechaDesde_tall;
            model.REGION_tall = workshop.REGION_tall;

            return View(model);
        }


        public IActionResult TodasCSV()
        {
            var comlumHeadrs = new string[]
            {
                "NUM;Razón social;Alias;LC;HPDV;ENSEÑA;Dirección;Población;CP;Provincia;Pais;Desde;Región"
            };


            // get all workshops
            var talleres = (from tempuser in _context.Workshop.Where(x => !x.pais_tall.Contains("BORRADO"))
                             select tempuser);

            var data = talleres.ToList();
            // Build the file content
            var tallerescsv = new StringBuilder();
            data.ForEach(line =>
            {
                var fechaDesde_tall = "";
                if (line.fechaDesde_tall != null)
                {
                    fechaDesde_tall = line.fechaDesde_tall.Value.ToString("dd/MM/yyyy");
                }
                tallerescsv.AppendLine(string.Join(";", line.id_tall,
                    line.razonsocial_tall,
                    line.alias_tall,
                    line.LC_tall,
                    line.HPDV_tall,
                    line.ENSENA_tall,
                    line.direccion_tall,
                    line.poblacion_tall,
                    line.cp_tall,
                    line.provincia_tall,
                    line.pais_tall,
                    fechaDesde_tall,
                    line.REGION_tall
                    ));
            });

            byte[] buffer = Encoding.UTF8.GetBytes($"{string.Join(";", comlumHeadrs)}\r\n{tallerescsv.ToString()}");

            var stream = new MemoryStream();
            var csvWriter = new StreamWriter(stream, Encoding.UTF8);
            csvWriter.WriteLine($"{string.Join(";", comlumHeadrs)}\r\n{tallerescsv.ToString()}");
            csvWriter.Flush();
            return File(stream.ToArray(), "text/csv", "talleres_todas.csv");
        }

    }
}