using MiniConsoleAppProject.Helper;
using MiniConsoleAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject
{
    internal class ProductService
    {
        private readonly string _productPath;
        private List<Product> _products = new();

        public ProductService(string productPath)
        {
            _productPath = productPath;
            _products = FileHelper.Deserialize<Product>(_productPath);
            if (_products == null)
            {
                _products = new List<Product>();
            }
        }

        public void CreateProduct()
        {
            do
            {
                string name;
                do
                {
                    Console.Write("Enter product name: ");
                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Name cannot be empty. Please try again.");
                        continue;
                    }
                    
                    if (_products.Exists(p => p.Name.Equals(name)))
                    {
                        Console.WriteLine("This product already exists");
                        name = null;
                    }

                } while (string.IsNullOrWhiteSpace(name));


                decimal price;
                while (true)
                {
                    Console.Write("Enter price (0 - back): ");
                    string input = Console.ReadLine();
                    if (input == "0") return;

                    if (decimal.TryParse(input, out price) && price > 0)
                        break;

                    ConsoleHelper.WriteError("Invalid price, try again.");
                }


                int stock;
                while (true)

                    {
                        Console.Write("Enter stock (0 - back): ");
                        string input = Console.ReadLine();
                        if (input == "0") return;

                        if (int.TryParse(input, out stock) && stock >= 0)
                            break;

                        ConsoleHelper.WriteError("Invalid stock, try again.");
                    }

                Product p = new Product(name, price, stock);
                _products.Add(p);
                SaveProducts();
                ConsoleHelper.WriteInfo("Product created successfully!");

                Console.Write("Do you want to add another product? (y/n): ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        public void DeleteProduct()
        {
            do
            {
                Console.Write("Enter product Id to delete (0 - back): ");
                string input = Console.ReadLine();
                if (input == "0") return;

                if (!int.TryParse(input, out int id))
                {
                    ConsoleHelper.WriteError("Invalid Id.");
                    continue;
                }

                var p = _products.Find(x => x.Id == id);
                if (p == null)
                {
                    ConsoleHelper.WriteError("Product not found.");
                    continue;
                }

                _products.Remove(p);
                SaveProducts();
                ConsoleHelper.WriteInfo("Product deleted successfully!");

                Console.Write("Do you want to delete another product? (y/n): ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        public void GetProductById()
        {
            do
            {
                Console.Write("Enter product Id (0 - back): ");
                string input = Console.ReadLine();
                if (input == "0") return;

                if (!int.TryParse(input, out int id))
                {
                    ConsoleHelper.WriteError("Invalid Id.");
                    continue;
                }

                var p = _products.Find(x => x.Id == id);
                if (p == null)
                {
                    ConsoleHelper.WriteError("Product not found.");
                    continue;
                }

                p.PrintInfo();
                Console.WriteLine();

                Console.Write("Do you want to search another product? (y/n): ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        public void ShowAllProducts()
        {
            if (_products.Count == 0)
            {
                ConsoleHelper.WriteError("No products available.");
                return;
            }

            ConsoleHelper.WriteInfo("All Products:");
            foreach (var p in _products)
            {
                p.PrintInfo();
                Console.WriteLine("-----------------------------");
            }
        }

        public void RefillProduct()
        {
            do
            {
                Console.Write("Enter product Id (0 - back): ");
                string input = Console.ReadLine();
                if (input == "0") return;

                if (!int.TryParse(input, out int id))
                {
                    ConsoleHelper.WriteError("Invalid Id.");
                    continue;
                }

                var p = _products.Find(x => x.Id == id);
                if (p == null)
                {
                    ConsoleHelper.WriteError("Product not found.");
                    continue;
                }

                int amount;
                while (true)
                {
                    Console.Write("Enter refill amount (0 - back): ");
                    string inp = Console.ReadLine();
                    if (inp == "0") return;

                    if (int.TryParse(inp, out amount) && amount > 0)
                        break;

                    ConsoleHelper.WriteError("Invalid amount.");
                }

                p.Stock += amount;
                SaveProducts();
                ConsoleHelper.WriteInfo("Stock updated successfully!");

                Console.Write("Do you want to refill another product? (y/n): ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        public List<Product> GetProducts() => _products;

        public void SaveProducts()
        {
            FileHelper.Serialize(_productPath, _products);
        }
    }
}