using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

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
        [HttpPost]
        public async  Task<IActionResult> Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }
            if (ModelState.IsValid)
            {
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
