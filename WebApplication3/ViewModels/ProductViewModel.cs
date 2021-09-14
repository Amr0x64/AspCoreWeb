using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> ProductList { get; set; }
        public Cart Cart { get; set; }
        public PageViewModel PageViewModel {get; set;}
    }
}
