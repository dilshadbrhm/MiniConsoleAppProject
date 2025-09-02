using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Helper
{
    internal class Effects
    {
        public static void MatrixEffect(int rows, int cycles)
        {
            Random rand = new Random();
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            for (int c = 0; c < cycles; c++)
            {
                for (int i = 0; i < rows; i++)
                {
                    Console.SetCursorPosition(rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight));
                    Console.Write(rand.Next(0, 2)); 
                                                    
                }
                Thread.Sleep(100);
            }

            Console.ResetColor(); 
            Console.Clear(); 
        }
        public static void LoadingBar()
        {
            Console.WriteLine("Yüklənir...");
            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int i = 0; i <= 20; i++)
            {
                Console.Write("\r[" + new string('=', i) + new string(' ', 20 - i) + $"] {i * 5}%");
                Console.Beep(500 + i * 20, 50);
                Thread.Sleep(100);
            }

            Console.ResetColor();
            Console.WriteLine("\nYükləmə tamamlandı!\n");
        }
        public static void PrintAsciiArt()
        {
            string[] art = new string[]
            {
               @"▀█████████▄     ▄████████    ▄████████  ▄█    █▄   ▄██████▄  
  ███    ███   ███    ███   ███    ███ ███    ███ ███    ███ 
  ███    ███   ███    ███   ███    ███ ███    ███ ███    ███ 
 ▄███▄▄▄██▀   ▄███▄▄▄▄██▀   ███    ███ ███    ███ ███    ███ 
▀▀███▀▀▀██▄  ▀▀███▀▀▀▀▀   ▀███████████ ███    ███ ███    ███ 
  ███    ██▄ ▀███████████   ███    ███ ███    ███ ███    ███ 
  ███    ███   ███    ███   ███    ███ ███    ███ ███    ███ 
▄█████████▀    ███    ███   ███    █▀   ▀██████▀   ▀██████▀  
               ███    ███                                    
"

            };



            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var line in art)
            {
                Console.WriteLine(line);
                Thread.Sleep(100);
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void GradientText(string text, int startColor, int endColor)
        {
            int length = text.Length;
            for (int i = 0; i < length; i++)
            {
                int colorIndex = startColor + (i * (endColor - startColor)) / length;
                Console.ForegroundColor = (ConsoleColor)(colorIndex % 16);
                Console.Write(text[i]);
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }
        public static void SpinningLoader(int duration = 2000)
        {
            string[] spinner = { "|", "/", "-", "\\" };
            int index = 0;
            DateTime end = DateTime.Now.AddMilliseconds(duration);

            Console.ForegroundColor = ConsoleColor.Blue;
            while (DateTime.Now < end)
            {
                Console.Write("\r" + spinner[index]);
                index = (index + 1) % spinner.Length;
                Thread.Sleep(100);
            }
            Console.ResetColor();
            Console.WriteLine("\r✔ Hazır!");
        }
        public static void TypeWriter(string text, int delay)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }

    }
}
