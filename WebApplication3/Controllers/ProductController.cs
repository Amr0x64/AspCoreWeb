﻿using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;
        private IHttpContextAccessor _accessor;
        public ProductController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment appEnvironment, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            db = context;
            _appEnvironment = appEnvironment;
            _accessor = accessor;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.IsDeleteProduct = false;
            return View(await db.Products.ToListAsync());
        }
        [HttpPost]  
        public async Task<IActionResult> Index(string nameProduct)
        {
            ViewBag.IsDeleteProduct = false;
            if (nameProduct != null)
            {
                var searchProductList =  db.Products.AsNoTracking().Where(p => EF.Functions.Like(p.Title, $"%{nameProduct}%")).OrderBy(x => x.Price);
                return View(searchProductList);
            }
            return View(await db.Products.ToListAsync());
        }

        #region =||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=|| Показать удаленные продукты ||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=||

        [HttpGet]
        public async Task<IActionResult> SelectDeleteProduct()
        {
            ViewBag.IsDeleteProduct = true;
            return View("Index", await db.Products.ToListAsync());
        }

        #endregion

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
        //Восстановить товар
        [HttpGet]
        public async Task<IActionResult> RestoreProduct (int id)
        {
            ViewBag.IsDeleteProduct = false;
            Product product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                product.isRemoved = false;
                db.Products.Update(product);
                await db.SaveChangesAsync();
                return View("Index", await db.Products.ToListAsync());
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, superuser")]
        public IActionResult Edit(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ProductId == id);
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
                var product = db.Products.FirstOrDefault(x => x.ProductId == model.Id);
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
            Product product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                product.isRemoved = true;
                db.Products.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public async Task<IActionResult> Detail(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                var ip = GetIp();
                var userView = db.UserViewProducts.FirstOrDefault(x => x.ProductId == id && x.UserIP == ip);
                if (userView == null)
                {
                        UserViewProduct userViewProduct = new UserViewProduct { UserIP = ip, ProductId = id };
                        db.Add(userViewProduct);
                        await db.SaveChangesAsync();

                        product.View = product.View + 1;
                        db.Update(product);
                        await db.SaveChangesAsync();
                    
                }
                return View(product);
            }
            return NotFound();
        }
        [NonAction]
        public String GetIp() => _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    } 
        
}
