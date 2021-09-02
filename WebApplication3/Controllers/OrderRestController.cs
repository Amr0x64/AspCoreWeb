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


namespace WebApplication3.Controllers
{
    [Route("api/order")]
    public class OrderRestController : Controller
    {
        private ApplicationContext db;
        private IConfiguration Configuration;
        public OrderRestController(ApplicationContext context, IConfiguration configuration)
        {
            db = context;
            Configuration = configuration;
        }
        [HttpGet("Obj")]
        [Produces("application/json")]
        public Product GetObject()
        {
            return new Product
            {
                Title = "Чебурашка",
                Price = 1
            };
        }
    }
}