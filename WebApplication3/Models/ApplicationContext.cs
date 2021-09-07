using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }   
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserViewProduct> UserViewProducts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<FiasStatment> FiasStatments { get; set; }
        public DbSet<StreetNumber> StreetNumbers { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<StreetNumber>().
        //        HasOne(f => f.FiasStatment).
        //        WithMany(s => s.StreetNumbers).
        //        HasForeignKey(f => f.FiasGuid).
        //        HasPrincipalKey(s => s.fias_guid);

        //    base.OnModelCreating(builder);
        //}
    }  
}
