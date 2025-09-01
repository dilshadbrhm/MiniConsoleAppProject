using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class OrderItem
    {
        private static int _count = 1;
        public int Id { get;private set; }
        public Product Product { get;set; }
        public int Count { get; set; }
        public decimal Price {  get; set; }
        public decimal SubTotal {  get; set; }

        public OrderItem(Product product,int count)
        {
            Id=_count++;
            Product=product;
            Count=count;
            Price=Product.Price;
            SubTotal = Price * Count;
        }
    }
}
