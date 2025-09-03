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
                do
                {
                    string email;
                    while (true)
                    {
                        Console.Write("Enter email (0 - back): ");
                        email = Console.ReadLine();
                        if (email == "0") return;

                        if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                            break;

                        ConsoleHelper.WriteError("Invalid email, try again.");
                    }

                    List<OrderItem> orderItems = new();

                    while (true)
                    {
                        Product p = null;
                        while (true)
                        {
                            Console.Write("Enter product Id (0 - back): ");
                            string input = Console.ReadLine();
                            if (input == "0") return;

                            if (int.TryParse(input, out int id))
                            {
                                p = _productService.GetProducts().Find(x => x.Id == id);
                                if (p != null) break;
                            }
                            ConsoleHelper.WriteError("Invalid Id or product not found.");
                        }

                        int count;
                        while (true)
                        {
                            Console.Write("Enter count (0 - back): ");
                            string input = Console.ReadLine();
                            if (input == "0") return;

                            if (int.TryParse(input, out count) && count > 0 && count <= p.Stock)
                                break;

                            ConsoleHelper.WriteError("Invalid count or not enough stock.");
                        }

                        OrderItem item = new OrderItem(p, count);
                        orderItems.Add(item);
                        p.Stock -= count;

                        Console.Write("Add another product to this order? (y/n): ");
                        if (Console.ReadLine().ToLower() != "y") break;
                    }

                    if (orderItems.Count > 0)
                    {
                        Order o = new Order(email, orderItems);
                        _orders.Add(o);
                        _productService.SaveProducts();
                        Save();
                        ConsoleHelper.WriteInfo("Order created successfully!");
                    }

                    Console.Write("Do you want to create another order? (y/n): ");
                }
                while (Console.ReadLine().ToLower() == "y");
            }

            public void ShowAllOrders()
            {
                if (_orders.Count == 0)
                {
                    ConsoleHelper.WriteError("No orders available.");
                    return;
                }

                ConsoleHelper.WriteInfo("All Orders:");
                foreach (var o in _orders)
                {
                    o.PrintInfo();
                    Console.WriteLine("-----------------------------");
                }
            }

            public void ChangeOrderStatus()
            {
                do
                {
                    Console.Write("Enter order Id (0 - back): ");
                    string input = Console.ReadLine();
                    if (input == "0") return;

                    if (!int.TryParse(input, out int id))
                    {
                        ConsoleHelper.WriteError("Invalid Id.");
                        continue;
                    }

                    var o = _orders.Find(x => x.Id == id);
                    if (o == null)
                    {
                        ConsoleHelper.WriteError("Order not found.");
                        continue;
                    }

                    Console.WriteLine("Choose status: 1.Pending, 2.Confirmed, 3.Completed (0 - back)");
                    string stInput = Console.ReadLine();
                    if (stInput == "0") return;

                    if (!int.TryParse(stInput, out int st) || st < 1 || st > 3)
                    {
                        ConsoleHelper.WriteError("Invalid status.");
                        continue;
                    }

                    o.Status = (OrderStatus)st;
                    Save();
                    ConsoleHelper.WriteInfo("Status updated!");

                    Console.Write("Do you want to update another order? (y/n): ");
                }
                while (Console.ReadLine().ToLower() == "y");
            }

            private void Save()
            {
                FileHelper.Serialize(_orderPath, _orders);
            }
        }
    }

}

