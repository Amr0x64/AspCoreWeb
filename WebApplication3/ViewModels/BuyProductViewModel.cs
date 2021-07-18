using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class BuyProductViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; }  
        public string Surname { get; set; }  
        public string Adress { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
