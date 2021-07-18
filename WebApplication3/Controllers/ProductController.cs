using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApplication3.ViewModels; 

namespace WebApplication3.Controllers
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private ApplicationContext db;
        public ProductController(ApplicationContext context)
        {
            db = context;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["ProductCount"] = db.Products.Count();
            return View(await db.Products.ToListAsync());
        }
        public IActionResult Detail(int id)
        {
            Product product = db.Products.Single(x => x.Id == id);
            if (product != null)
            {
                return View(product);
            }
            return NotFound();
        }
        [Authorize]
        [HttpGet]
        public IActionResult BuyProduct(int id)
        {
            Product product = db.Products.Single(x => x.Id == id);
            
            if (product != null)
            {
                User user = db.Users.Single(x => x.UserName == User.Identity.Name);
                BuyProductViewModel model = new BuyProductViewModel { IdProduct = product.Id, IdUser = user.Id, Product = product};
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> BuyProduct(BuyProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BuyProduct buyProduct = new BuyProduct { IdProduct = model.IdProduct, IdUser = model.IdUser, Name = model.Name, Surname = model.Surname, Adress = model.Adress, Time = DateTime.Now };
                    db.BuyProducts.Add(buyProduct);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }

            }
            return View(model);

        }   }
}
