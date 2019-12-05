using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    [Authorize]
    public class LocalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add()
        {
            WorkshopAddViewModel model = new WorkshopAddViewModel { fechaDesde_tall = DateTime.Now };
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}