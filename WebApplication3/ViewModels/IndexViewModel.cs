using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class IndexViewModel
    {
        public int? ProductId { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
