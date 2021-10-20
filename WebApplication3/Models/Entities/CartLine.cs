using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication3
{
    public partial class CartLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
    }
}
