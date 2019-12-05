using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller
    {
    }


    public class AlertData
    {
        public string product { get; set; }
        public int stock { get; set; }
        public int pendiente { get; set; }
        public int validadas { get; set; }
        public int total { get; set; }
        public int status { get; set; }
    }

    [Produces("application/json")]
    [Route("api/GetAlertDataTable")]
    [Authorize]
    public class GetAlertDataTableController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public GetAlertDataTableController(IHostingEnvironment hostingEnvironment,
            ApplicationDbContext context,
            UserManager<User> userManager,
            IHttpContextAccessor accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        // GET: /api/GetAlertDataTable
        [HttpPost]
        public async Task<IActionResult> GetAlertDataTable()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var alertResult = from participation in _context.Participation
                                      join regalo in _context.Regalo on participation.premioSelFrnt_par equals regalo.id_regalo  into partGroup 
                                      from m in partGroup.DefaultIfEmpty() 
                                      select new
                                      {
                                          premioSelFrnt_par = participation.premioSelFrnt_par,
                                          id_est = participation.id_est,
                                          producto = m.producto,
                                          grupoproducto = m.grupoproducto,
                                          talla_par = participation.talla_par
                                      };

            var alertList = _context.Regalosalert.ToList();
            List<AlertData> alertData = new List<AlertData>();
            alertList.ForEach(line =>
            {
                int talla_Validadas = 0, talla_Pendientes = 0, talla_Rechazadas = 0;

                if (line.isGrupo_ale == 0) //Producto concreto
                {
                    foreach (var v in alertResult)
                    {
                        if (v.producto == line.producto_ale && v.talla_par == line.talla_ale)
                        {
                            if (v.id_est == 1) talla_Pendientes++;
                            if (v.id_est == 2) talla_Validadas++;
                            if (v.id_est == 3) talla_Rechazadas++;
                        }
                    }
                }
                else if (line.isGrupo_ale == 1)
                {
                    foreach (var v in alertResult)
                    {
                        if (v.grupoproducto == line.producto_ale)
                        {
                            if (v.id_est == 1) talla_Pendientes++;
                            if (v.id_est == 2) talla_Validadas++;
                            if (v.id_est == 3) talla_Rechazadas++;
                        }
                    }
                }
                var lLocalStatus = 0;
                if ((int)((talla_Validadas + talla_Pendientes) * 100 /  line.stock_ale) >= 90)
                {
                    lLocalStatus = 2;
                }
                else if ((int)((talla_Validadas + talla_Pendientes) * 100 / line.stock_ale) >= 80 && (int)((talla_Validadas + talla_Pendientes) * 100 / line.stock_ale) < 90)
                {
                    lLocalStatus = 1;
                }
                if (line.isGrupo_ale == 0)
                {
                    alertData.Add(new AlertData()
                    {
                        product = line.producto_ale + " " + line.talla_ale,
                        stock = line.stock_ale,
                        validadas = talla_Validadas,
                        pendiente = talla_Pendientes,
                        total = talla_Validadas + talla_Pendientes,
                        status = lLocalStatus
                    });
                }
                else if (line.isGrupo_ale == 1)
                {
                    alertData.Add(new AlertData()
                    {
                        product = line.producto_ale,
                        stock = line.stock_ale,
                        validadas = talla_Validadas,
                        pendiente = talla_Pendientes,
                        total = talla_Validadas + talla_Pendientes,
                        status = lLocalStatus
                    });
                }   
            });


/*
            alertList.ForEach(line =>
            {
                if (line.producto_ale == "SUDADERA")
                {
                    int lPendiente = 0, lValidadas = 0;
                    int xlPendiente = 0, xlValidadas = 0;
                    foreach (var v in alertResult)
                    {
                        if (line.isGrupo_ale == 0)
                        {
                            if (v.producto == line.producto_ale && v.talla_par == "L")
                            {
                                if (v.id_est == 2)
                                    lValidadas++;
                                if (v.id_est == 1)
                                    lPendiente++;
                            }
                            else if (v.producto == line.producto_ale && v.talla_par == "XL")
                            {
                                if (v.id_est == 2)
                                    xlValidadas++;
                                if (v.id_est == 1)
                                    xlPendiente++;
                            }
                        }
                        else if (line.isGrupo_ale == 1)
                        {
                            if (v.grupoproducto == line.producto_ale && v.talla_par == "L")
                            {
                                if (v.id_est == 2)
                                    lValidadas++;
                                if (v.id_est == 1)
                                    lPendiente++;
                            }
                            else if (v.grupoproducto == line.producto_ale && v.talla_par == "XL")
                            {
                                if (v.id_est == 2)
                                    xlValidadas++;
                                if (v.id_est == 1)
                                    xlPendiente++;
                            }
                        }
                    }
                    var lLocalStatus = 0;
                    if ((int)((lValidadas + lPendiente) * 100 / 50) >= 90)
                    {
                        lLocalStatus = 2;
                    }
                    else if ((int)((lValidadas + lPendiente) * 100 / 50) >= 80 && (int)((lValidadas + lPendiente) * 100 / 50) < 90)
                    {
                        lLocalStatus = 1;
                    }
                    alertData.Add(new AlertData()
                    {
                        product = "SUDADERA L",
                        stock = 50,
                        validadas = lValidadas,
                        pendiente = lPendiente,
                        total = lValidadas + lPendiente,
                        status = lLocalStatus
                    });
                    var xlLocalStatus = 0;
                    if ((int)((xlValidadas + xlPendiente) * 100 / 50) >= 90)
                    {
                        xlLocalStatus = 2;
                    }
                    else if ((int)((xlValidadas + xlPendiente) * 100 / 50) >= 80 && (int)((xlValidadas + xlPendiente) * 100 / 50) < 90)
                    {
                        xlLocalStatus = 1;
                    }
                    alertData.Add(new AlertData()
                    {
                        product = "SUDADERA XL",
                        stock = 50,
                        validadas = xlValidadas,
                        pendiente = xlPendiente,
                        total = xlValidadas + xlPendiente,
                        status = xlLocalStatus
                    });
                }
                else if (line.producto_ale == "CHAQUETA")
                {
                    int lPendiente = 0, lValidadas = 0;
                    int xlPendiente = 0, xlValidadas = 0;
                    foreach (var v in alertResult)
                    {
                        if (line.isGrupo_ale == 0)
                        {
                            if (v.producto == line.producto_ale && v.talla_par == "L")
                            {
                                if (v.id_est == 2)
                                    lValidadas++;
                                if (v.id_est == 1)
                                    lPendiente++;
                            }
                            else if (v.producto == line.producto_ale && v.talla_par == "XL")
                            {
                                if (v.id_est == 2)
                                    xlValidadas++;
                                if (v.id_est == 1)
                                    xlPendiente++;
                            }
                        }
                        else if (line.isGrupo_ale == 1)
                        {
                            if (v.grupoproducto == line.producto_ale && v.talla_par == "L")
                            {
                                if (v.id_est == 2)
                                    lValidadas++;
                                if (v.id_est == 1)
                                    lPendiente++;
                            }
                            else if (v.grupoproducto == line.producto_ale && v.talla_par == "XL")
                            {
                                if (v.id_est == 2)
                                    xlValidadas++;
                                if (v.id_est == 1)
                                    xlPendiente++;
                            }
                        }
                    }
                    var lLocalStatus = 0;
                    if ((int)((lValidadas + lPendiente) * 100 / 50) >= 90)
                    {
                        lLocalStatus = 2;
                    }
                    else if ((int)((lValidadas + lPendiente) * 100 / 50) >= 80 && (int)((lValidadas + lPendiente) * 100 / 50) < 90)
                    {
                        lLocalStatus = 1;
                    }
                    alertData.Add(new AlertData()
                    {
                        product = "CHAQUETA L",
                        stock = 50,
                        validadas = lValidadas,
                        pendiente = lPendiente,
                        total = lValidadas + lPendiente,
                        status = lLocalStatus
                    });
                    var xlLocalStatus = 0;
                    if ((int)((xlValidadas + xlPendiente) * 100 / 50) >= 90)
                    {
                        xlLocalStatus = 2;
                    }
                    else if ((int)((xlValidadas + xlPendiente) * 100 / 50) >= 80 && (int)((xlValidadas + xlPendiente) * 100 / 50) < 90)
                    {
                        xlLocalStatus = 1;
                    }
                    alertData.Add(new AlertData()
                    {
                        product = "CHAQUETA XL",
                        stock = 50,
                        validadas = xlValidadas,
                        pendiente = xlPendiente,
                        total = xlValidadas + xlPendiente,
                        status = xlLocalStatus
                    });
                }
                else
                {
                    int pendiente = 0, validadas = 0;
                    foreach (var v in alertResult)
                    {
                        if (line.isGrupo_ale == 0)
                        {
                            if (v.producto == line.producto_ale)
                            {
                                if (v.id_est == 2)
                                    validadas++;
                                if (v.id_est == 1)
                                    pendiente++;
                            }
                        }
                        else if (line.isGrupo_ale == 1)
                        {
                            if (v.grupoproducto == line.producto_ale)
                            {
                                if (v.id_est == 2)
                                    validadas++;
                                if (v.id_est == 1)
                                    pendiente++;
                            }
                        }
                    }
                    var localStatus = 0;
                    if ((int)((validadas + pendiente) * 100 / line.stock_ale) >= 90)
                    {
                        localStatus = 2;
                    }
                    else if ((int)((validadas + pendiente) * 100 / line.stock_ale) >= 80 && (int)((validadas + pendiente) * 100 / line.stock_ale) < 90)
                    {
                        localStatus = 1;
                    }
                    alertData.Add(new AlertData()
                    {
                        product = line.producto_ale,
                        stock = line.stock_ale,
                        validadas = validadas,
                        pendiente = pendiente,
                        total = validadas + pendiente,
                        status = localStatus
                    });
                }
            });


            */

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            //Sorting 
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                switch (sortColumn)
                {
                    case "product":
                        if (sortColumnDirection == "asc")
                        {
                            alertData = alertData.OrderBy(alert => alert.product).ToList();
                        }
                        else
                        {
                            alertData = alertData.OrderByDescending(alert => alert.product).ToList();
                        }
                        break;
                    case "stock":
                        if (sortColumnDirection == "asc")
                        {
                            alertData = alertData.OrderBy(alert => alert.stock).ToList();
                        }
                        else
                        {
                            alertData = alertData.OrderByDescending(alert => alert.stock).ToList();
                        }
                        break;
                    case "pendiente":
                        if (sortColumnDirection == "asc")
                        {
                            alertData = alertData.OrderBy(alert => alert.pendiente).ToList();
                        }
                        else
                        {
                            alertData = alertData.OrderByDescending(alert => alert.pendiente).ToList();
                        }
                        break;
                    case "validadas":
                        if (sortColumnDirection == "asc")
                        {
                            alertData = alertData.OrderBy(alert => alert.validadas).ToList();
                        }
                        else
                        {
                            alertData = alertData.OrderByDescending(alert => alert.validadas).ToList();
                        }
                        break;
                    case "total":
                        if (sortColumnDirection == "asc")
                        {
                            alertData = alertData.OrderBy(alert => alert.total).ToList();
                        }
                        else
                        {
                            alertData = alertData.OrderByDescending(alert => alert.total).ToList();
                        }
                        break;
                    case "status":
                        if (sortColumnDirection == "asc")
                        {
                            alertData = alertData.OrderBy(alert => alert.status).ToList();
                        }
                        else
                        {
                            alertData = alertData.OrderByDescending(alert => alert.status).ToList();
                        }
                        break;
                }
            }

            var data = alertData;
            int recordsTotal = alertData.Count;
            int recordsFiltered = recordsTotal;
            return Json(new { data, draw, recordsFiltered, recordsTotal });
        }
    }


}