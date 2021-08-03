using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
   
    public class Product 
    {
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Category { get; set; }
        public int Count { get; set; }
        public string PathImg { get; set; }
        public string AddUser { get; set; }
        public DateTime? AddDate { get; set; }
        public string ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int View { get; set; }
        public bool isRemoved { get; set; }
        
    }
}
