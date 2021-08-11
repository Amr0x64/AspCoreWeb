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
        public async Task<IActionResult> Index()
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            ViewData["PriceAll"] = 0;
            var queryOrderProduct = await db.Orders.ToListAsync();
            foreach (var i in queryOrderProduct)
            {
                
            }

            return View(await db.Products.ToListAsync());
        }
        //cVOO-1-View
        [HttpPost]
        public async Task<IActionResult> SelectViweProduct(int choiseVieworOrder, int date)
        {
            if (true)
            {
                ViewBag.productList = CountViewProduct(date);
                return View(await db.Products.ToListAsync());
            }
        }
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
