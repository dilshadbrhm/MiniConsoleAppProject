using MiniConsoleAppProject.Enums;
using MiniConsoleAppProject.Helper;
using MiniConsoleAppProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace MiniConsoleAppProject
{
    internal class ManagementApplication
    {
        public string productPath = @"C:\Users\dilsa\OneDrive\Masaüstü\VSrep\MiniConsoleAppProject\products.json";
        public string orderPath = @"C:\Users\dilsa\OneDrive\Masaüstü\VSrep\MiniConsoleAppProject\orders.json";

        List<Product> products;
        List<Order> orders;
        public ManagementApplication()
        {
            products = FileHelper.Deserialize<Product>(productPath);
            orders = FileHelper.Deserialize<Order>(orderPath);
        }



        public void Run()
        {

            string str = null;
            int num = 0;
            bool result = false;


            while (!(num == 0 && result))
            {

                Console.WriteLine("1.Create Product\n2.Delete Product\n3.Get Product By Id\n4.Show All Product\n5.Refill Product\n6.Order Product\n7.Show All Orders\n8.Change Order Status\n\n0.Exit");
                str = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(str, out num);
                switch (num)
                {
                    case 1:
                        CreateProduct();
                        break;
                    case 2:
                        DeleteProduct();
                        break;
                    case 3:
                        GetProductById();
                        break;
                    case 4:
                        ShowAllProducts();
                        break;
                    case 5:
                        RefillProduct();
                        break;
                    case 6:
                        OrderProduct();
                        break;
                    case 7:
                        ShowAllOrders();
                        break;
                    case 8:
                        ChangeOrderStatus();
                        break;
                    case 0:
                        Console.WriteLine("Program ended");
                        return;

                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }


            }
        }

        public void CreateProduct()
        {
            Console.Write("Enter product name:");
            string name = Console.ReadLine();

            if (products.Exists(p => p.Name == name))
            {
                Console.WriteLine("This product already exists");
                return;
            }

            Console.Write("Enter price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price");
                return;
            }

            Console.Write("Enter stock: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Invalid stock");
                return;
            }

            Product p = new Product(name, price, stock);
            products.Add(p);
            FileHelper.Serialize(productPath, products);
        }
        public void DeleteProduct()
        {
            Console.Write("Enter Id:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            Product p = products.Find(p => p.Id == id);
            if (p == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            products.Remove(p);
            FileHelper.Serialize(productPath, products);
        }

        public void GetProductById()
        {
            Console.Write("Enter Id:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            var p = products.Find(p => p.Id == id);
            if (p != null) p.PrintInfo();
            else Console.WriteLine("Product not found");
        }
        void ShowAllProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products available");
                return;
            }

            foreach (var p in products)
                p.PrintInfo();
        }

        public void RefillProduct()
        {
            Console.Write("Enter product Id:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            var p = products.Find(p => p.Id == id);
            if (p == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            Console.Write("Enter value to add:");
            if (!int.TryParse(Console.ReadLine(), out int add) || add <= 0)
            {
                Console.WriteLine("Invalid value");
                return;
            }

            p.Stock += add;
            FileHelper.Serialize(productPath, products);
        }

        public void OrderProduct()
        {
            Console.Write("Enter email:");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Invalid email");
                return;
            }

            List<OrderItem> orderItems = new();

            while (true)
            {
                Console.Write("Enter product Id:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid Id");
                    continue;
                }

                var p = products.Find(p => p.Id == id);
                if (p == null)
                {
                    Console.WriteLine("Product not found");
                    continue;
                }

                Console.Write("Enter count:");
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

                Console.Write("Add another (y/n):");
                if (Console.ReadLine().ToLower() != "y") break;
            }

            if (orderItems.Count > 0)
            {
                Order o = new Order(email, orderItems);
                orders.Add(o);
                FileHelper.Serialize(productPath, products);
                FileHelper.Serialize(orderPath, orders);
            }
        }

        public void ShowAllOrders()
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders available");
                return;
            }

            foreach (var o in orders)
                o.PrintInfo();
        }

        public void ChangeOrderStatus()
        {
            Console.Write("Enter order Id:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            var o = orders.Find(o => o.Id == id);
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
            FileHelper.Serialize(orderPath, orders);
        }
    }
}
