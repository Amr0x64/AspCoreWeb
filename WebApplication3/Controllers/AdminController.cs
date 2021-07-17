using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApplication3.ViewModels;

using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationContext db;
        public AdminController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewProduct()
        {
            return View(await db.Products.ToListAsync());
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product { Id = model.Id, Description = model.Description, Title = model.Title, Price = model.Price, Time = model.Time };
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewProduct");
            }
            return View(model);
        }
    }
}
