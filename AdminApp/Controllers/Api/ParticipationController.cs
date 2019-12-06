using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;

namespace AdminApp.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Participaciones")]
    [Authorize]
    public class ParticipationController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public ParticipationController(IHostingEnvironment hostingEnvironment,
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

        // POST: api/Participationes
        [HttpPost]
        public async Task<IActionResult> GetParticipationes()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skip number of Rows count
            var start = HttpContext.Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = HttpContext.Request.Form["length"].FirstOrDefault();
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            // Sort Column Direction (asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box) 
            var searchValue = HttpContext.Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;

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
            var participationes = (from tempuser in _context.Participation.Include(x => x.Regalo).Where(x => country != "" ? x.pais_par.Equals(country) : true)
                                   select tempuser);

            //Sorting 
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                switch (sortColumn)
                {
                    case "id_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.id_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.id_par);
                        }
                        break;
                    case "registrofecha_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.registrofecha_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.registrofecha_par);
                        }
                        break;
                    case "registrohora_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.registrohora_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.registrohora_par);
                        }
                        break;
                    case "nombre_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.nombre_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.nombre_par);
                        }
                        break;
                    case "apellidos_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.apellidos_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.apellidos_par);
                        }
                        break;
                    case "telefono_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.telefono_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.telefono_par);
                        }
                        break;
                    case "email_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.email_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.email_par);
                        }
                        break;
                    case "dni_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.dni_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.dni_par);
                        }
                        break;
                    case "premioSelFrnt_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => (x.valorpremio_par == null) ? _context.Regalo.Single(r => r.id_regalo.Equals(x.premioSelFrnt_par)).producto : x.valorpremio_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => (x.valorpremio_par == null) ? _context.Regalo.Single(r => r.id_regalo.Equals(x.premioSelFrnt_par)).producto : x.valorpremio_par);
                        }
                        break;
                    case "pais_par":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(x => x.pais_par);
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(x => x.pais_par);
                        }
                        break;
                    case "id_est":
                        if (sortColumnDirection == "asc")
                        {
                            participationes = participationes.OrderBy(m => m.id_est.Equals(3) ? "Rechazado" : m.id_est.Equals(2) ? "Validado" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(1) && m.solicitar_adjunto.Equals(1) ? "Actualizando" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(0) && m.solicitar_adjunto.Equals(1) ? "Modificando" : m.id_est.Equals(1) ? "Pendiente" : "");
                        }
                        else
                        {
                            participationes = participationes.OrderByDescending(m => m.id_est.Equals(3) ? "Rechazado" : m.id_est.Equals(2) ? "Validado" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(1) && m.solicitar_adjunto.Equals(1) ? "Actualizando" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(0) && m.solicitar_adjunto.Equals(1) ? "Modificando" : m.id_est.Equals(1) ? "Pendiente" : "");
                        }
                        break;
                }
            }

            // Search
            //  || m.registrohora_par.Value.ToString().Contains(searchValue) //
            //|| ((m.valorpremio_par == null) ? (_context.Regalo.Where(x => x.id_regalo.Equals(m.premioSelFrnt_par.Value)).FirstOrDefault() == null ? "" :
            // _context.Regalo.Where(x => x.id_regalo.Equals(m.premioSelFrnt_par.Value)).FirstOrDefault().producto) : m.valorpremio_par).ToUpper()
            //       .Contains(searchValue.ToUpper())  
            //no funciona  || m.valorpremio_par.ToUpper().Contains(searchValue.ToUpper())  
            string filter = HttpContext.Request.Form["filter"].FirstOrDefault();
            if (!string.IsNullOrEmpty(searchValue))
            {
                switch (filter)
                {
                    case "todos":
                        participationes = participationes.Where(
                            m => m.nombre_par.ToUpper().Contains(searchValue.ToUpper())
                            || m.apellidos_par.ToUpper().Contains(searchValue.ToUpper())
                            || m.id_par.ToString().Contains(searchValue) // 
                            || m.registrofecha_par.Value.ToString("dd/MM/yyyy").Contains(searchValue)
                            || m.registrohora_par.Value.ToString().Contains(searchValue)
                            || m.telefono_par.Contains(searchValue)
                            || m.dni_par.ToUpper().Contains(searchValue.ToUpper())
                            || m.email_par.ToUpper().Contains(searchValue.ToUpper())
                            || m.pais_par.ToUpper().Contains(searchValue.ToUpper())
                            || ((m.valorpremio_par == null) ? ("-") : m.valorpremio_par).ToUpper().Contains(searchValue.ToUpper())
                            || (m.id_est.Equals(3) ? "Rechazado" : m.id_est.Equals(2) ? "Validado" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(1) && m.solicitar_adjunto.Equals(1) ? "Actualizando" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(0) && m.solicitar_adjunto.Equals(1) ? "Modificando" : m.id_est.Equals(1) ? "Pendiente" : "").ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                    case "id_par":
                        participationes = participationes.Where(
                            m => m.id_par.ToString().Contains(searchValue) // 
                        );
                        break;
                    case "registrofecha_par":
                        participationes = participationes.Where(
                            m => m.registrofecha_par.Value.ToString("dd/MM/yyyy").Contains(searchValue)
                        );
                        break;
                    case "registrohora_par":
                        participationes = participationes.Where(
                            m => m.registrohora_par.Value.ToString().Contains(searchValue)
                        );
                        break;
                    case "nombre_par":
                        participationes = participationes.Where(
                            m => m.nombre_par.ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                    case "apellidos_par":
                        participationes = participationes.Where(
                            m => m.apellidos_par.ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                    case "telefono_par":
                        participationes = participationes.Where(
                            m => m.telefono_par.Contains(searchValue)
                        );
                        break;
                    case "email_par":
                        participationes = participationes.Where(
                            m => m.email_par.ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                    case "dni_par":
                        participationes = participationes.Where(
                            m => m.dni_par.ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                    case "regalo_par":
                        participationes = participationes.Where(
                            m => ((m.valorpremio_par == null) ? ("-") : m.valorpremio_par).ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                    case "pais_par":
                        participationes = participationes.Where(
                            m => m.pais_par.ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                    case "estado_par":
                        participationes = participationes.Where(
                            m => (m.id_est.Equals(3) ? "Rechazado" : m.id_est.Equals(2) ? "Validado" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(1) && m.solicitar_adjunto.Equals(1) ? "Actualizando" : m.id_est.Equals(1) && m.adjunto_adjunto.Equals(0) && m.solicitar_adjunto.Equals(1) ? "Modificando" : m.id_est.Equals(1) ? "Pendiente" : "").ToUpper().Contains(searchValue.ToUpper())
                        );
                        break;
                }
            }

            //total number of rows counts
            recordsTotal = participationes.Count();
            //Paging   
            var data = participationes.Skip(skip).Take(pageSize).ToList();


            int recordsFiltered = recordsTotal;
            return Json(new { data, draw, recordsFiltered, recordsTotal });
        }

        // POST: api/Participaciones/5
        [HttpPost("{id}")]
        public async Task<IActionResult> EditParticipation([FromRoute] int id, [FromBody] ParticipationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.id_par)
            {
                return BadRequest();
            }
            try
            {
                var msg = "";
                var currentUser = await _userManager.GetUserAsync(User);

                var participation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == model.id_par);
                if (participation == null)
                {
                    return NotFound();
                }

                // Two participaciones validated cant have the same factura1 or factura2 with the same id_tall
                var id_tall = participation.id_tall;
                var factura1_par = model.factura1_par;
                var factura2_par = model.factura2_par;

                int validated_id_par = 0;

                if (factura1_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(factura1_par) || p.factura2_par.Equals(factura1_par)));
                    if (tmp.Count() > 0)
                    {
                        validated_id_par = tmp.FirstOrDefault().id_par;
                    }
                }

                if (factura2_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(factura2_par) || p.factura2_par.Equals(factura2_par)));
                    if (tmp.Count() > 0)
                    {
                        validated_id_par = tmp.FirstOrDefault().id_par;
                    }
                }
               
                if (validated_id_par != 0)
                {
                    msg = "duplicate";
                    // log for validate
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "id_par: " + id + ", " +
                            "action: " + "Actualizar Información" + ", " +
                            "result: Factura validada para el mismo taller en la participación #" + validated_id_par
                            );
                    }
                    return Json(new { success = false, message = msg, validated_id_par });
                }
                

                participation.email_par = model.email_par;
                participation.dni_par = model.dni_par;
                participation.telefono_par = model.telefono_par;
                participation.nombre_par = model.nombre_par;
                participation.apellidos_par = model.apellidos_par;
                participation.factura1_par = model.factura1_par;
                participation.factura2_par = model.factura2_par;
                participation.fechaCompra1_par = model.fechaCompra1_par;
                participation.fechaCompra2_par = model.fechaCompra2_par;
                participation.llanta = model.llanta;
                participation.TamanoRueda = model.TamanoRueda;
                participation.medidallanta_par = model.medidallanta_par;
                participation.numllantas_par = model.numllantas_par;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                participation.fechaM_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                participation.usuM_par = currentUser.Id; // user who modifes
                participation.usuariolocalidad_par = model.usuariolocalidad_par;
                participation.usuarioprovincia_par = model.usuarioprovincia_par;

                _context.Update(participation);

                _context.SaveChanges();

                // log for main edit action
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Edit Participación" + ", " +
                        "result: " + "Participación actualizada con éxito."
                        );
                }

                return Json(new { success = true, message = "Participación actualizada con éxito." });
            }
            catch (Exception ex)
            {
                // log for main edit action
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Edit Participación" + ", " +
                        "result: " + "Fail."
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    public class RegStat
    {
        public string date { get; set; }
        public int esCount { get; set; }
        public int ptCount { get; set; }
    }



    [Produces("application/json")]
    [Route("api/DashboardRegStat")]
    [Authorize]
    public class DashboardRegStatController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public DashboardRegStatController(IHostingEnvironment hostingEnvironment,
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

        // GET: /api/DashboardRegStat
        [HttpGet]
        public async Task<IActionResult> DashboardRegStat()
        {
            // registration graph
            var regList = _context.Participation.ToList();
            List<RegStat> regStat = new List<RegStat>();
            regList.ForEach(line =>
            {
                var obj = regStat.FirstOrDefault(r => r.date.Equals(line.registrofecha_par.Value.ToString("dd/MM/yyyy")));
                if (obj != null)
                {
                    if (line.pais_par.Equals("ES"))
                    {
                        obj.esCount = obj.esCount + 1;
                    }
                    else if (line.pais_par.Equals("PT"))
                    {
                        obj.ptCount = obj.ptCount + 1;
                    }
                }
                else
                {
                    if (line.pais_par.Equals("ES"))
                    {
                        regStat.Add(new RegStat() { date = line.registrofecha_par.Value.ToString("dd/MM/yyyy"), esCount = 1, ptCount = 0 });
                    }
                    else if (line.pais_par.Equals("PT"))
                    {
                        regStat.Add(new RegStat() { date = line.registrofecha_par.Value.ToString("dd/MM/yyyy"), esCount = 0, ptCount = 1 });
                    }
                }
            });

            return Json(regStat);
        }
    }

   

   
    [Produces("application/json")]
    [Route("api/ChangeEmail")]
    [Authorize]
    public class ChangeEmailController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public ChangeEmailController(IHostingEnvironment hostingEnvironment,
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

        // POST: /api/ChangeEmail/5
        [HttpPost("{id}")]
        public async Task<IActionResult> ChangeEmail([FromRoute] int id, [FromBody] dynamic participation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id_par = participation.id_par;

            if (id != id_par)
            {
                return BadRequest();
            }
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var updateParticipation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id_par);
                if (updateParticipation == null)
                {
                    return NotFound();
                }

                updateParticipation.email_par = participation.email_par;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                updateParticipation.fechaM_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                updateParticipation.usuM_par = currentUser.Id; // user who modifes

                _context.Update(updateParticipation);

                _context.SaveChanges();

                var comment = (participation.create == 0)? updateParticipation.motivo_adj_factura ?? "" : updateParticipation.motivo_adj_datos ?? "";

                // log for email edit action
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Edit email" + ", " +
                        "result: " + "Email actualizado con éxito."
                        );
                }
                return Json(new { success = true, message = "Email actualizado con éxito.", comment = comment });
            }
            catch (Exception ex)
            {
                // log for email edit action
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Edit email" + ", " +
                        "result: " + "Fail."
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }
    }


    [Produces("application/json")]
    [Route("api/ChangeTalleres")]
    [Authorize]
    public class ChangeTalleresController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public ChangeTalleresController(IHostingEnvironment hostingEnvironment,
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

        // POST: /api/ChangeTalleres/5
        [HttpPost("{id}")]
        public async Task<IActionResult> ChangeTalleres([FromRoute] int id, [FromBody] ParticipationEditViewModel participation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participation.id_par)
            {
                return BadRequest();
            }
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var updateParticipation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == participation.id_par);
                if (updateParticipation == null)
                {
                    return NotFound();
                }
                if(participation.id_tall != null)
                    updateParticipation.id_tall = participation.id_tall;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                updateParticipation.fechaM_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                updateParticipation.usuM_par = currentUser.Id; // user who modifes

                _context.Update(updateParticipation);

                _context.SaveChanges();

                // log for email edit action
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Edit id_tall" + ", " +
                        "result: " + "Taller actualizado con éxito."
                        );
                }
                return Json(new { success = true, message = "Taller actualizado con éxito." });
            }
            catch (Exception ex)
            {
                // log for email edit action
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Edit id_tall" + ", " +
                        "result: " + "Edit Taller Fail."
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    [Produces("application/json")]
    [Route("api/SendEmail")]
    [Authorize]
    public class SendEmailController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public SendEmailController(IHostingEnvironment hostingEnvironment,
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

        // POST: /api/SendEmail
        
        [HttpPost("{id}")]
        public async Task<IActionResult> SendEmail([FromRoute] int id, [FromBody] dynamic data)
        {
            string tmp = id + "";
            int ID = int.Parse(tmp.Substring(0, tmp.Length - 1));
            int con = int.Parse(tmp.Substring(tmp.Length-1));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updateParticipation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == ID);
                if (updateParticipation == null)
                {
                    return NotFound();
                }

                var DOMAIN01 = "DOMAIN01";
                var DOMAIN02 = "DOMAIN02";

                DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
                DOMAIN02 = lines[5].Replace("DOMAIN02: ", "");

                updateParticipation.adjunto_adjunto = 0;
                updateParticipation.fecha_hora_adjunto_adjunto = null;
                updateParticipation.solicitar_adjunto = 1;

                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                updateParticipation.fecha_hora_solicitar_adjunto = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);

                string commentEnvDate = updateParticipation.fecha_hora_solicitar_adjunto?.ToString("dd'/'MM'/'yyyy HH:mm:ss");
                string commentDate = "";

                if (con == 0)
                {
                    updateParticipation.motivo_adj_factura = data.comment;
                    updateParticipation.fechaEnv_adj_factura = updateParticipation.fecha_hora_solicitar_adjunto;
                    commentDate = updateParticipation.fecha_adj_factura?.ToString("dd'/'MM'/'yyyy HH:mm:ss");
                }
                else
                {
                    updateParticipation.motivo_adj_datos = data.comment;
                    updateParticipation.fechaEnv_adj_datos = updateParticipation.fecha_hora_solicitar_adjunto;
                    commentDate = updateParticipation.fecha_adj_datos?.ToString("dd'/'MM'/'yyyy HH:mm:ss");
                }

                _context.Update(updateParticipation);

                await _context.SaveChangesAsync();

                var pais_par = updateParticipation.pais_par;
                var url_par = updateParticipation.url_par;

                var url = "test";
                var pieza = "";
                var asunto = "";
                Random random = new Random();

                if (pais_par == "ES")
                {
                    url = DOMAIN01 + "/mod-adjuntar?u=" + url_par + "&xa=" + random.Next();
                    pieza = "97974";
                    asunto = "Solicitud de  factura";
                    if (con == 1)
                    {
                        url = DOMAIN01 + "/mod-adjuntar-info?u=" + url_par + "&xa=" + random.Next();
                        pieza = "97972";
                        asunto = "Solicitud Datos Factura";
                    }

                }
                else // PT
                {
                    url = DOMAIN02 + "/mod-adjuntar?u=" + url_par + "&xa=" + random.Next();
                    pieza = "97976";
                    asunto = "Documento ilegível";
                    if (con == 1)
                    {
                        url = DOMAIN02 + "/mod-adjuntar-info?u=" + url_par + "&xa=" + random.Next();
                        pieza = "97977";
                        asunto = "Dados da factura";
                    }
                }

                var cemail = updateParticipation.email_par;
                var nombre = updateParticipation.nombre_par;
                var dato_20 = "Campaña Michelin";
                var dato_18 = "";
                var result = await teenvio_actualizar_contacto_url(cemail, pieza, asunto, nombre, dato_20, url, dato_18, "", "", pais_par);
                if (result)
                {
                    // log for Solicitar Reenvio Factura No Legible
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        if (con != 1)
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_par: " + id + ", " +
                                "action: " + "Solicitar Reenvio Factura No Legible" + ", " +
                                "result: " + "Success !"
                                );
                        }
                        else
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_par: " + id + ", " +
                                "action: " + "Solicitar Datos Factura" + ", " +
                                "result: " + "Success !"
                                );
                        }
                    }
                    return Json(new { success = true, message = "Solicitud de adjunto enviada con éxito.",
                        comment = data.comment, env_date = commentEnvDate, date = commentDate });
                }
                else
                {
                    // log for Solicitar Reenvio Factura No Legible
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        if (con != 1)
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_par: " + id + ", " +
                                "action: " + "Solicitar Reenvio Factura No Legible" + ", " +
                                "result: " + "Fail."
                                );
                        }
                        else
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_par: " + id + ", " +
                                "action: " + "Solicitar Datos Factura" + ", " +
                                "result: " + "Fail."
                                );
                        }
                    }
                    return Json(new { success = false, message = "La solicitud de archivo adjunto enviada falla." });
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<bool> teenvio_actualizar_contacto_url(String cemail, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String dato_20, String url, String dato_18, String link_bbll, String link_priv, String pais)
        {
            var action = "contact_save";
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }

            multipartContent.Add(new StringContent(cemail), "email");
            multipartContent.Add(new StringContent(dato_20), "dato_20");
            multipartContent.Add(new StringContent(url), "dato_19");
            multipartContent.Add(new StringContent(dato_18), "dato_18");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var contact_id = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                contact_id = contact_id.Replace("OK: ", "");
                if (await teenvio_enviar_pieza_url(contact_id, pieza_teenvio, asunto_teenvio, nombre_teenvio, url, link_bbll, link_priv, pais))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }

        public async Task<bool> teenvio_enviar_pieza_url(String contact_id, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String url, String link_bbll, String link_priv, String pais)
        {
            var action = "send_campaign";

            //var vars = Json(new { var_url = url, link_bbll = link_bbll, link_priv = link_priv, link_contacto = "link_contacto" });
            string vars = "{\"var_url\":\"" + url + "\",\"link_bbll\":\"" + link_bbll + "\",\"link_priv\":\"" + link_priv + "\",\"link_contacto\":\"test_contact\"}";

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }
            multipartContent.Add(new StringContent(contact_id), "contact_id");
            multipartContent.Add(new StringContent(nombre_teenvio), "name");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            multipartContent.Add(new StringContent(asunto_teenvio), "subject");
            multipartContent.Add(new StringContent(vars, Encoding.UTF8, "application/json"), "vars");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var id_envio = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                id_envio = contact_id.Replace("OK: ", "");
                return true;
            }
            return false;
        }
    }

    [Produces("application/json")]
    [Route("api/Validar")]
    [Authorize]
    public class ValidarController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        private string uploadFoler;

        public ValidarController(IHostingEnvironment hostingEnvironment,
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

            uploadFoler = lines[6].Replace("UPLOAD_URL: ", "");
        }

        // POST: /api/Validar/5
        [HttpPost("{id}")]
        public async Task<IActionResult> Validar([FromRoute] int id, [FromBody] ParticipationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.id_par)
            {
                return BadRequest();
            }
            try
            {
                var msg = "";
                var currentUser = await _userManager.GetUserAsync(User);
                var participation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id);
                if (participation == null)
                {
                    return NotFound();
                }

                // Two participaciones validated cant have the same factura1 or factura2 with the same id_tall
                var id_tall = participation.id_tall;
                var factura1_par = model.factura1_par;
                var factura2_par = model.factura2_par;

                int validated_id_par = 0;

                if (factura1_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(factura1_par) || p.factura2_par.Equals(factura1_par)));
                    if (tmp.Count() > 0)
                    {
                        validated_id_par = tmp.FirstOrDefault().id_par;
                    }
                }

                if (factura2_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(factura2_par) || p.factura2_par.Equals(factura2_par)));
                    if (tmp.Count() > 0)
                    {
                        validated_id_par = tmp.FirstOrDefault().id_par;
                    }
                }

                if (validated_id_par != 0)
                {
                    msg = "duplicate";

                    // log for validate
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "id_par: " + id + ", " +
                            "action: " + "Validate" + ", " +
                            "result: Factura validada para el mismo taller en la participación #" + validated_id_par
                            );
                    }

                    participation.motivo_par = ".Número factura repetido en participación " + validated_id_par;
                    _context.Update(participation);
                    await _context.SaveChangesAsync();

                    return Json(new { success = false, message = msg, validated_id_par });
                }
                
                participation.email_par = model.email_par;
                participation.dni_par = model.dni_par;
                participation.telefono_par = model.telefono_par;
                participation.nombre_par = model.nombre_par;
                participation.apellidos_par = model.apellidos_par;
                participation.factura1_par = model.factura1_par;
                participation.factura2_par = model.factura2_par;
                participation.fechaCompra1_par = model.fechaCompra1_par;
                participation.fechaCompra2_par = model.fechaCompra2_par;
                participation.llanta = model.llanta;
                participation.TamanoRueda = model.TamanoRueda;
                participation.medidallanta_par = model.medidallanta_par;
                participation.numllantas_par = model.numllantas_par;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                participation.fechaM_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                participation.usuM_par = currentUser.Id; // user who modifes
                participation.usuariolocalidad_par = model.usuariolocalidad_par;
                participation.usuarioprovincia_par = model.usuarioprovincia_par;

                //20191125
                var producto = _context.Regalo.Where(r => r.id_regalo.Equals(participation.premioSelFrnt_par)).FirstOrDefault().producto;
                var grupoproducto = _context.Regalo.Where(r => r.id_regalo.Equals(participation.premioSelFrnt_par)).FirstOrDefault().grupoproducto;
                var talla = participation.talla_par;
        
                _context.SaveChanges();

                // check limit by email/dni
                var mywheels = participation.Numero_ruedas_int; //mywheels
                var dni_par = participation.dni_par;
                var email_par = participation.email_par;
               
                var general = _context.General.SingleOrDefault();
                var chckdni_gen = general.chckdni_gen;
                var chckpremios_gen = general.chckpremios_gen;
                var chckemail_gen = general.chckemail_gen;

                var nummaxdni_gen = general.nummaxdni_gen;
                var nummaxemail_gen = general.nummaxemail_gen;
                var nummaxpremios_gen = general.nummaxpremios_gen;

                if (chckdni_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.Where(x => x.id_est.Equals(2) && x.dni_par.ToUpper().Equals(dni_par.ToUpper()));
                    //var test = _context.Participation.Where(x => x.id_est.Equals(2) && x.dni_par.Equals(dni_par)).Count();
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxdni_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por DNI";
                        // log for validate
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_par: " + id + ", " +
                                "action: " + "Validate" + ", " +
                                "result: limits of number wheel depends of dni / email"
                                );
                        }
                        return Json(new { success = false, message = msg });
                    }
                }

                if (chckemail_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.Where(x => x.id_est.Equals(2) && x.email_par.ToUpper().Equals(email_par.ToUpper()));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxemail_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por EMAIL";
                        // log for validate
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_par: " + id + ", " +
                                "action: " + "Validate" + ", " +
                                "result: limits of number wheel depends of dni / email"
                                );
                        }
                        return Json(new { success = false, message = msg });
                    }
                }

                if (chckpremios_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.Where(x => x.id_est.Equals(2));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxpremios_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido PROMO";
                        // log for validate
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_par: " + id + ", " +
                                "action: " + "Validate" + ", " +
                                "result: limits of number wheel depends of dni / email"
                                );
                        }
                        return Json(new { success = false, message = msg });
                    }
                }

                // check limit by talla (L or XL)
                if (grupoproducto != null)
                {
                    var a = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto)).Count();
                   // var regaloslimite = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto)).SingleOrDefault();
                    var regaloslimite = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto)).FirstOrDefault();
                    if (regaloslimite != null)
                    {
                        bool hay_registro = false;
                        var b = regaloslimite.unidades_lim;
                        if (regaloslimite.especial == "0") //Es GrupoProductoFisico sin tallas
                        {
                            if (a >= b)
                            {
                                msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                                // log for form
                                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                                {
                                    sw.WriteLine(
                                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                        "id_par: " + id + ", " +
                                        "id_tall: " + id_tall + ", " +
                                        "email: " + email_par + ", " +
                                        "dni: " + dni_par + ", " +
                                        "action: " + "validate form" + ", " +
                                        "result: " + grupoproducto + " in case units are over"
                                        );
                                }
                                return Json(new { success = false, message = msg, limit = true });
                            }
                            else
                            {
                                hay_registro = true;
                            }
                        }
                        else
                        {
                            //Control talla
                            var validated_talla_count = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto) && x.talla_par.Equals(talla)).Count();
                            var regaloslimite_talla = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto) && x.producto.Equals(producto) && x.dimension.Equals(talla)).SingleOrDefault();
                            var regaloslimite_talla_count = regaloslimite_talla.unidades_lim;
                            if (validated_talla_count >= regaloslimite_talla_count)
                            {
                                msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                                // log for form
                                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                                {
                                    sw.WriteLine(
                                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                        "id_par: " + id + ", " +
                                        "id_tall: " + id_tall + ", " +
                                        "email: " + email_par + ", " +
                                        "dni: " + dni_par + ", " +
                                        "action: " + "validate form" + ", " +
                                        "result: " + grupoproducto + " in case units are over"
                                        );
                                }
                                return Json(new { success = false, message = msg, limit = true });
                            }
                            else
                            {
                                hay_registro = true;
                            }

                        }

                        if (hay_registro == false)
                        {
                            msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                            // log for form
                            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                            {
                                sw.WriteLine(
                                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                    "id_par: " + id + ", " +
                                    "id_tall: " + id_tall + ", " +
                                    "email: " + email_par + ", " +
                                    "dni: " + dni_par + ", " +
                                    "action: " + "validate form" + ", " +
                                    "result: " + grupoproducto + " error control limits"
                                    );
                            }
                            return Json(new { success = false, message = msg, limit = true });
                        }
                    }

                }

                // log for form
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                       "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                       "email: " + participation.email_par + ", " +
                       "dni: " + participation.dni_par + ", " +
                       "telefono: " + participation.telefono_par + ", " +
                       "nombre: " + participation.nombre_par + ", " +
                       "apellidos: " + participation.apellidos_par + ", " +
                       "factura1: " + participation.factura1_par + ", " +
                       "factura2: " + participation.factura2_par + ", " +
                       "fechaCompra1: " + participation.fechaCompra1_par + ", " +
                       "fechaCompra2: " + participation.fechaCompra2_par + ", " +
                       "llanta: " + participation.llanta + ", " +
                       "TamanoRueda: " + participation.TamanoRueda + ", " +
                       "medidallanta " + participation.medidallanta_par + ", " +
                       "numllantas: " + participation.numllantas_par + ", " +
                       "usuariolocalidad: " + participation.usuariolocalidad_par + ", " +
                       "usuarioprovincia: " + participation.usuarioprovincia_par + ", " +
                       "action: " + "validate form" + ", " +
                       "result: success! " );
                }

                return Json(new { success = true, message = "ok" });
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id: " + id + 
                        "action: " + "validate form" + ", " +
                        "result: " + ex.Message
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }

    }

    [Produces("application/json")]
    [Route("api/AttachmentValidar")]
    [Authorize]
    public class AttachmentValidarController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        private string uploadRootFolder;

        public AttachmentValidarController(IHostingEnvironment hostingEnvironment,
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

            uploadRootFolder = lines[6].Replace("UPLOAD_URL: ", "");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AttachmentValidar([FromRoute] int id, [FromBody] JObject param)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var participation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id);
                if (participation == null)
                {
                    return NotFound();
                }

                var files = new List<Attachment>();
                // adjunto
                if (string.IsNullOrEmpty(param["adjunto1_par"].Value<string>()))
                {
                    participation.adjunto1_par = null;
                }
                else
                {
                    participation.adjunto1_par = moveTempFile(id, participation.url_par, 1, param["adjunto1_par"].Value<string>());
                    if (string.IsNullOrEmpty(participation.adjunto1_par))
                    {
                        return Json(new { success = false, message = "adjunto1 verify failed." });
                    }

                    files.Add(new Attachment { label = GetOriginFileName(param["adjunto1_par"].Value<string>()), filename = participation.adjunto1_par });
                }

                if (string.IsNullOrEmpty(param["adjunto2_par"].Value<string>()))
                {
                    participation.adjunto2_par = null;
                }
                else
                {
                    participation.adjunto2_par = moveTempFile(id, participation.url_par, 2, param["adjunto2_par"].Value<string>());
                    if (string.IsNullOrEmpty(participation.adjunto2_par))
                    {
                        return Json(new { success = false, message = "adjunto2 verify failed." });
                    }

                    files.Add(new Attachment { label = GetOriginFileName(param["adjunto2_par"].Value<string>()), filename = participation.adjunto2_par });
                }

                if (string.IsNullOrEmpty(param["adjunto3_par"].Value<string>()))
                {
                    participation.adjunto3_par = null;
                }
                else
                {
                    participation.adjunto3_par = moveTempFile(id, participation.url_par, 3, param["adjunto3_par"].Value<string>());
                    if (string.IsNullOrEmpty(participation.adjunto3_par))
                    {
                        return Json(new { success = false, message = "adjunto3 verify failed." });
                    }

                    files.Add(new Attachment { label = GetOriginFileName(param["adjunto3_par"].Value<string>()), filename = participation.adjunto3_par });
                }

                if (string.IsNullOrEmpty(param["adjunto4_par"].Value<string>()))
                {
                    participation.adjunto4_par = null;
                }
                else
                {
                    participation.adjunto4_par = moveTempFile(id, participation.url_par, 4, param["adjunto4_par"].Value<string>());
                    if (string.IsNullOrEmpty(participation.adjunto4_par))
                    {
                        return Json(new { success = false, message = "adjunto4 verify failed." });
                    }

                    files.Add(new Attachment { label = GetOriginFileName(param["adjunto4_par"].Value<string>()), filename = participation.adjunto4_par });
                }

                if (string.IsNullOrEmpty(param["adjunto5_par"].Value<string>()))
                {
                    participation.adjunto5_par = null;
                }
                else
                {
                    participation.adjunto5_par = moveTempFile(id, participation.url_par, 5, param["adjunto5_par"].Value<string>());
                    if (string.IsNullOrEmpty(participation.adjunto5_par))
                    {
                        return Json(new { success = false, message = "adjunto5 verify failed." });
                    }

                    files.Add(new Attachment { label = GetOriginFileName(param["adjunto5_par"].Value<string>()), filename = participation.adjunto5_par });
                }

                _context.SaveChanges();

                WriteLog(string.Format("{0} Attachment Verify Success!", id));

                return Json(new { success = true, message = "ok", id_par = id, participation.url_par, files });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private string GetOriginFileName(string adjunto_par)
        {
            string[] parts = adjunto_par.Split(new Char[] { ':' });
            if (parts.Length < 2)
            {
                return adjunto_par;
            }

            return parts[1];
        }

        private string moveTempFile(int id_par, string url_par, int index, string adjunto_par)
        {
            string[] parts = adjunto_par.Split(new Char[] { ':' });
            if (parts.Length < 2)
            {
                return url_par + "_" + index + "_" + adjunto_par;
            }

            string tempFileName = Path.Combine(Constants.Config.temp_folder,  parts[0]);
            string fileName = parts[1];

            string uploadFolder = Path.Combine(uploadRootFolder, id_par.ToString());
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            fileName = url_par + "_" + index + "_" + fileName;
            string fullPath = Path.Combine(uploadFolder, fileName);

            try
            {
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                System.IO.File.Move(tempFileName, fullPath);
            }
            catch (Exception e)
            {
                WriteLog(e.Message);
                return null;
            }

            return fileName;
        }

        private void WriteLog(string message)
        {
            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
            {
                sw.WriteLine(string.Format("Datetime: {0} IP: {1}; {2}", 
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), ip, message));
            }
        }

        private class Attachment
        {
            public string label;
            public string filename;
        }
    }
    
    [Produces("application/json")]
    [Route("api/SendEmailValidar")]
    [Authorize]
    public class SendEmailValidarController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public SendEmailValidarController(IHostingEnvironment hostingEnvironment,
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

        // POST: /api/SendEmailValidar
        [HttpPost("{id}")]
        public async Task<IActionResult> SendEmailValidar([FromRoute] int id, [FromBody] ParticipationValidarEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.id_par)
            {
                return BadRequest();
            }
            try
            {
                var msg = "";

                var currentUser = await _userManager.GetUserAsync(User);
                var id_usu = currentUser.Id;

                var participation = _context.Participation.Where(m => m.id_par == id).FirstOrDefault();
                var pais_par = participation.pais_par;
                var producto = model.producto;
                var premioSelFrnt_par = participation.premioSelFrnt_par;

                //20191128 BEGIN
                var talla = participation.talla_par;
                //20191128 END

                var regalo = _context.Regalo.Where(r => r.id_regalo.Equals(premioSelFrnt_par) && r.pais.Equals(pais_par)).FirstOrDefault();
                var tipo = _context.Regalo.Where(r => r.producto.Equals(producto) && r.pais.Equals(pais_par)).FirstOrDefault().tipo;
                var grupoproducto = _context.Regalo.Where(r => r.producto.Equals(producto) && r.pais.Equals(pais_par)).FirstOrDefault().grupoproducto;
                
                if (regalo != null)
                {
                    //tipo = regalo.tipo;
                    //grupoproducto = regalo.grupoproducto;
                }
                else
                {
                    msg = "Fail";
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "id: " + participation.id_par + ", " +
                            "producto: " + producto + ", " +
                            "id_tall: " + participation.id_tall + ", " +
                            "email: " + participation.email_par + ", " +
                            "dni: " + participation.dni_par + ", " +
                            "action: " + "validate form -2" + ", " +
                            "result: no regalo. Fail."
                            );
                    }
                    return Json(new { success = false, message = msg });
                }


                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id: " + participation.id_par + ", " +
                        "producto: " + producto + ", " +
                        "id_tall: " + participation.id_tall + ", " +
                        "email: " + participation.email_par + ", " +
                        "dni: " + participation.dni_par + ", " +
                        "action: " + "validate form -2" + ", " +
                        "result: " + grupoproducto + " beginning re-validate"
                        );
                }

                if (tipo == "VIRTUAL")
                {

                }
                else if (tipo == "FISICO")
                {
                    //DOUBT102019 var a = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto) && x.pais_par.Equals(pais_par)).Count();
                    /*20191128 
                    var a = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto)).Count();
                    var regaloslimite = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto)).SingleOrDefault();
                    if(regaloslimite != null)
                    {
                        var b = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto)).SingleOrDefault().unidades_lim;
                        if (a < b)
                        {

                        }
                        else
                        {
                            var descripcion = _context.Regalo.Where(r => r.producto.Equals(producto) && r.pais.Equals(pais_par)).FirstOrDefault().descripcion;
                            msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                            // log for validate
                            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                            {
                                sw.WriteLine(
                                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                    "id_par: " + id + ", " +
                                    "action: " + "Validar" + ", " +
                                    "regalo: " + descripcion + ", " +
                                    "result: in case units are over"
                                    );
                            }
                            return Json(new { success = false, message = msg });
                        }
                    }*/

                    // check limit by talla (L or XL)
                    if (grupoproducto != null)
                    {
                        var a = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto)).Count();
                        // var regaloslimite = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto)).SingleOrDefault();
                        var regaloslimite = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto)).FirstOrDefault();
                        if (regaloslimite != null)
                        {
                            bool hay_registro = false;
                            var b = regaloslimite.unidades_lim;
                            if (regaloslimite.especial == "0") //Es GrupoProductoFisico sin tallas
                            {
                                if (a >= b)
                                {
                                    msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                                    // log for form
                                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                                    {
                                        sw.WriteLine(
                                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                            "id: " + participation.id_par + ", " +
                                            "producto: " + producto + ", " +
                                            "id_tall: " + participation.id_tall + ", " +
                                            "email: " + participation.email_par + ", " +
                                            "dni: " + participation.dni_par + ", " +
                                            "action: " + "validate form -2" + ", " +
                                            "result: " + grupoproducto + " in case units are over"
                                            );
                                    }
                                    return Json(new { success = false, message = msg, limit = true });
                                }
                                else
                                {
                                    hay_registro = true;
                                }
                            }
                            else
                            {
                                //Control talla
                                var validated_talla_count = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto) && x.talla_par.Equals(talla)).Count();
                                var regaloslimite_talla = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto) && x.producto.Equals(producto) && x.dimension.Equals(talla)).SingleOrDefault();
                                var regaloslimite_talla_count = regaloslimite_talla.unidades_lim;
                                if (validated_talla_count >= regaloslimite_talla_count)
                                {
                                    msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                                    // log for form
                                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                                    {
                                        sw.WriteLine(
                                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                            "id: " + participation.id_par + ", " +
                                            "producto: " + producto + ", " +
                                            "id_tall: " + participation.id_tall + ", " +
                                            "email: " + participation.email_par + ", " +
                                            "dni: " + participation.dni_par + ", " +
                                            "action: " + "validate form -2" + ", " +
                                            "result: " + grupoproducto + " in case units are over"
                                            );
                                    }
                                    return Json(new { success = false, message = msg, limit = true });
                                }
                                else
                                {
                                    hay_registro = true;
                                }

                            }

                            if (hay_registro == false)
                            {
                                msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                                // log for form
                                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                                {
                                    sw.WriteLine(
                                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                        "id: " + participation.id_par + ", " +
                                        "producto: " + producto + ", " +
                                        "id_tall: " + participation.id_tall + ", " +
                                        "email: " + participation.email_par + ", " +
                                        "dni: " + participation.dni_par + ", " +
                                        "action: " + "validate form -2" + ", " +
                                        "result: " + grupoproducto + " in case units are over"
                                        );
                                }
                                return Json(new { success = false, message = msg, limit = true });
                            }
                        }

                    }

                }

                participation.premioSelBck_par = premioSelFrnt_par;

                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                participation.fechaValidacion_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                participation.fechaEnvioEmail_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                participation.usuValidacion_par = id_usu;
                participation.fechaM_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                participation.usuM_par = id_usu;
                participation.usuEnvioEmail_par = id_usu;
                participation.id_est = 2;
                participation.valorpremio_par = producto;
                CultureInfo provider = CultureInfo.InvariantCulture;
                participation.registrocaducidadfecha_par = DateTime.ParseExact(lines[0].Replace("FechaCaducidad: ", ""), "dd/MM/yyyy", provider);
                //To do
                var test = _context.Regalo.Where(r => r.pais.Equals(pais_par) && r.producto.Equals(producto)).FirstOrDefault().id_regalo;

                _context.SaveChanges();

                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id: " + participation.id_par + ", " +
                        "producto: " + producto + ", " +
                        "id_tall: " + participation.id_tall + ", " +
                        "email: " + participation.email_par + ", " +
                        "dni: " + participation.dni_par + ", " +
                        "action: " + "validate form -2" + ", " +
                        "result: " + grupoproducto + " ending re-validate"
                        );
                }


                // send email and sms
                var url = "";
                var DOMAIN01 = "DOMAIN01";
                var DOMAIN02 = "DOMAIN02";
                var url_par = participation.url_par;
                var telefono_par = participation.telefono_par;
                var sms = "";
                Random random = new Random();
                DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
                DOMAIN02 = lines[5].Replace("DOMAIN02: ", "");
                if (pais_par == "ES")
                {
                    if (tipo == "VIRTUAL")
                    {

                        //url = DOMAIN01 + "/mi-participacion?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                        url = DOMAIN01 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                        //curl
                        var httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromSeconds(5);
                        var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tinyurl.com/api-create.php?url=" + url);
                        var response = await httpClient.SendAsync(request);
                        HttpContent responseContent = response.Content;
                        var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
                        var http = await reader.ReadToEndAsync();

                        var FechaCaducidad = "05/05/2020";
                        FechaCaducidad = lines[0].Replace("FechaCaducidad: ", "");
                        sms = "LA COMPRA DE NEUMATICOS MICHELIN TIENE PREMIO. DESCARGA TU REGALO ANTES DEL " + FechaCaducidad + " " + http + ". NO+PUBLI 900264257";

                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par);

                    }
                    else
                    {
                        sms = "LA COMPRA DE NEUMATICOS MICHELIN TIENE PREMIO. TU REGALO ESTA CAMINO. LO RECIBIRAS PROXIMAMENTE NO+PUBLI 900264257";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par);
                    }
                }
                else
                {
                    if (tipo == "VIRTUAL")
                    {

                        //url = DOMAIN02 + "/mi-participacao?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                        url = DOMAIN02 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                        //curl
                        var httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromSeconds(5);
                        var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tinyurl.com/api-create.php?url=" + url);
                        var response = await httpClient.SendAsync(request);
                        HttpContent responseContent = response.Content;
                        var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
                        var http = await reader.ReadToEndAsync();

                        var FechaCaducidad = "05/05/2020";
                        FechaCaducidad = lines[0].Replace("FechaCaducidad: ", "");
                        sms = "A SUA COMPRA DE PNEUS MICHELIN TEM UM PREMIO. FAÇA O DOWNLOAD DA SUA OFERTA ANTES DE " + FechaCaducidad + ". " + http + " NÃO+PUBLI 914242444";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+351" + telefono_par);
                    }
                    else
                    {
                        sms = "A SUA COMPRA DE PNEUS MICHELIN TEM UM PREMIO.O SEU PREMIO ESTA A CAMINHO E RECEBERA O MESMO BREVEMENTE.NÃO + PUBLI 914242444";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+351" + telefono_par);
                    }
                }

                var pieza = "";
                var asunto = "";
                if (pais_par == "ES")
                {
                    //url = DOMAIN01 + "/mi-participacion?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    url = DOMAIN01 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    pieza = "97987";
                    if (tipo == "VIRTUAL") pieza = "97988";
                    asunto = "La compra de neumáticos MICHELIN tiene premio";
                }
                else
                {
                    //url = DOMAIN02 + "/mi-participacao?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    url = DOMAIN02 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    pieza = "97990";
                    if (tipo == "VIRTUAL") pieza = "97989";
                    asunto = "A compra de pneus MICHELIN tem um prêmio";
                }

                var cemail = participation.email_par;
                var nombre = participation.nombre_par;
                var dato_20 = "Campaña Michelin";
                byte[] nombre_bytes = Encoding.UTF8.GetBytes(nombre);
                nombre = Encoding.UTF8.GetString(nombre_bytes);

                var nombreregalo = ""; // error
                var valorpremio_par = participation.valorpremio_par;
                if (valorpremio_par == null)
                {
                    nombreregalo = regalo.descripcion;
                }
                else
                {
                    nombreregalo = valorpremio_par;
                    nombreregalo = _context.Regalo.Where(r => r.producto.Equals(valorpremio_par) && r.pais.Equals(pais_par)).FirstOrDefault().descripcion;
                }
                byte[] nombreregalo_bytes = Encoding.UTF8.GetBytes(nombreregalo);
                nombreregalo = Encoding.UTF8.GetString(nombreregalo_bytes);

                var id_tall = participation.id_tall;

                var taller = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).SingleOrDefault().razonsocial_tall;
                byte[] taller_bytes = Encoding.UTF8.GetBytes(taller);
                taller = Encoding.UTF8.GetString(taller_bytes);
                var code = "";

                if (await teenvio_actualizar_regalovalidado(cemail, pieza, asunto, nombre, dato_20, cemail, nombreregalo, url, taller, pais_par))
                {
                    code = "ok";
                    msg = "Participación validada con éxito.";
                }
                else
                {
                    code = "ko";
                    msg = "Participación validada. Ha ocurrido un error en el envío del email";
                }

                // log for Validar
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "regalo: " + nombreregalo + ", " +
                        "action: " + "Validar" + ", " +
                        "result: " + msg
                        );
                }

                return Json(new { success = true, message = msg });
            }
            catch (Exception ex)
            {
                // log for Validar
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Validar" + ", " +
                        "result: " + "Fail."
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }

        }

        public async Task<bool> teenvio_actualizar_regalovalidado(String cemail, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String dato_20, String email, String nombreregalo, String urlregalo, String nombre_taller, String pais)
        {
            var action = "contact_save";
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }
            multipartContent.Add(new StringContent(cemail), "email");
            multipartContent.Add(new StringContent(dato_20), "dato_20");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var contact_id = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                contact_id = contact_id.Replace("OK: ", "");
                if (await teenvio_enviar_regalovalidado(contact_id, pieza_teenvio, asunto_teenvio, nombre_teenvio, email, nombreregalo, urlregalo, nombre_taller, pais))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> teenvio_enviar_regalovalidado(String contact_id, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String email, String nombreregalo, String urlregalo, String nombre_taller, String pais)
        {
            var action = "send_campaign";

            string vars = "{\"var_nombreregalo\":\"" + nombreregalo + "\", \"var_url\":\"" + urlregalo + "\", \"var_nombrepersona\":\"" + nombre_teenvio + "\",\"nombre_taller\":\"" + nombre_taller + "\"}";

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }
            multipartContent.Add(new StringContent(contact_id), "contact_id");
            multipartContent.Add(new StringContent("Michelin"), "name");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            multipartContent.Add(new StringContent(asunto_teenvio), "subject");
            multipartContent.Add(new StringContent(vars, Encoding.UTF8, "application/json"), "vars");

            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var id_envio = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                id_envio = contact_id.Replace("OK: ", "");
                return true;
            }
            return false;
        }
    }

    [Produces("application/json")]
    [Route("api/Rechazar")]
    [Authorize]
    public class RechazarController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        private string uploadFoler;

        public RechazarController(IHostingEnvironment hostingEnvironment,
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

            uploadFoler = lines[6].Replace("UPLOAD_URL: ", "");
        }

        // POST: /api/Rechazar/5
        [HttpPost("{id}")]
        public async Task<IActionResult> Rechazar([FromRoute] int id, [FromBody] ParticipationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.id_par)
            {
                return BadRequest();
            }
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var participation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id);
                if (participation == null)
                {
                    return NotFound();
                }

                // Two participaciones validated cant have the same factura1 or factura2 with the same id_tall
                var id_tall = participation.id_tall;
                var factura1_par = model.factura1_par;
                var factura2_par = model.factura2_par;

                int validated_id_par = 0;

                if (factura1_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(factura1_par) || p.factura2_par.Equals(factura1_par)));
                    if (tmp.Count() > 0)
                    {
                        validated_id_par = tmp.FirstOrDefault().id_par;
                    }
                }

                if (factura2_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(factura2_par) || p.factura2_par.Equals(factura2_par)));
                    if (tmp.Count() > 0)
                    {
                        validated_id_par = tmp.FirstOrDefault().id_par;
                    }
                }

                participation.email_par = model.email_par;
                participation.dni_par = model.dni_par;
                participation.telefono_par = model.telefono_par;
                participation.nombre_par = model.nombre_par;
                participation.apellidos_par = model.apellidos_par;
                participation.factura1_par = model.factura1_par;
                participation.factura2_par = model.factura2_par;
                participation.fechaCompra1_par = model.fechaCompra1_par;
                participation.fechaCompra2_par = model.fechaCompra2_par;
                participation.llanta = model.llanta;
                participation.TamanoRueda = model.TamanoRueda;
                participation.medidallanta_par = model.medidallanta_par;
                participation.numllantas_par = model.numllantas_par;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                participation.fechaM_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                participation.usuM_par = currentUser.Id; // user who modifes
                participation.usuariolocalidad_par = model.usuariolocalidad_par;
                participation.usuarioprovincia_par = model.usuarioprovincia_par;

                _context.SaveChanges();

                return Json(new { success = true, message = "ok", validated_id_par = validated_id_par });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }

    [Produces("application/json")]
    [Route("api/SendEmailRechazar")]
    [Authorize]
    public class SendEmailRechazarController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public SendEmailRechazarController(IHostingEnvironment hostingEnvironment,
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

        // POST: /api/SendEmailRechazar
        [HttpPost("{id}")]
        public async Task<IActionResult> SendEmailRechazar([FromRoute] int id, [FromBody] ParticipationRechazarEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.id_par)
            {
                return BadRequest();
            }
            try
            {
                var participation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id);
                if (participation == null)
                {
                    return NotFound();
                }
                
                var general = _context.General.SingleOrDefault();
                var link_bbll = general.fileprivacidad_gen;
                var link_priv = general.filelegal_gen;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                var fechaValidacion_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                var fecha_actual = TimeZoneInfo.ConvertTime(DateTime.UtcNow.Date, targetTimeZone);
                var hora_actual = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone).TimeOfDay;
                var textootrosmotivo = model.textootrosmotivo.Replace("'", "\\'");

                var currentUser = await _userManager.GetUserAsync(User);
                var id_usu = currentUser.Id;

                var updateParticipation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id);
                updateParticipation.id_est = 3;
                updateParticipation.fechaValidacion_par = fechaValidacion_par;
                updateParticipation.usuValidacion_par = id_usu;

                var iddup = 0;
                if (participation.factura1_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(participation.id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(participation.factura1_par) || p.factura2_par.Equals(participation.factura1_par)));
                    if (tmp.Count() > 0)
                    {
                        iddup = tmp.FirstOrDefault().id_par;
                    }
                }

                if (participation.factura2_par != "")
                {
                    var tmp = _context.Participation.Where(p => (p.id_tall.Equals(participation.id_tall)
                                                && p.id_est.Equals(2)) &&
                                                p.id_par != model.id_par &&
                                                (p.factura1_par.Equals(participation.factura2_par) || p.factura2_par.Equals(participation.factura2_par)));
                    if (tmp.Count() > 0)
                    {
                        iddup = tmp.FirstOrDefault().id_par;
                    }
                }

                //var to send sms correct subject
                var motivoparselec = model.motivo_par;

                // if (model.validated_id_par != 0 && model.validated_id_par != null)
                if (iddup != 0)
                {
                    updateParticipation.motivo_par = model.motivo_par + ".\n Número factura repetido en participación #" + iddup;
                }
                else
                {
                    updateParticipation.motivo_par = model.motivo_par;
                }
               
                if (model.motivo_par == "Otros")
                {
                    updateParticipation.comentarios_par = textootrosmotivo;
                }
                else
                {
                    updateParticipation.comentarios_par = "";
                }

                updateParticipation.fechaM_par = fechaValidacion_par;
                updateParticipation.usuM_par = id_usu;
                updateParticipation.rechazoFecha_par = fecha_actual;
                updateParticipation.rechazoHora_par = hora_actual;

                _context.Update(updateParticipation);

                await _context.SaveChangesAsync();

                var _participation = _context.Participation.SingleOrDefault(x => x.id_est.Equals(3) && x.id_par.Equals(id));
                if (_participation == null)
                {
                    return NotFound();
                }
                var email_par = _participation.email_par;
                var motivo_par = _participation.motivo_par;
                var comentarios_par = _participation.comentarios_par;
                var nombre_par = _participation.nombre_par;
                var apellidos_par = _participation.apellidos_par;
                var codigo_par = _participation.codigo_par;
                var pais_par = _participation.pais_par;
                var telefono_par = _participation.telefono_par;
                var motivocomentario = "";
                if (motivo_par == "Otros")
                {
                    motivocomentario = comentarios_par;
                }
                else
                {
                    motivocomentario = motivoparselec; // motivo_par;
                }
                var id_gan = _participation.id_gan;

                var _updateParticipation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id);
                _updateParticipation.fechaEnvioEmail_par = fechaValidacion_par;
                _updateParticipation.usuEnvioEmail_par = id_usu;
                _context.Update(_updateParticipation);

                await _context.SaveChangesAsync();

                var smsRechazado = "";
                if (pais_par == "ES")
                {
                    if (motivocomentario == "Nombre distinto en la factura y el formulario")
                    {
                        smsRechazado = "nombre distinto en factura y formulario";
                    }
                    else if (motivocomentario == "DNI/CIF  distinto en la factura y el formulario")
                    {
                        smsRechazado = "DNI/CIF distinto en factura y formulario";
                    }
                    else if (motivocomentario == "El taller seleccionado no coincide con la factura")
                    {
                        smsRechazado = "el taller seleccionado no coincide con la factura";
                    }
                    else if (motivocomentario == "El documento adjunto no es una factura")
                    {
                        smsRechazado = "no adjunta factura";
                    }
                    else if (motivocomentario == "El ticket de compra no es válido")
                    {
                        smsRechazado = "el ticket de compra no es válido";
                    }
                    else if (motivocomentario == "Factura adjunta no legible")
                    {
                        smsRechazado = "factura no legible";
                    }
                    else if (motivocomentario == "Factura incompleta")
                    {
                        smsRechazado = "factura incompleta";
                    }
                    else if (motivocomentario == "Factura no indica los productos en promoción")
                    {
                        smsRechazado = "factura sin producto en promocion";
                    }
                    else if (motivocomentario == "Factura no corresponde a un taller adherido a la promoción")
                    {
                        smsRechazado = "factura de taller no adherido";
                    }
                    else if (motivocomentario == "Factura ya ha sido premiada")
                    {
                        smsRechazado = "factura ya premiada";
                    }
                    else if (motivocomentario == "Faltan datos del usuario en la factura")
                    {
                        smsRechazado = "factura sin datos de ususario";
                    }
                    else if (motivocomentario == "Fecha de factura fuera de periodo promocional")
                    {
                        smsRechazado = "fecha de factura fuera de plazo";
                    }
                    else if (motivocomentario == "Otros")
                    {
                        smsRechazado = textootrosmotivo;
                    }
                    else if (motivocomentario == "Neumático de la factura no incluido en la promoción")
                    {
                        smsRechazado = "modelo no incluido en la promoción";
                    }
                    else if (motivocomentario == "Supera el límite de participación por email y/o dni")
                    {
                        smsRechazado = "supera limite email/DNI";
                    }

                    var sms = "UPS! TU PARTICIPACION EN LA PROMOCION MICHELIN & TU HA SIDO RECHAZADA. MOTIVO: " + smsRechazado.ToUpper() + ". No + Publi 900264257";
                    // call sms
                    InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                    await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par);
                }
                else
                {
                    if (motivocomentario == "Os dados da fatura e da participação não coincidem")
                    {
                        smsRechazado = "nome da fatura e da participação não coincidem";
                    }
                    else if (motivocomentario == "NIF da fatura e da participação não coincidem")
                    {
                        smsRechazado = "NIF da fatura e da participação não coincidem";
                    }
                    else if (motivocomentario == "Data da fatura fora do prazo da campanha")
                    {
                        smsRechazado = "Data da fatura fora do prazo da campanha";
                    }
                    else if (motivocomentario == "Fatura sem dados do participante")
                    {
                        smsRechazado = "Fatura sem dados do participante";
                    }
                    else if (motivocomentario == "Fatura ilegível")
                    {
                        smsRechazado = "Fatura ilegível";
                    }
                    else if (motivocomentario == "Fatura incompleta")
                    {
                        smsRechazado = "Fatura incompleta";
                    }
                    else if (motivocomentario == "Participação sem fatura")
                    {
                        smsRechazado = "Participação sem fatura";
                    }
                    else if (motivocomentario == "Fatura sem produtos da promoção")
                    {
                       // smsRechazado = "Fatura sem produtos da promoção";
                        smsRechazado = "producto não esta incluido na promoção";
                    }
                    else if (motivocomentario == "Fatura de uma oficina não aderente a campanha")
                    {
                        smsRechazado = "Fatura de uma oficina não aderente";
                    }
                    else if (motivocomentario == "Fatura já premiada")
                    {
                        smsRechazado = "Fatura já premiada";
                    }
                    else if (motivocomentario == "Excede o limite email/dni")
                    {
                        smsRechazado = "Excede o limite email/dni";
                    }
                    else if (motivocomentario == "Fatura Inválida")
                    {
                        smsRechazado = "fatura invalida";
                    }
                    else if (motivocomentario == "La oficina selecionada não corresponde à fatura")
                    {
                        smsRechazado = "a oficina selecionada não corresponde à fatura";
                    }
                    else if (motivocomentario == "Otros")
                    {
                        smsRechazado = textootrosmotivo;
                    }
                    else if (motivocomentario == "O modelo de pneu não esta incluido na promoção")
                    {
                        smsRechazado = "modelo não esta incluido na promoção";
                    }
                    else if (motivocomentario == "Excede o limite email/dni")
                    {
                        smsRechazado = "excede o limite email/dni";
                    }

                    var sms = "UPS! A SUA PARTICIPACAO NA CAMPANHA OUTONO NAO FOI VALIDADA. MOTIVO: " + smsRechazado.ToUpper() + ". NAO+Pub: 914242444";
                    // call sms
                    InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                    await _infoBitSmsManager.SendSMS(sms, "+351" + telefono_par);
                }

                var cemail = email_par;
                var nombre = nombre_par;
                var dato_20 = "Campaña Michelin";
                byte[] bytes = Encoding.UTF8.GetBytes(motivocomentario);
                motivocomentario = Encoding.UTF8.GetString(bytes);

                var code = "";
                var msg = "";
                var url = "";
                var pieza = "";
                var asunto = "";
                var DOMAIN01 = "DOMAIN01";
                var DOMAIN02 = "DOMAIN02";
                Random random = new Random();
                DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
                DOMAIN02 = lines[5].Replace("DOMAIN02: ", "");
                var url_par = _participation.url_par; // t

                if (pais_par == "ES")
                {
                    url = DOMAIN01 + "/mi-participacion?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    pieza = "97993";
                    asunto = "Tu participación en MICHELIN & Tú ha sido rechazada";
                }
                else
                {
                    url = DOMAIN02 + "/mi-participacao?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    pieza = "97999";
                    asunto = "Sua participação na MICHELIN & Você foi rejeitada";
                }
                if (await teenvio_actualizar_contacto_rechazo(url, cemail, pieza, asunto, nombre, dato_20, motivocomentario, "", link_bbll, link_priv, pais_par))
                {
                    code = "ok";
                    msg = "Participación Rechazada con éxito";
                }
                else
                {
                    code = "ko";
                    msg = "Ha ocurrido un error en el envío del motivo de rechazo";
                }

                // log for Rechazada
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Rechazada" + ", " +
                        "result: " + msg
                        );
                }

                return Json(new { success = true, message = msg });
            }
            catch (Exception ex)
            {
                // log for Rechazada
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Rechazada" + ", " +
                        "result: " + "Fail"
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }

        }

        public async Task<bool> teenvio_actualizar_contacto_rechazo(String url, String cemail, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String dato_20, String motivo, String dato_18, String link_bbll, String link_priv, String pais)
        {
            var action = "contact_save";
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }
            multipartContent.Add(new StringContent(cemail), "email");
            multipartContent.Add(new StringContent(dato_20), "dato_20");
            multipartContent.Add(new StringContent(dato_18), "dato_19");
            multipartContent.Add(new StringContent(dato_18), "dato_18");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var contact_id = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                contact_id = contact_id.Replace("OK: ", "");
                if (await teenvio_enviar_pieza_rechazo(url, contact_id, pieza_teenvio, asunto_teenvio, nombre_teenvio, motivo, link_bbll, link_priv, pais))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }

        public async Task<bool> teenvio_enviar_pieza_rechazo(String url, String contact_id, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String motivo, String link_bbll, String link_priv, String pais)
        {
            var action = "send_campaign";
            link_bbll = "http://promocoes.michelin.pt/pdf/" + link_bbll;
            link_priv = "http://promocoes.michelin.pt/pdf/" + link_priv;
            var link_contacto = "http://promocoes.michelin.pt/contacto";
            // to do
            string vars = "{\"var_url\":\"" + url + "\", \"var_rechazo\":\"" + motivo + "\", \"var_nombrepersona\":\"" + nombre_teenvio + "\", \"link_bbll\":\"" + link_bbll + "\",\"link_priv\":\"" + link_priv + "\",\"link_contacto\":\"" + link_contacto + "\"}";

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }
            multipartContent.Add(new StringContent(contact_id), "contact_id");
            multipartContent.Add(new StringContent(nombre_teenvio), "name");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            multipartContent.Add(new StringContent(asunto_teenvio), "subject");
            multipartContent.Add(new StringContent(vars, Encoding.UTF8, "application/json"), "vars");

            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var id_envio = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                id_envio = contact_id.Replace("OK: ", "");
                return true;
            }
            return false;
        }
    }

    [Produces("application/json")]
    [Route("api/Reactivar")]
    [Authorize]
    public class ReactivarController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public ReactivarController(IHostingEnvironment hostingEnvironment,
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

        // POST: /api/Reactivar/5
        [HttpPost("{id}")]
        public async Task<IActionResult> Reactivar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var participation = await _context.Participation.SingleOrDefaultAsync(m => m.id_par == id);
                if (participation == null)
                {
                    return NotFound();
                }
                participation.id_est = 1;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                participation.fechaM_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                var currentUser = await _userManager.GetUserAsync(User);
                var id_usu = currentUser.Id;
                participation.usuM_par = id_usu;
                _context.Update(participation);

                _context.SaveChanges();

                // log for Reactivar
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Reactivar" + ", " +
                        "result: " + "Participación reactiva con éxito."
                        );
                }

                return Json(new { success = true, message = "Participación reactiva con éxito." });
            }
            catch (Exception ex)
            {
                // log for Reactivar
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Reactivar" + ", " +
                        "result: " + "Fail."
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    [Produces("application/json")]
    [Route("api/SendEmailReenviar")]
    [Authorize]
    public class SendEmailReenviarController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public SendEmailReenviarController(IHostingEnvironment hostingEnvironment,
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

        // POST: /api/SendEmailReenviar
        [HttpPost("{id}")]
        public async Task<IActionResult> SendEmailReenviar([FromRoute] int id, [FromBody] ParticipationReenviarEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.id_par)
            {
                return BadRequest();
            }
            try
            {
                var msg = "";
                var participation = _context.Participation.Where(m => m.id_par == id).FirstOrDefault();
                var timezone = lines[3].Replace("Timezone: ", "");
                var currentUser = await _userManager.GetUserAsync(User);
                var id_usu = currentUser.Id;
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                var fechaValidacion_par = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
                participation.fechaEnvioEmail_par = fechaValidacion_par;
                participation.usuEnvioEmail_par = id_usu;
                _context.SaveChanges();

                var pais_par = participation.pais_par;
                var producto = participation.valorpremio_par;
                var tipo = _context.Regalo.Where(r => r.producto.Equals(producto) && r.pais.Equals(pais_par)).FirstOrDefault().tipo;

                var url = "";
                var DOMAIN01 = "DOMAIN01";
                var DOMAIN02 = "DOMAIN02";
                var url_par = participation.url_par;
                var telefono_par = participation.telefono_par;
                var sms = "";
                Random random = new Random();

                if (pais_par == "ES")
                {
                    if (tipo == "VIRTUAL")
                    {
                        DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
                        //url = DOMAIN01 + "/mi-participacion?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                        url = DOMAIN01 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                        //curl
                        var httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromSeconds(5);
                        var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tinyurl.com/api-create.php?url=" + url);
                        var response = await httpClient.SendAsync(request);
                        HttpContent responseContent = response.Content;
                        var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
                        var http = await reader.ReadToEndAsync();

                        var FechaCaducidad = "05/05/2020";
                        FechaCaducidad = lines[0].Replace("FechaCaducidad: ", "");
                        sms = "LA COMPRA DE NUMATICOS MICHELIN TIENE PREMIO. DESCARGA TU REGALO ANTES DEL  " + FechaCaducidad + ". " + http + " NO+PUBLI 900264257";

                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par);

                    }
                    else
                    {
                        sms = "TU PARTICIPACION EN LA PROMOCION OTOÑO MICHELIN HA SIDO REGISTRADA. EN 5 DIAS LABORABLES PROCEDEREMOS A LA VALIDACION. NO+PUBLI 900264257 ";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par);
                    }
                }
                else
                {
                    if (tipo == "VIRTUAL")
                    {
                        DOMAIN02 = lines[5].Replace("DOMAIN02: ", "");
                        //url = DOMAIN02 + "/mi-participacao?u=" + url_par + "&z=" + id + "&xa=" + random.Next();
                        url = DOMAIN02 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + random.Next();
                        //curl
                        var httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromSeconds(5);
                        var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tinyurl.com/api-create.php?url=" + url);
                        var response = await httpClient.SendAsync(request);
                        HttpContent responseContent = response.Content;
                        var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
                        var http = await reader.ReadToEndAsync();

                        var FechaCaducidad = "05/05/2020";
                        FechaCaducidad = lines[0].Replace("FechaCaducidad: ", "");
                        sms = "A SUA COMPRA DE PNEUS MICHELIN TEM UM PREMIO. FAÇA O DOWNLOAD DA SUA OFERTA ANTES DE " + FechaCaducidad + ". " + http + " NÃO+PUBLI 914242444";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+351" + telefono_par);
                    }
                    else
                    {
                        sms = "A SUA COMPRA DE PNEUS MICHELIN TEM UM PREMIO.O SEU PREMIO ESTA A CAMINHO E RECEBERA O MESMO BREVEMENTE. NÃO+PUBLI 914242444";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+351" + telefono_par);
                    }
                }

                var pieza = "";
                var asunto = "";

                if (pais_par == "ES")
                {
                    //url = DOMAIN01 + "/mi-participacion?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    url = DOMAIN01 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    pieza = "97987";
                    if (tipo == "VIRTUAL") pieza = "97988";
                    asunto = "La compra de neumáticos MICHELIN tiene premio";
                }
                else
                {
                    //url = DOMAIN02 + "/mi-participacao?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();
                    url = DOMAIN02 + "/get_gift_201910?u=" + url_par + "&z=" + id + "&xa=" + +random.Next();

                    pieza = "97990";
                    if (tipo == "VIRTUAL") pieza = "97989";
                    asunto = "A compra de pneus MICHELIN tem um prêmio";
                }

                var cemail = participation.email_par;
                var nombre = participation.nombre_par;
                var dato_20 = "Campaña Michelin";
                byte[] nombre_bytes = Encoding.UTF8.GetBytes(nombre);
                nombre = Encoding.UTF8.GetString(nombre_bytes);

                var nombreregalo = ""; // to do

                var valorpremio_par = participation.valorpremio_par;
                if (valorpremio_par == null)
                {
                    nombreregalo = _context.Regalo.Where(r => r.producto.Equals(producto) && r.pais.Equals(pais_par)).FirstOrDefault().descripcion;
                }
                else
                {
                    nombreregalo = valorpremio_par;
                    nombreregalo = _context.Regalo.Where(r => r.producto.Equals(valorpremio_par) && r.pais.Equals(pais_par)).FirstOrDefault().descripcion;
                }

                byte[] nombreregalo_bytes = Encoding.UTF8.GetBytes(nombreregalo);
                nombreregalo = Encoding.UTF8.GetString(nombreregalo_bytes);

                var id_tall = participation.id_tall;

                var taller = _context.Workshop.Where(x => x.id_tall.Equals(id_tall)).SingleOrDefault().razonsocial_tall;
                byte[] taller_bytes = Encoding.UTF8.GetBytes(taller);
                taller = Encoding.UTF8.GetString(taller_bytes);
                var code = "";

                if (await teenvio_actualizar_regalovalidado(cemail, pieza, asunto, nombre, dato_20, cemail, nombreregalo, url, taller, pais_par))
                {
                    code = "ok";
                    msg = "Email enviado con éxito.";
                }
                else
                {
                    code = "ko";
                    msg = "Fail";
                }

                // log for Reenviar
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Reenviar" + ", " +
                        "result: " + msg
                        );
                }

                return Json(new { success = true, message = msg });
            }
            catch (Exception ex)
            {
                // log for Reenviar
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_par: " + id + ", " +
                        "action: " + "Reenviar" + ", " +
                        "result: " + "Fail"
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }

        }

        public async Task<bool> teenvio_actualizar_regalovalidado(String cemail, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String dato_20, String email, String nombreregalo, String urlregalo, String nombre_taller, String pais)
        {
            var action = "contact_save";
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }
            multipartContent.Add(new StringContent(cemail), "email");
            multipartContent.Add(new StringContent(dato_20), "dato_20");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var contact_id = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                contact_id = contact_id.Replace("OK: ", "");
                if (await teenvio_enviar_regalovalidado(contact_id, pieza_teenvio, asunto_teenvio, nombre_teenvio, email, nombreregalo, urlregalo, nombre_taller, pais))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> teenvio_enviar_regalovalidado(String contact_id, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String email, String nombreregalo, String urlregalo, String nombre_taller, String pais)
        {
            var action = "send_campaign";

            string vars = "{\"var_nombreregalo\":\"" + nombreregalo + "\", \"var_url\":\"" + urlregalo + "\", \"var_nombrepersona\":\"" + nombre_teenvio + "\",\"nombre_taller\":\"" + nombre_taller + "\"}";

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            if (pais == "ES")
            {
                multipartContent.Add(new StringContent("517"), "rid");
            }
            else
            {
                multipartContent.Add(new StringContent("520"), "rid");
            }
            multipartContent.Add(new StringContent(contact_id), "contact_id");
            multipartContent.Add(new StringContent("Michelin"), "name");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            multipartContent.Add(new StringContent(asunto_teenvio), "subject");
            multipartContent.Add(new StringContent(vars, Encoding.UTF8, "application/json"), "vars");

            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var id_envio = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                id_envio = contact_id.Replace("OK: ", "");
                return true;
            }
            return false;
        }
    }
}