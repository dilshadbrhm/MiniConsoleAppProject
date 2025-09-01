using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class Product
    {
        private static int _count = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name cannot be empty");

            if (price <= 0)
                throw new Exception("Price cannot be less than zero");

            if (stock < 0)
                throw new Exception("Stock cannot be negative");

            Id = _count++;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"{Id} {Name} {Price} {Stock}");
        }

       
    }
}

