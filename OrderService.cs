using MiniConsoleAppProject.Enums;
using MiniConsoleAppProject.Helper;
using MiniConsoleAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject
{
    internal class OrderService
    {
        private readonly string _orderPath;
        private List<Order> _orders;
        private readonly ProductService _productService;

        public OrderService(string orderPath, ProductService productService)
        {
            _orderPath = orderPath;
            _productService = productService;
            _orders = FileHelper.Deserialize<Order>(_orderPath);
        }

        public void OrderProduct()
        {
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Invalid email");
                return;
            }

            List<OrderItem> orderItems = new();

            while (true)
            {
                Console.Write("Enter product Id: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid Id");
                    continue;
                }

                var p = _productService.GetProducts().Find(p => p.Id == id);
                if (p == null)
                {
                    Console.WriteLine("Product not found");
                    continue;
                }

                Console.Write("Enter count: ");
                if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                {
                    Console.WriteLine("Invalid count");
                    continue;
                }

                if (count > p.Stock)
                {
                    Console.WriteLine("Not enough stock");
                    continue;
                }

                OrderItem item = new OrderItem(p, count);
                orderItems.Add(item);
                p.Stock -= count;

                Console.Write("Add another (y/n): ");
                if (Console.ReadLine().ToLower() != "y") break;
            }

            if (orderItems.Count > 0)
            {
                Order o = new Order(email, orderItems);
                _orders.Add(o);
                _productService.SaveProducts();
                Save();
                Console.WriteLine("Order created successfully!");
            }
        }

        public void ShowAllOrders()
        {
            if (_orders.Count == 0)
            {
                Console.WriteLine("No orders available");
                return;
            }

            foreach (var o in _orders)
                o.PrintInfo();
        }

        public void ChangeOrderStatus()
        {
            Console.Write("Enter order Id: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            var o = _orders.Find(o => o.Id == id);
            if (o == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            Console.WriteLine("Choose status: 1.Pending, 2.Confirmed, 3.Completed");
            if (!int.TryParse(Console.ReadLine(), out int st) || st < 1 || st > 3)
            {
                Console.WriteLine("Invalid status");
                return;
            }

            o.Status = (OrderStatus)st;
            Save();
            Console.WriteLine("Status updated");
        }

        private void Save()
        {
            FileHelper.Serialize(_orderPath, _orders);
        }
    }
}

