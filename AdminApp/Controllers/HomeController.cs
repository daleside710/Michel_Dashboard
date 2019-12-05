using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // status box
            
            //ES
            ViewBag.esTotal = _context.Participation.Where(r => r.pais_par.Equals("ES")).ToList().Count;
            
            ViewBag.esTotalSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("ES")).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.esTotalSum == "")
            {
                ViewBag.esTotalSum = "0";
            }

            ViewBag.esValidadas = _context.Participation.Where(r => r.pais_par.Equals("ES") && r.id_est.Equals(2)).ToList().Count;
            ViewBag.esValidadasSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("ES") && r.id_est.Equals(2)).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.esValidadasSum == "")
            {
                ViewBag.esValidadasSum = "0";
            }
            if (ViewBag.esTotal == 0)
            {
                ViewBag.esValidadasPro = 0;
            }
            else
            {
                ViewBag.esValidadasPro = (int)(100 * ViewBag.esValidadas / ViewBag.esTotal);
            }
            
            ViewBag.esPendientes = _context.Participation.Where(r => r.pais_par.Equals("ES") && r.id_est.Equals(1)).ToList().Count;
            ViewBag.esPendientesSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("ES") && r.id_est.Equals(1)).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.esPendientesSum == "")
            {
                ViewBag.esPendientesSum = "0";
            }
            if (ViewBag.esTotal == 0)
            {
                ViewBag.esPendientesPro = 0;
            }
            else
            {
                ViewBag.esPendientesPro = (int)(100 * ViewBag.esPendientes / ViewBag.esTotal);
            }
            
            ViewBag.esRechazadas = _context.Participation.Where(r => r.pais_par.Equals("ES") && r.id_est.Equals(3)).ToList().Count;
            ViewBag.esRechazadasSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("ES") && r.id_est.Equals(3)).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.esRechazadasSum == "")
            {
                ViewBag.esRechazadasSum = "0";
            }
            if (ViewBag.esTotal == 0)
            {
                ViewBag.esRechazadasPro = 0;
            }
            else
            {
                ViewBag.esRechazadasPro = (int)(100 * ViewBag.esRechazadas / ViewBag.esTotal);
            }
            
            //PT
            ViewBag.ptTotal = _context.Participation.Where(r => r.pais_par.Equals("PT")).ToList().Count;
            ViewBag.ptTotalSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("PT")).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.ptTotalSum == "")
            {
                ViewBag.ptTotalSum = "0";
            }

            ViewBag.ptValidadas = _context.Participation.Where(r => r.pais_par.Equals("PT") && r.id_est.Equals(2)).ToList().Count;
            ViewBag.ptValidadasSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("PT") && r.id_est.Equals(2)).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.ptValidadasSum == "")
            {
                ViewBag.ptValidadasSum = "0";
            }
            if (ViewBag.ptTotal == 0)
            {
                ViewBag.ptValidadasPro = 0;
            }
            else
            {
                ViewBag.ptValidadasPro = (int)(100 * ViewBag.ptValidadas / ViewBag.ptTotal);
            }
            

            ViewBag.ptPendientes = _context.Participation.Where(r => r.pais_par.Equals("PT") && r.id_est.Equals(1)).ToList().Count;
            ViewBag.ptPendientesSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("PT") && r.id_est.Equals(1)).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.ptRechazadasSum == "")
            {
                ViewBag.ptPendientesSum = "0";
            }
            if (ViewBag.ptTotal == 0)
            {
                ViewBag.ptPendientesPro = 0;
            }
            else
            {
                ViewBag.ptPendientesPro = (int)(100 * ViewBag.ptPendientes / ViewBag.ptTotal);
            }
            

            ViewBag.ptRechazadas = _context.Participation.Where(r => r.pais_par.Equals("PT") && r.id_est.Equals(3)).ToList().Count;
            ViewBag.ptRechazadasSum = String.Format("{0:#,#}", _context.Participation.Where(r => r.pais_par.Equals("PT") && r.id_est.Equals(3)).Sum(s => s.Numero_ruedas_int));
            if (ViewBag.ptRechazadasSum == "")
            {
                ViewBag.ptRechazadasSum = "0";
            }
            if (ViewBag.ptTotal == 0)
            {
                ViewBag.ptRechazadasPro = 0;
            }
            else
            {
                ViewBag.ptRechazadasPro = (int)(100 * ViewBag.ptRechazadas / ViewBag.ptTotal);
            }
            

            // user role
            var currentUser = await _userManager.GetUserAsync(User);
            
            var role = currentUser.tipo_usu;
            if (role == "2")
            {
                return Redirect("Talleres");
            }
            ViewBag.userRole = role;
            ViewBag.userRegion = currentUser.pais_usu;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
