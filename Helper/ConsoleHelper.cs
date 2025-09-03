using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Helper
{
    internal static class ConsoleHelper
    {
        public static void WriteError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public static void WriteInfo(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(msg);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public static bool BackToMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nBack to main menu? (y/n): ");
            Console.ResetColor();

            string input = Console.ReadLine().ToLower();
            Console.Clear();
            return input == "y";
        }
    }
}
