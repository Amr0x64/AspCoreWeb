using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Infrastructure;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class CartController : Controller
    {
        private ApplicationContext db;
        private Cart cart;
        public CartController(ApplicationContext context, Cart cartService)
        {
            db = context;
            cart = cartService;
        }
        public RedirectToActionResult AddToCart(int productld, string returnUrl)
        {
            Product product = db.Products.Single(x => x.ProductId == productld);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productld, string returnUrl)
        {
            Product product = db.Products.Single(x => x.ProductId == productld);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }        
        public IActionResult Index (string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

    }
}
