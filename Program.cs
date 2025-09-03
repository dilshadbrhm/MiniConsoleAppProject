using MiniConsoleAppProject.Helper;
using System.Text.Json.Serialization;

namespace MiniConsoleAppProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Effects.PrintAsciiArt();
            ManagementApplication application = new ManagementApplication();
            //Effects.PrintAsciiArt();
            //Effects.GradientText("Xoş Gəldiniz!", 2, 14);
            //Effects.LoadingBar();
            //Effects.SpinningLoader();
            //Effects.MatrixEffect(10, 20);

            //Console.Title = "Mini Console Demo";
            //Console.OutputEncoding = System.Text.Encoding.UTF8;

            Effects.GradientText("Welcome to the Console Demo!", 1, 15);
            Effects.LoadingBar();
            Effects.TypeWriter("Hazırsan? O zaman başlayaq!", 50);

            Effects.SpinningLoader(3000);
            Effects.MatrixEffect(10, 50);
            application.Run();
          
         


        }
    }
}
