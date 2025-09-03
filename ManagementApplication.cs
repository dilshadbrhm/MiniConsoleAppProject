using MiniConsoleAppProject;
using MiniConsoleAppProject.Enums;
using MiniConsoleAppProject.Helper;
using MiniConsoleAppProject.MiniConsoleAppProject;
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
internal class ManagementApplication
{
    public string productPath = @"C:\Users\dilsa\OneDrive\Masaüstü\VSrep\MiniConsoleAppProject\products.json";
    public string orderPath = @"C:\Users\dilsa\OneDrive\Masaüstü\VSrep\MiniConsoleAppProject\orders.json";

    private ProductService productService;
    private OrderService orderService;

    public ManagementApplication()
    {
        productService = new ProductService(productPath);
        orderService = new OrderService(orderPath, productService);
    }

    public void Run()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        int num = -1;
        while (num != 0)
        {
            ConsoleHelper.WriteInfo("1.Create Product\n2.Delete Product\n3.Get Product By Id\n4.Show All Product\n5.Refill Product\n6.Order Product\n7.Show All Orders\n8.Change Order Status\n\n0.Exit");

            if (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.Clear();
                ConsoleHelper.WriteError("Wrong input");
                continue;
            }

            Console.Clear();
            switch (num)
            {
                case 1: productService.CreateProduct(); break;
                case 2: productService.DeleteProduct(); break;
                case 3: productService.GetProductById(); break;
                case 4: productService.ShowAllProducts(); break;
                case 5: productService.RefillProduct(); break;
                case 6: orderService.OrderProduct(); break;
                case 7: orderService.ShowAllOrders(); break;
                case 8: orderService.ChangeOrderStatus(); break;
                case 0:
                    ConsoleHelper.WriteInfo("Program ended");
                    Effects.MatrixEffect(10, 10);
                    return;
                default:
                    ConsoleHelper.WriteError("Wrong input");
                    break;
            }
        }
    }
}
