using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace WebApplication3.Controllers
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private ApplicationContext db;
        public ProductController(ApplicationContext context)
        {
            db = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }
        public IActionResult Detail(int id)
        {
            Product product = db.Products.Single(x => x.Id == id);
            if (product != null)
            {
                return View(product);
            }
            return NotFound();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreatePost(Blog post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Blogs.Add(post);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(post);

        //}

    }
}
