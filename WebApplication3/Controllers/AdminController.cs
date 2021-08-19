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
    [Authorize(Roles = "admin, superuser")]
    public class AdminController : Controller
    {
        private ApplicationContext db;
        public AdminController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewData["OrderCount"] = db.Orders.Where(x => x.Shipped == false).Count();
            return View(db.Products.ToList());
        }
        public JsonResult JsonResponse()
        {
            var a = db.Products.FirstOrDefault(i => i.ProductId == 15);
            return Json(a);
        }

        public IActionResult Home()
        {
            var model = new AdminViewModel();
            model.Order = db.Orders.Count();
            model.TotalProfit = 0;
            foreach (var cartLine in db.CartLines.ToList())
            {
                model.TotalProfit += cartLine.Quantity * db.Products.FirstOrDefault(p => p.ProductId == cartLine.ProductId).Price;
            }
            return View(model);
        }
        #region}{}{}{}{}{}{}{}{}{}{}{}{}{}{Выборка продукта}{}{}{}{}{}{}{}{}{}{}{}{}{}{
        public async Task<IActionResult> SelectViweProduct(string date)     
        {
            if (date == "day")
            {
                return View("Index", db.Products.OrderByDescending(v => v.View).ToList());
            }
            return View("Index", await db.Products.ToListAsync());
        }
        #endregion
        [NonAction]
        public  List<int> CountViewProduct(int date)
        {
            List<int> productList = new List<int>();
            if (true)
            {
               TimeSpan ts = new TimeSpan(24, 0, 0);
               var queryList = db.Products.Join(db.UserViewProducts, p => p.ProductId, u => u.ProductId, (p, u) => new
                {
                    ProductId = p.Title,
                    View = p.View,
                    ProductIdView = u.ProductId,
                    UserIp = u.UserIP,
                    ViewDate = u.ViewDate
                }).Where(p => SravDate(TimeSpan.Compare(p.ViewDate.Subtract(DateTime.Now), ts)))
                      .GroupBy(p => p.ProductId).Select(g => new { Count = g.Count()}).OrderByDescending(m => m.Count );

              foreach (var product in  queryList)
                {
                    productList.Add(product.Count);
                }
                return productList;
            }
            //else if (date == 2)
            //{

            //}
            //else if (date == 3)
            //{
            //    //await db.Products.Where(d => d.AddDate).OrderByDescending(p => p.View).ToListAsync();
            //}
            //else if (date == 4)
            //{
            //   // await db.Products.Where(d => d.AddDate).OrderByDescending(p => p.View).ToListAsync();
            //}
        }
        [NonAction]
        private Boolean SravDate(int val) => val == -1;
    }
}
