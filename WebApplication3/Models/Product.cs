using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication3.Models
{
    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Like { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
