using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class BuyProduct
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public DateTime Time { get; set; }
    }
}
