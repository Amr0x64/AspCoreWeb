using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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
            .FirstOrDefault(p => p.Product.ProductId == product.ProductId);
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
        //Обновление корзины
        public virtual void DeleteProduct(Product product)
        {
            CartLine line = lineCollection.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (line.Quantity == 1)
            {
                RemoveLine(product);
            }
            else
            {
                line.Quantity -= 1;
            }
        }   
        public virtual void AddProduct(Product product)
        {
            CartLine line = lineCollection.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            line.Quantity += 1;
        }
        //Вычисление общей суммы
        public virtual int ComputeTotalValue() =>  lineCollection.Sum(e => e.Product.Price * e.Quantity);
        //Полность очистить
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;   
    }
    //public class CartLine
    //{
    //    public int ID { get; set; }
    //    public int ProductId { get; set; }
    //    public Product Product { get; set; }
    //    public int Quantity { get; set; }
    //    public int OrderId { get; set; }
    //}
}
