using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.ViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string PathImg { get; set; }
        public DateTime Time { get; set; }
        public string ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
    }
}
