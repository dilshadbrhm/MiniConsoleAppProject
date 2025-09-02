using MiniConsoleAppProject.Helper;
using System.Text.Json.Serialization;

namespace MiniConsoleAppProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ManagementApplication application = new ManagementApplication();
            //Effects.PrintAsciiArt();
            //Effects.GradientText("Xoş Gəldiniz!", 2, 14);
            //Effects.LoadingBar();
            //Effects.SpinningLoader();
            //Effects.MatrixEffect(10, 20);

            Console.Title = "Mini Console Demo";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Effects.PrintAsciiArt();
            Effects.GradientText("Welcome to the Console Demo!", 1, 15);
            Effects.LoadingBar();
            Effects.TypeWriter("Hazırsan? O zaman başlayaq!", 50);

            Effects.SpinningLoader(3000); // 3 saniyə dönsün
            Effects.MatrixEffect(20, 90); // 20 sətr, 90 dövr
             application.Run();   
            Console.ResetColor();
            Console.WriteLine("\nDemo bitdi, çıxmaq üçün hər hansı düyməyə bas...");
            Console.ReadKey();
            //application.Run();


        }
    }
}
