using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class UserViewProduct
    {
        public int UserViewProductId { get; set; }
        public int ProductId { get; set; }
        public string UserIP { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
