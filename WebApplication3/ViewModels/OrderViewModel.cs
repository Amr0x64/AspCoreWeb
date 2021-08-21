using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [BindNever]
        public bool Shipped { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите первкю строку адреса")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Введите первкю строку города")]
        public string City { get; set; }
        [Required(ErrorMessage = "Введите первкю строку страны")]

        public string Country { get; set; }
        public string Zip { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> productList { get; set; }
        public string productTitle { get; set; }
    }
}
