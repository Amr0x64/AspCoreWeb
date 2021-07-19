using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class BuyProduct
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        public int Count { get; set; }
    }
}
