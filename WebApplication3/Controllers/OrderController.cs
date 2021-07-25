using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationContext db;
        private Cart cart;
        public OrderController(ApplicationContext context, Cart cartService)
        {
            db = context;
            cart = cartService;
        }
        public IActionResult Checkout() => View(new Order());
        [Authorize(Roles ="admin, superuser")]
        public IActionResult List() => View(db.Orders.AsNoTracking().Include(o => o.Lines).ThenInclude(p => p.Product).Where(l => !l.Shipped));
        [Authorize(Roles = "admin, superuser")]
        [HttpPost]
        public async Task<IActionResult> MarkShipped(int orderId)
        {
            Order order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                db.Orders.Update(order);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public async  Task<IActionResult> Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }
            if (ModelState.IsValid)
            {
                var correctName = order.Name.Replace(" ", "");
                var firstChar = correctName.Substring(0, 1).ToUpper();
                correctName = firstChar + correctName.Substring(1).ToLowerInvariant();

                order.Name = correctName;
                order.Lines = cart.Lines.ToArray();
                db.AttachRange(order.Lines.Select(l => l.Product));
                if (order.OrderId == 0)
                {
                    db.Orders.Add(order);
                }
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
