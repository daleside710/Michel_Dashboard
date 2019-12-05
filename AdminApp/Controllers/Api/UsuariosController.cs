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
using static AdminApp.Controllers.ManageController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AdminApp.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Usuarios")]
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private string[] lines;
        public IHttpContextAccessor _accessor;
        private DateTime datetime;
        private string ip;

        public UsuariosController(ApplicationDbContext context,
            UserManager<User> userManager,
            IHttpContextAccessor accessor)
        {
            _context = context;
            _userManager = userManager;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> GetUsers()
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
            int usuC_usu = currentUser.Id;

            // get all Users
            var users = (from tempuser in _context.User
                         select tempuser);

            //Sorting 
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                switch (sortColumn)
                {
                    case "nombre_usu":
                        if (sortColumnDirection == "asc")
                        {
                            users = users.OrderBy(user => user.nombre_usu);
                        }
                        else
                        {
                            users = users.OrderByDescending(user => user.nombre_usu);
                        }
                        break;
                    case "email":
                        if (sortColumnDirection == "asc")
                        {
                            users = users.OrderBy(user => user.Email);
                        }
                        else
                        {
                            users = users.OrderByDescending(user => user.Email);
                        }
                        break;
                    case "tipo_usu":
                        if (sortColumnDirection == "asc")
                        {
                            users = users.OrderBy(user => user.tipo_usu);
                        }
                        else
                        {
                            users = users.OrderByDescending(user => user.tipo_usu);
                        }
                        break;
                    case "pais_usu":
                        if (sortColumnDirection == "asc")
                        {
                            users = users.OrderBy(user => user.pais_usu);
                        }
                        else
                        {
                            users = users.OrderByDescending(user => user.pais_usu);
                        }
                        break;
                    case "activo_usu":
                        if (sortColumnDirection == "asc")
                        {
                            users = users.OrderBy(user => user.activo_usu);
                        }
                        else
                        {
                            users = users.OrderByDescending(user => user.activo_usu);
                        }
                        break;
                }
            }

            // Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                users = users.Where(m => m.nombre_usu.ToUpper().Contains(searchValue.ToUpper())
                || m.Email.ToUpper().Contains(searchValue.ToUpper())
                || (m.tipo_usu.Equals("1") ? "ADM" : "SAC").ToUpper().Contains(searchValue.ToUpper())
                || (m.pais_usu.Equals("1") ? "PORTUGAL" : "ESPAÑA").ToUpper().Contains(searchValue.ToUpper())
                || (m.activo_usu.Equals(1) ? "Activar" : "Desactivar").ToUpper().Contains(searchValue.ToUpper())
                );
            }

            //total number of rows counts
            recordsTotal = users.Count();
            //Paging   
            var data = users.Skip(skip).Take(pageSize).ToList();
            int recordsFiltered = recordsTotal;
            return Json(new { data, draw, recordsFiltered, recordsTotal });
        }

        // PUT: api/User
        [HttpPut]
        public async Task<IActionResult> AddUser([FromBody] UserAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
               
                var user = new User
                {
                    UserName = model.Email, // 
                    Email = model.Email, // 
                    nombre_usu = model.nombre_usu, // Name
                    apellidos_usu = model.apellidos_usu, // Surname
                    tipo_usu = model.tipo_usu, // Role
                    pais_usu = model.pais_usu, // Country 
                    activo_usu = 1, // Active 
                    usuC_usu = currentUser.Id, // user who creates 
                    usuM_usu = currentUser.Id, // user who modifes 
                    fechaC_usu = datetime, // created datetime 
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Log
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine("Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + " Email: " + currentUser.Email + " Nombre: " + currentUser.nombre_usu + " Action: Usuario agregado.");
                    }

                    return Json(new { success = true, message = "Usuario agregado con éxito." });
                }
                else
                {
                    return Json(new { success = false, message = result.Errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser([FromRoute] int id, [FromBody] UserEditViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var updateUser = await _userManager.FindByIdAsync(user.Id.ToString());
                updateUser.Email = user.Email;
                updateUser.NormalizedUserName = user.Email.ToUpper();
                updateUser.UserName = user.Email;
                updateUser.nombre_usu = user.nombre_usu;
                updateUser.apellidos_usu = user.apellidos_usu;
                updateUser.tipo_usu = user.tipo_usu;
                updateUser.pais_usu = user.pais_usu;
                updateUser.usuM_usu = currentUser.Id; // user who modifes

                updateUser.fechaM_usu = datetime;

                var result = await _userManager.UpdateAsync(updateUser);

                if (result.Succeeded)
                {
                    // log
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine("Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + " Email: " + currentUser.Email + " Nombre: " + currentUser.nombre_usu + " Usuario actualizado.");
                    }

                    return Json(new { success = true, message = "Usuario actualizado con éxito." });
                }
                else
                {
                    return Json(new { success = false, message = result });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                var currentUser = await _userManager.GetUserAsync(User);

                _context.User.Remove(user);
                var result = await _context.SaveChangesAsync();
                // log
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine("Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + " Email: " + currentUser.Email + " Nombre: " + currentUser.nombre_usu + " Usuario eliminado.");
                }

                return Json(new { success = true, message = "Usuario eliminado." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }

    [Produces("application/json")]
    [Route("api/ChangePassword")]
    [Authorize]
    public class ChangePasswordController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private string[] lines;
        public IHttpContextAccessor _accessor;
        private DateTime datetime;
        private string ip;

        public ChangePasswordController(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            _context = context;
            _userManager = userManager;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        // PATCH: /api/User/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> UserChangePassword([FromRoute] int id, [FromBody] PasswordEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.Id)
            {
                return BadRequest();
            }
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        // log
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine("Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + " Email: " + currentUser.Email + " Nombre: " + currentUser.nombre_usu + " Usuario actualizado.");
                        }

                        return Json(new { success = true, message = "Usuario actualizado con éxito." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Usuario actualizado falla." });
                    }
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = false, message = ManageMessageId.Error });
        }

    }

    [Produces("application/json")]
    [Route("api/Deactivate")]
    [Authorize]
    public class DeactivateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private string[] lines;
        public IHttpContextAccessor _accessor;
        private DateTime datetime;
        private string ip;

        public DeactivateController(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            _context = context;
            _userManager = userManager;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        // POST: api/Deactivate/5
        [HttpPost("{id}")]
        public async Task<IActionResult> UserDeactivate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                var updateUser = await _userManager.FindByIdAsync(id.ToString());
                if (updateUser.activo_usu == 0)
                {
                    updateUser.activo_usu = 1;
                }
                else
                {
                    updateUser.activo_usu = 0;
                }

                updateUser.fechaM_usu = datetime;

                var result = await _userManager.UpdateAsync(updateUser);
                if (result.Succeeded)
                {
                    // log
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine("Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + " Email: " + currentUser.Email + " Nombre: " + currentUser.nombre_usu + " Usuario actualizado.");
                    }

                    return Json(new { success = true, message = "Usuario actualizado con éxito." });
                }
                else
                {
                    return Json(new { success = false, message = "Usuario actualizado falla." });
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}