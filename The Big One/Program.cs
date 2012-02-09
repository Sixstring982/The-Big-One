using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Big_One
{
    class Program
    {
        static bool gameOver = false;
        static void Main(string[] args)
        {
            ShowSplash();

            while (!gameOver)
            {
                DoInput(ReadInput());
                break;
            }
            Console.ReadKey();
        }

        static string[] ReadInput()
        {
            return Console.ReadLine().Split(" ".ToCharArray());
        }

        static void DoInput(string[] inputString)
        {
            switch(inputString[0].ToLower())
            {
                case "n":
                    break;
            }
        }

        static void ShowSplash()
        {
            Console.WriteLine("The Big One");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("©2012 Trent Small");
            Console.WriteLine("     Zaach Tubb");
            Console.WriteLine("");
        }
    }
}
