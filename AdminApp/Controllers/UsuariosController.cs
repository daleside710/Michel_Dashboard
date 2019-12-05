using AdminApp.Data;
using AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Controllers
{
    [Authorize]
    public class UsuariosController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsuariosController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var role = currentUser.tipo_usu;
            if (role == "0")
            {
                return Redirect("/");
            }
            if(role == "2")
            {
                return Redirect("/");
            }
            return View();
        }

        public IActionResult Add()
        {
            UserAddViewModel model = new UserAddViewModel();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            User user = _context.User.Where(x => x.Id.Equals(id)).FirstOrDefault();
            UserEditViewModel model = new UserEditViewModel();
            model.nombre_usu = user.nombre_usu;
            model.apellidos_usu = user.apellidos_usu;
            model.tipo_usu = user.tipo_usu;
            model.pais_usu = user.pais_usu;
            model.Email = user.Email;
            return View(model);
        }

        public IActionResult UpdatePassword(int id)
        {
            return View();
        }

    }
}
