using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        // Добавить в корзину
        public virtual void AddItem(Product product, int quantity)
        {
            //Добаваляет в список корзин продукт , при его отсутствии , иначе увеличивает счетчик на 1
            CartLine line = lineCollection
            .Where(p => p.Product.ProductId == product.ProductId)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        //Удаление с корзины
        public virtual void RemoveLine(Product product) =>
        lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        //Вычисление общей суммы
        public virtual int ComputeTotalValue() =>  lineCollection.Sum(e => e.Product.Price * e.Quantity);
        //Полность очистить
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public int CartLinelD { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
