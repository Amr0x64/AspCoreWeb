using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "admin, superuser")]
    public class UserController : Controller
    {
        UserManager<User> _userManager;
        private ApplicationContext db;
        private RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, ApplicationContext context, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public IActionResult Index()
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            var employList = new List<User>();
            foreach (var user in _userManager.Users.ToList())
            {
                if (_userManager.GetRolesAsync(user).Result[0] != "user")
                {
                    employList.Add(user);
                }
            }
            return View(employList);
        }
        public IActionResult Create()
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Name, Year = model.Year };
                if ((model.RoleName != "user") && (model.RoleName != "admin"))
                {
                    return View(model); 
                }
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year, Name = user.UserName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Name;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Name = user.UserName, Id = user.Id };
            return View(model);
        }
        #region ||--||--||--||--||--||--||--||--||--||--||--||--СМЕНА ПАРОЛЯ--||--||--||--||--||--||--||--||--||--||--||--||--||--||--||
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    if (model.NewPassword == model.NewPasswordConfirm )
                    {
                        var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;

                        var _passwordHasher =
                            HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                        IdentityResult result =
                            await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);

                        if (result.Succeeded)
                        {
                            user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                            await _userManager.UpdateAsync(user);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Пароли не совпадают");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
        #endregion
    }
}
