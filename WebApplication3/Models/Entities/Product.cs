using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication3
{
    public partial class Product
    {
        [Required]
        [Key]
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
        public DateTime AddDate { get; set; }
        public string ChangeUser { get; set; }
        public DateTime ChangeDate { get; set; }
        public int View { get; set; }
        public bool isRemoved { get; set; }
        
        public List<CartLine> CartLines { get; set; }
        public List<UserViewProduct> UserViewProducts { get; set; }
    }
}
