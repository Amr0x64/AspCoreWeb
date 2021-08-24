using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class MemoryRepository : IRepository
    {
        private Dictionary<string, Product> products;
        private string guid = System.Guid.NewGuid().ToString();
        public MemoryRepository()
        {
            products = new Dictionary<string, Product>();
            new List<Product>
            {
                new Product {ProductId = 1, Title = "Philips", Description = "mvioerjvoerjvoj",Category = "cwefec", Price = 3413213, AddDate = DateTime.Now, Count = 12}
            }.ForEach(p => AddProduct(p));
        }
        public IEnumerable<Product> Products => products.Values;
        public Product this[string name] => products[name];     
        public void AddProduct(Product product)
        {
            products[product.Title] = product;
        }
        public void DeleteProduct(Product product)
        {
            products.Remove(product.Title);
        }
        public override string ToString()
        {
            return guid;
        }
    }
}
