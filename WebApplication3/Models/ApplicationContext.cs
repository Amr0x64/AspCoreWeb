using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {       
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products{ get; set; }
        public DbSet<BuyProduct> BuyProducts { get; set; }
    }
}
