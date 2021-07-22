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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public ProductController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            db = context;
        }
        
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("admin")) ViewBag.checkAdmin = true;
                else
                {
                    ViewBag.checkAdmin = false;
                }
            }          
            return View(await db.Products.ToListAsync());
        }
        [HttpPost]  
        public async Task<IActionResult> Index(string nameProduct)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains("admin")) ViewBag.checkAdmin = true;
            else
            {
                ViewBag.checkAdmin = false;
            }
            if (nameProduct != null)
            {
                var searchProductList =  db.Products.AsNoTracking().Where(p => EF.Functions.Like(p.Title, $"%{nameProduct}%")).OrderBy(x => x.Price);
                return View(searchProductList);
            }
            return View(await db.Products.ToListAsync());
        }
        public IActionResult Detail(int id)
        {
            Product product = db.Products.Single(x => x.ProductId == id);
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
            Product product = db.Products.Single(x => x.ProductId == id);
            if (product != null && product.Count != 0)
            {
                User user = db.Users.Single(x => x.UserName == User.Identity.Name);
                BuyProductViewModel model = new BuyProductViewModel { IdProduct = product.ProductId, IdUser = user.Id, Product = product};
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BuyProduct(BuyProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BuyProduct buyProduct = new BuyProduct { ProductId = model.IdProduct, UserId = model.IdUser, Name = model.Name, Surname = model.Surname, Adress = model.Adress, Time = DateTime.Now};
                    Product product = db.Products.Single(x => x.ProductId == model.IdProduct);
                    product.Count = product.Count - 1;

                    db.Products.Update(product);
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
        }
        [Authorize(Roles ="admin")]
        public IActionResult CreateProduct()
        {
            return View();
        }
        //Добавление товара админом
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product { ProductId = model.Id, Title = model.Title, Description = model.Description, Price = model.Price, Time = DateTime.Now, Count = model.Count };
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var product = db.Products.Single(x => x.ProductId == id);
            if (product != null)
            {
                EditProductViewModel model = new EditProductViewModel { Id = product.ProductId, Title = product.Title, Description = product.Description, Price = product.Price, Count = product.Count };
                return View(model);
            }
            return NotFound();
        }
        //Изминение товара админом
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                var product = db.Products.Single(x => x.ProductId == model.Id);
                if (product != null)
                {
                    product.Title = model.Title;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.Count = model.Count;
                    db.Products.Update(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        //Удаление товара админом
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = db.Products.Single(x => x.ProductId == id);
            if (product != null)
            {
                product.isRemoved = true;
                db.Products.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    } 
        
}
