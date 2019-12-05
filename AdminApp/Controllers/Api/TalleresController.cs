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
using Microsoft.Extensions.Configuration;

namespace AdminApp.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Talleres")]
    [Authorize]
    public class TalleresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private string[] lines;

        public TalleresController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            lines = System.IO.File.ReadAllLines(@"config.txt");
        }

        // POST: api/Workshops
        [HttpPost]
        public async Task<IActionResult> GetWorkshops()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var role = currentUser.tipo_usu;
            var country = currentUser.pais_usu;
            

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
            var workshops = (from tempuser in _context.Workshop.Where(x => !x.pais_tall.Contains("BORRADO"))
                                         select tempuser);
            // get all Workshops
            if (role == "2")
            {
                if(country == "1"){ //PORTUGAL
                    workshops = (from tempuser in _context.Workshop.Where(x => x.pais_tall.Equals("PORTUGAL") && !x.pais_tall.Contains("BORRADO"))
                                     select tempuser);
                }
                else if(country == "0") // ESPAÑA
                {
                    workshops = (from tempuser in _context.Workshop.Where(x => x.pais_tall.Equals("ESPAÑA") && !x.pais_tall.Contains("BORRADO"))
                                 select tempuser);
                }

            }
            

            //Sorting 
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                switch (sortColumn)
                {
                    case "razonsocial_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.razonsocial_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.razonsocial_tall);
                        }
                        break;
                    case "alias_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.alias_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.alias_tall);
                        }
                        break;
                    case "lC_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.LC_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.LC_tall);
                        }
                        break;
                    case "hpdV_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.HPDV_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.HPDV_tall);
                        }
                        break;
                    case "ensenA_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.ENSENA_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.ENSENA_tall);
                        }
                        break;
                    case "direccion_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.direccion_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.direccion_tall);
                        }
                        break;
                    case "poblacion_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.poblacion_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.poblacion_tall);
                        }
                        break;
                    case "cp_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.cp_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.cp_tall);
                        }
                        break;
                    case "provincia_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.provincia_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.provincia_tall);
                        }
                        break;
                    case "pais_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.pais_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.pais_tall);
                        }
                        break;
                    case "fechaDesde_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.fechaDesde_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.fechaDesde_tall);
                        }
                        break;
                    case "REGION_tall":
                        if (sortColumnDirection == "asc")
                        {
                            workshops = workshops.OrderBy(workshop => workshop.REGION_tall);
                        }
                        else
                        {
                            workshops = workshops.OrderByDescending(workshop => workshop.REGION_tall);
                        }
                        break;
                }
            }

            // Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                workshops = workshops.Where(m => m.razonsocial_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.alias_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.LC_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.HPDV_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.ENSENA_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.direccion_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.poblacion_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.cp_tall.Contains(searchValue)
                                    || m.provincia_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.pais_tall.ToUpper().Contains(searchValue.ToUpper())
                                    || m.fechaDesde_tall.Value.ToString("dd/MM/yyyy").Contains(searchValue)
                                    || m.REGION_tall.ToUpper().Contains(searchValue.ToUpper())
                                    );
                    
            }

            //total number of rows counts
            recordsTotal = workshops.Count();
            //Paging   
            var data = workshops.Skip(skip).Take(pageSize).ToList();
            int recordsFiltered = recordsTotal;
            return Json(new { data, draw, recordsFiltered, recordsTotal });
        }

        // PUT: api/Workshops
        [HttpPut]
        public async Task<IActionResult> AddWorkshop([FromBody] WorkshopAddViewModel workshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var pais_tall = "ESPAÑA";
                if (workshop.provincia_tall == "0")
                {
                    pais_tall = "ESPAÑA";
                }
                else if(workshop.provincia_tall == "1")
                {
                    pais_tall = "PORTUGAL";
                }
                else if(workshop.provincia_tall == "2")
                {
                    pais_tall = "PENINSULA";
                }

                var register = new Workshop
                {
                    razonsocial_tall = workshop.razonsocial_tall,
                    alias_tall = workshop.alias_tall,
                    LC_tall = workshop.LC_tall,
                    HPDV_tall = workshop.HPDV_tall,
                    ENSENA_tall = workshop.ENSENA_tall,
                    direccion_tall = workshop.direccion_tall,
                    poblacion_tall = workshop.poblacion_tall,
                    cp_tall = workshop.cp_tall,
                    provincia_tall = workshop.provincia_tall,
                    pais_tall = pais_tall,
                    fechaDesde_tall = workshop.fechaDesde_tall,
                    usuC_tall = currentUser.Id,
                    usuM_tall = currentUser.Id,
                    REGION_tall = workshop.REGION_tall,
                };
                _context.Workshop.Add(register);

                _context.SaveChanges();

                return Json(new { success = true, message = "Taller registrado con éxito" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // PUT: api/Workshop/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditWorkshop([FromRoute] int id, [FromBody] WorkshopEditViewModel workshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workshop.id_tall)
            {
                return BadRequest();
            }
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var updateWorkshop = _context.Workshop.Where(m => m.id_tall == workshop.id_tall).FirstOrDefault();
                if (updateWorkshop == null)
                {
                    return NotFound();
                }
                var pais_tall = "ESPAÑA";

                if (workshop.pais_tall == "0")
                {
                    pais_tall = "ESPAÑA";
                }
                else if (workshop.pais_tall == "1")
                {
                    pais_tall = "PORTUGAL";
                }
                else if (workshop.pais_tall == "2")
                {
                    pais_tall = "PENINSULA";
                }

                updateWorkshop.razonsocial_tall = workshop.razonsocial_tall;
                updateWorkshop.alias_tall = workshop.alias_tall;
                updateWorkshop.LC_tall = workshop.LC_tall;
                updateWorkshop.HPDV_tall = workshop.HPDV_tall;
                updateWorkshop.ENSENA_tall = workshop.ENSENA_tall;
                updateWorkshop.direccion_tall = workshop.direccion_tall;
                updateWorkshop.poblacion_tall = workshop.poblacion_tall;
                updateWorkshop.cp_tall = workshop.cp_tall;
                updateWorkshop.provincia_tall = workshop.provincia_tall;
                updateWorkshop.pais_tall = pais_tall;
                updateWorkshop.fechaDesde_tall = workshop.fechaDesde_tall;
                updateWorkshop.REGION_tall = workshop.REGION_tall;
                updateWorkshop.usuM_tall = currentUser.Id;
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);

                updateWorkshop.fechaM_tall = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);

                _context.SaveChanges();

                return Json(new { success = true, message = "Taller actualizado con éxito" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

        }

        // DELETE: api/Workshop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshop([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                var deleteString = "Borrado " + TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone).ToString("yyyy-MM-dd");
                var workshop = _context.Workshop.Where(m => m.id_tall == id).FirstOrDefault();
                if (workshop == null)
                {
                    return NotFound();
                }
                var currentUser = await _userManager.GetUserAsync(User);
                workshop.pais_tall = deleteString;
                workshop.usuM_tall = currentUser.Id;
                _context.SaveChanges();

                return Json(new { success = true, message = "Taller eliminado con éxito" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}