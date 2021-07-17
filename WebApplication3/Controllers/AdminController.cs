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
                Product product = new Product { Id = model.Id, Title = model.Title, Description = model.Description, Price = model.Price, Time = DateTime.Now };
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewProduct");
            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var product = db.Products.Single(x => x.Id == id);
            if (product != null)
            {
               EditProductViewModel model = new EditProductViewModel { Id = product.Id, Title = product.Title, Description = product.Description, Price = product.Price };
               return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var product = db.Products.Single(x => x.Id == model.Id);
                if (product != null)
                {
                    product.Title = model.Title;
                    product.Description = model.Description;
                    product.Price = model.Price;

                    db.Products.Update(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ViewProduct");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = db.Products.Single(x => x.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewProduct");
            }
            return NotFound();
        }
    }
}
