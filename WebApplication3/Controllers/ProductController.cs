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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        private RPRCContext db;
        private readonly UserManager<User> _userManager;     
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;
        private IHttpContextAccessor _accessor;
        private Cart _cart;
        public ProductController(RPRCContext context, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment appEnvironment,
            IHttpContextAccessor accessor, Cart cart)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            db = context;
            _appEnvironment = appEnvironment;
            _accessor = accessor;
            _cart = cart;
        }
        
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.IsDeleteProduct = false;
            ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());

            int pageSize = 5;
            var count = await db.Products.CountAsync();
            var products = await db.Products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            
            ProductViewModel model = new ProductViewModel { ProductList = products, Cart = _cart, PageViewModel = pageViewModel };
            return View(model);
        }
        [HttpGet]  
        public async Task<IActionResult> Search(string nameProduct)
        {
            ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());
            ViewBag.IsDeleteProduct = false;
            if (nameProduct != null)
            {
                var searchProductList = await db.Products.Where(p => EF.Functions.Like(p.Title, $"%{nameProduct}%"))
                    .OrderByDescending(x => x.Price).ToListAsync();
                ProductViewModel model = new ProductViewModel { ProductList = searchProductList, Cart = _cart };
                return View("Index", model);
            }
            return View("Index", new ProductViewModel { ProductList = await db.Products.ToListAsync(), Cart = _cart });
        }

        #region =||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=|| Показать удаленные продукты ||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=||=||
        [Authorize(Roles = "admin, superuser")]
        [HttpGet]
        public async Task<IActionResult> SelectDeleteProduct()
        {
            ViewBag.IsDeleteProduct = true;
            ProductViewModel model = new ProductViewModel { ProductList = await db.Products.ToListAsync() };
            return View("Index", model);
        }

        #endregion

        [Authorize(Roles = "admin, superuser")]
        public IActionResult CreateProduct()
        {
            ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());
            return View();
        }
    
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
                Product product = new Product { ProductId = model.Id, Title = model.Title, Description = model.Description,Category = model.Category, 
                    Price = model.Price, AddDate = DateTime.Now, Count = model.Count, PathImg = path, AddUser = User.Identity.Name};
                
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }   
            return View(model);
        }
        //Восстановить товар
        [HttpPost]
        public async Task<IActionResult> RestoreProduct (int id)
        {
            ViewBag.IsDeleteProduct = true;
            Product product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());
                product.isRemoved = false;
                db.Products.Update(product);
                await db.SaveChangesAsync();
                return View("Index", new ProductViewModel { ProductList = await db.Products.ToListAsync(), Cart = _cart });
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, superuser")]
        public IActionResult Edit(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());
                EditProductViewModel model = new EditProductViewModel { Id = product.ProductId, Title = product.Title, Description = product.Description, 
                    Price = product.Price, Count = product.Count};
                
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
            ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());
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
                ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());
                var ip = GetIp();
                var userView = db.UserViewProducts.FirstOrDefault(x => x.ProductId == id && x.UserIP == ip);
                if (userView == null)
                {
                        UserViewProduct userViewProduct = new UserViewProduct { UserIP = ip, ProductId = id , ViewDate = DateTime.Now};
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
        [HttpGet]
        public async Task<IActionResult> DetailCategory(string categoryName)
        {
            ViewBag.IsDeleteProduct = false;
            ViewBag.Categorys = UniqueElem(db.Products.Select(g => g.Category).ToList());
            return View("Index", new ProductViewModel { ProductList = await db.Products.Where(p => p.Category == categoryName).ToListAsync(), Cart = _cart } );
        }
        //В корзине
        [HttpGet]
        public IActionResult DeleteOneProduct(int productId)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                _cart.DeleteProduct(product);
            }
            return Ok(_cart.Lines.FirstOrDefault(p => p.ProductId == productId).Quantity.ToString());
        }

        [HttpGet]
        public IActionResult AddOneProduct(int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _cart.AddProduct(product);
            }
            return Ok(_cart.Lines.FirstOrDefault(p => p.ProductId == productId).Quantity.ToString());
        }
        [NonAction]
        public String GetIp() => _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

        [NonAction]
        private List<String> UniqueElem(List<String> attrList)
        {
            var unqueList = new List<String>();
            foreach (var e in attrList)
            {
                if (unqueList.Contains(e))
                {
                    continue;
                }
                else
                {
                    unqueList.Add(e);
                }
            }
            return unqueList;
        }
    } 
}