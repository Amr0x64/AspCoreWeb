using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
//using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace WebApplication3.Controllers
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        //private ApplicationContext db;
        //public BlogController(ApplicationContext context)
        //{
        //    db = context;
        //}
        
        //public async Task<IActionResult> Index()
        //{
        //    //await db.Blogs.ToListAsync()
        //    return View(await db.Blogs.ToListAsync());
        //}
        //[Authorize]
        //public IActionResult CreatePost()
        //{
        //    return View();
        //}
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
