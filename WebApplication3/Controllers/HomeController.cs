using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        private RPRCContext db;
        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, RPRCContext context)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            db = context;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                CategoreList = db.Products.Select(p => p.Category).Distinct().ToList()
            };
            return View(model);
        }
        public IActionResult About() => View();
        public IActionResult Contacts() => View();

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
