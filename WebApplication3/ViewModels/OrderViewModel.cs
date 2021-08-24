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
        public string Adress { get; set; }
        public DateTime OrderDate { get; set; }
        public List<FiasStatment> adressList { get; set; }
        public string productTitle { get; set; }
    }
}
