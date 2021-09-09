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
using Microsoft.AspNetCore.Http;
using WebApplication3.Infrastructure;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "admin, superuser")]
    [HttpsOnly]
    public class AdminController : Controller
    {
        private ApplicationContext db;
        private IRepository repository;
        
        public AdminController(ApplicationContext context, IRepository repo)
        {
            db = context;
            repository = repo;
        }
        [RequireHttps]
        //[Profile]
        public IActionResult Index([FromServices]Totalizeir totalizeir)
        {
            ViewData["OrderCount"] = db.Orders.Where(x => x.Shipped == false).Count();
            ViewBag.HomeController = repository.ToString();
            ViewBag.Totalizier = totalizeir.Repository.ToString();
            return View(db.Products.ToList());
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
        public async Task<IActionResult> SelectProduct(string date)     
        {
                List<CartLine> cartLineList = new List<CartLine>();
                List<Product> productListToOrder = new List<Product>();
                if (date == "day")
                {
                ViewData["TopOrders"] = "Топ заказов на сегодня";
                foreach (var order in db.Orders.Where(d => (DateTime.Now.Day == d.OrderDate.Day) && (DateTime.Now.Month == d.OrderDate.Month) && (DateTime.Now.Year == d.OrderDate.Year))
                    .ToList())
                    {
                        cartLineList.Add(db.CartLines.FirstOrDefault(o => o.OrderId == order.OrderId));
                    }    
                }
                else if (date == "month")
                {
                    ViewData["TopOrders"] = "Топ заказов за месяц";
                    foreach (var order in db.Orders.Where(d => (DateTime.Now.Month == d.OrderDate.Month) && (DateTime.Now.Year == d.OrderDate.Year)).ToList())
                    {
                        cartLineList.Add(db.CartLines.FirstOrDefault(o => o.OrderId == order.OrderId));
                    }

                }
                else if (date == "allTime")
                {
                    ViewData["TopOrders"] = "Топ заказов за все время";
                    foreach (var order in db.Orders.ToList())
                    {
                        cartLineList.Add(db.CartLines.FirstOrDefault(o => o.OrderId == order.OrderId));
                    }
                }
                else 
                {
                    return View("ProductStatistics", await db.Products.ToListAsync());
                }
                var cartLineListUnique = new List<CartLine>();
                foreach(var line in cartLineList)
                {
                    bool checkId = true;
                    foreach (var lineUnique in cartLineListUnique)
                    {
                        if (lineUnique.ProductId == line.ProductId)
                        {
                            var ct = cartLineListUnique.FirstOrDefault(p => p.ProductId == line.ProductId);
                            ct.Quantity += line.Quantity;
                            checkId = false;
                            break;
                        }
                    }
                    if (checkId)
                    {
                        cartLineListUnique.Add(line);
                    }
                }
                cartLineListUnique = cartLineListUnique.OrderByDescending(q => q.Quantity).ToList();
                foreach (var productId in cartLineListUnique) productListToOrder.Add(db.Products.FirstOrDefault(p => p.ProductId == productId.ProductId));
                return View("ProductStatistics", productListToOrder);
        }
        #endregion
       
    }
}
