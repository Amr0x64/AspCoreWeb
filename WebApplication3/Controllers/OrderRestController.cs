using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderRestController : Controller   
    {
        private RPRCContext db;
        private IConfiguration Configuration;
        private UserManager<User> _userManager;
        public OrderRestController(RPRCContext context, IConfiguration configuration, UserManager<User> userManager)
        {
            db = context;
            Configuration = configuration;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userManager.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound();
            else return user;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            
            
            return Ok(user);
        }

        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null) return BadRequest();

            await _userManager.CreateAsync(user);
            return Ok(User);
        }
        [Route("pr")]
        public IActionResult prSpis()
        {
            Product product = db.Products.FirstOrDefault();
            db.UserViewProducts.Where(p => p.ProductId == product.ProductId).Load();    
            foreach (var item in product.UserViewProducts) Debug.WriteLine(item.UserIP);
            return Ok();
        }

        [Route("LazyLoad")]
        public ContentResult LazyLoad()
        {
            string htmlContent = "";
            var products = db.apartments.FromSqlRaw("SELECT * FROM apartments").ToList();
            foreach (var item in products) htmlContent += item.FlatNumber + "\n";
            
            return Content(htmlContent, "text/html");
        }
    }
}