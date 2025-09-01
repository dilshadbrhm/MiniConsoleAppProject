using MiniConsoleAppProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class Order
    {
        private static int _count = 1;
        public int Id { get; private set; }
        public List<OrderItem> Items = new();
        public decimal Total { get; set; }
        public string Email { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderedAt { get; set; } = DateTime.Now;
        public Order(string email, List<OrderItem> items)
        {
            if (email == null || !email.Contains("@"))
                throw new Exception("Wrong email");
            Id = _count++;
            Email = email;
            Total = CalculateTotal();
        }

        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (OrderItem item in Items)
            {
                total += item.SubTotal;

            }
            return total;
        }

        internal void PrintInfo()
        {
            throw new NotImplementedException();
        }
    }
}
