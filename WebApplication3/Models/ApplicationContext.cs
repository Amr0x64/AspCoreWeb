using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WebApplication3.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        private readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<BuyProduct> BuyProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(logStream.WriteLine, Microsoft.Extensions.Logging.LogLevel.Warning);
        }
        public override void Dispose()
        {
            base.Dispose();
            logStream.Dispose();
        }
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logStream.DisposeAsync();
        }
       
    }
}
