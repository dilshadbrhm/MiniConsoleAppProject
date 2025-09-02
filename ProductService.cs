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
        private List<Product> _products;

        public ProductService(string productPath)
        {
            _productPath = productPath;
            _products = FileHelper.Deserialize<Product>(_productPath);
        }
    
     public void CreateProduct()
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
                Console.Write("Enter price: ");
                if (decimal.TryParse(Console.ReadLine(), out price) && price > 0)
                    break;
                Console.WriteLine("Invalid price. Try again.");
            }

            int stock;
            while (true)
            {
                Console.Write("Enter stock: ");
                if (int.TryParse(Console.ReadLine(), out stock) && stock >= 0)
                    break;
                Console.WriteLine("Invalid stock. Try again.");
            }

            Product p = new Product(name, price, stock);
            _products.Add(p);
            Save();
            Console.WriteLine("Product created");
        }

        public void DeleteProduct()
        {
            int id = GetIdFromUser("Enter Id to delete: ");
            var product = _products.Find(p => p.Id == id);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            _products.Remove(product);
            Save();
            Console.WriteLine("Product deleted.");
        }

        public void ShowAllProducts()
        {
            if (_products.Count == 0)
            {
                Console.WriteLine("No products available");
                return;
            }

            foreach (var p in _products)
                p.PrintInfo();
        }

        public void RefillProduct()
        {
            int id = GetIdFromUser("Enter product Id to refill: ");
            var product = _products.Find(p => p.Id == id);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            int add;
            while (true)
            {
                Console.Write("Enter value to add: ");
                if (int.TryParse(Console.ReadLine(), out add) && add > 0)
                    break;

                Console.WriteLine("Invalid value. Try again.");
            }

            product.Stock += add;
            Save();
            Console.WriteLine("Stock updated");
        }

        public Product GetProductById()
        {
            int id = GetIdFromUser("Enter product Id: ");
            var product = _products.Find(p => p.Id == id);

            if (product == null)
                Console.WriteLine("Product not found.");
            else
                product.PrintInfo();

            return product;
        }

        private int GetIdFromUser(string message)
        {
            int id;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                    return id;

                Console.WriteLine("Invalid Id. Try again.");
            }
        }

        private void Save()
        {
            FileHelper.Serialize(_productPath, _products);
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public void SaveProducts()
        {
            Save();
        }
    }
}
