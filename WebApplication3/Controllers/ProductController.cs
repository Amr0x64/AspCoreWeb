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
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication3.Controllers
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;
        public ProductController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            db = context;
            _appEnvironment = appEnvironment;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }
        [HttpPost]  
        public async Task<IActionResult> Index(string nameProduct)
        {
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
        //qpkfj[iwejf[iouernpuifgupuirhgiuweh
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
        public async Task<IActionResult> BuyProduct(BuyProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var correctName = model.Name.Replace(" ", "");
                    var firstChar = correctName.Substring(0, 1).ToUpper();
                    correctName = firstChar + correctName.Substring(1).ToLowerInvariant();

                    var correctSurname = model.Surname.Replace(" ", "");
                    var firstCharS = correctSurname.Substring(0, 1).ToUpper();
                    correctSurname = firstCharS + correctSurname.Substring(1).ToLowerInvariant();

                    BuyProduct buyProduct = new BuyProduct { ProductId = model.IdProduct, UserId = model.IdUser, Name = correctName, Surname = correctSurname, Adress = model.Adress, Time = DateTime.Now};
                    Product product = db.Products.FirstOrDefault(x => x.ProductId == model.IdProduct);
                    product.Count = product.Count - 1;

                    db.Products.Update(product);
                   

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
        [Authorize(Roles = "admin, superuser")]
        public IActionResult CreateProduct()
        {
            return View();
        }
        //Добавление товара админом
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, superuser")]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            string path = "";
            if (ModelState.IsValid)
            {
                if (model.UploadedFile != null)
                {
                    path = "/img/" + model.UploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.UploadedFile.CopyToAsync(fileStream);
                    }
                }
                Product product = new Product { ProductId = model.Id, Title = model.Title, Description = model.Description, Price = model.Price, AddDate = DateTime.Now, Count = model.Count, PathImg = path, AddUser = User.Identity.Name};
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }   
            return View(model);
        }
        [Authorize(Roles = "admin, superuser")]
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
        [Authorize(Roles = "admin, superuser")]
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
                    product.ChangeDate = DateTime.Now;
                    product.ChangeUser = User.Identity.Name;
                    db.Products.Update(product);
                    await db.SaveChangesAsync();
                    TempData["message"] = $"{model.Title} отредоктирован";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        //Удаление товара админом
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, superuser")]
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
