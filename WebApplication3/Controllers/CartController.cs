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
        [HttpGet]
        public IActionResult AddToCart(int productld)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductId == productld);
            var cartProduct = cart.Lines.FirstOrDefault(p => p.Product.ProductId == productld);
            cart.AddItem(product, 1);
            return Ok();
        }
        [HttpPost]
        public RedirectToActionResult RemoveFromCart(int productld, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductId == productld);
            if (product != null)
            { 
                cart.RemoveLine(product);    
            }
            return RedirectToAction("Index", new { returnUrl });
        }        
        [HttpPost]
        public RedirectToActionResult DeleteOneProduct(int productld, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductId == productld);
            if (product != null)
            {
                cart.DeleteProduct(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [HttpPost]
        public RedirectToActionResult AddOneProduct(int productId, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddProduct(product);   
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [HttpGet]
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
