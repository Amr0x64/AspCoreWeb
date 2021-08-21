using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationContext db;
        private Cart cart;
        public IConfiguration Configuration { get; }
        public OrderController(ApplicationContext context, Cart cartService, IConfiguration configuration)
        {
            db = context;
            cart = cartService;
            Configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            List<Product> _productList = new List<Product>();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    Product product = new Product { ProductId = (int)cells[0], Title = (string)cells[1] };
                    _productList.Add(product);
                }
            }
            //ds.WriteXml("products.xml");
            connection.Close();

            OrderViewModel model = new OrderViewModel { productList = await db.Products.ToListAsync() };
            return View(model);
        }

        [Authorize(Roles = "admin, superuser")]
        public IActionResult List()
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            return View(db.Orders.AsNoTracking().Include(o => o.Lines).ThenInclude(p => p.Product).Where(l => !l.Shipped));
        }

        [Authorize(Roles = "admin, superuser")]
        [HttpPost]
        public async Task<IActionResult> MarkShipped(int orderId)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
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
        public async  Task<IActionResult> Checkout(OrderViewModel order)
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");  
            }
            if (ModelState.IsValid)
            {
                var dqr = order.productTitle;
                var correctName = order.Name.Replace(" ", "");
                var firstChar = correctName.Substring(0, 1).ToUpper();
                correctName = firstChar + correctName.Substring(1).ToLowerInvariant();

                Order orderDb = new Order { City = order.City, Country = order.Country, Line1 = order.Line1, Line2 = order.Line2, Line3 = order.Line3, OrderId = order.OrderId, Shipped = false, Zip = order.Zip };
                orderDb.Name = correctName;
                orderDb.OrderDate = DateTime.Now;
                orderDb.Lines = cart.Lines.ToArray();
                db.AttachRange(orderDb.Lines.Select(l => l.Product));
                db.Add(orderDb);
                await db.SaveChangesAsync();
                    
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(new OrderViewModel() { productList = await db.Products.ToListAsync() });
            }
        }
        public ViewResult Completed()
        {
            ViewData["OrderCount"] = db.Orders.Where(o => o.Shipped == false).Count();
            cart.Clear();
            return View();
        }
    }
}
