using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication3
{
    public partial class UserViewProduct
    {
        [Key]
        public int UserViewProductId { get; set; }
        public int ProductId { get; set; }
        //перехватываем ip пользователя
        public string UserIP { get; set; }
        public DateTime ViewDate { get; set; }
        
        /*[ForeignKey("ProductId")]
        public Product Product { get; set; }*/
    }
}
