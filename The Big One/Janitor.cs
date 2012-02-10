using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace The_Big_One
{
    class Janitor
    {
        public static void Visit()
        {
            bool inJanitor = true;

            while (inJanitor)
            {
                Console.Clear();
                PrintMainMenu();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Buy();
                        break;
                    case ConsoleKey.D2:
                        Sell();
                        break;
                    case ConsoleKey.D3:
                        inJanitor = false;
                        break;
                }
            }
        }

        private static void Sell()
        {
            Console.Clear();
            Console.Write("WHAT ARE YOU SELLIN'");
            Thread.Sleep(150);
            Console.Write('?');
            Thread.Sleep(150);
            Console.Write('?');
            Thread.Sleep(150);
            Console.Write('?');
            Thread.Sleep(150);

            bool inSellScreen = true;

            while (inSellScreen)
            {
                Console.Clear();
                Console.WriteLine("WHAT ARE YOU SELLIN'???");
                Console.WriteLine("MONEY: " + Program.GetMoney());
                for (int i = 0; i < Program.GetInventory().Count; i++)
                {
                    Console.WriteLine("[" + i + "] " + Program.GetInventory()[i]);
                }
                Console.WriteLine("[Q]uit");
                ConsoleKey key = Console.ReadKey().Key;
                if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
                {
                    int invID = (int)(key - ConsoleKey.D0);
                    if (Program.GetInventory().Count > invID)
                    {
                        Treasure t = Program.GetInventory()[invID];
                        Program.AddMoney(t.value);
                        Program.GetInventory().Remove(t);
                    }
                }
                if (key == ConsoleKey.Q)
                    inSellScreen = false;
            }
        }

        private static void Buy()
        {
            Console.Clear();
            Console.Write("WHAT ARE YOU BUYIN'");
            Thread.Sleep(150);
            Console.Write('?');
            Thread.Sleep(150);
            Console.Write('?');
            Thread.Sleep(150);
            Console.Write('?');
            Thread.Sleep(150);

            bool inBuyScreen = true;
            while (inBuyScreen)
            {
                Console.Clear();
                Console.WriteLine("WHAT ARE YOU BUYIN'???");
                Console.WriteLine("MONEY: " + Program.GetMoney());
                Console.WriteLine("[1]: Deadweight - $100");
                Console.WriteLine("[2]: Spray Can  - $500");
                Console.WriteLine("[3]: Jack       - $1000");
                Console.WriteLine("[Q]uit");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        if(Program.GetMoney() >= 100)
                        {
                            Program.AddToGadgetInventory(Item.ItemType.Deadweight);
                            Program.AddMoney(-100);
                        }
                        break;
                    case ConsoleKey.D2:
                        if (Program.GetMoney() >= 500)
                        {
                            Program.AddToGadgetInventory(Item.ItemType.SprayCan);
                            Program.AddMoney(-500);
                        }
                        break;
                    case ConsoleKey.D3:
                        if (Program.GetMoney() >= 1000)
                        {
                            Program.AddToGadgetInventory(Item.ItemType.Jack);
                            Program.AddMoney(-1000);
                        }
                        break;
                    case ConsoleKey.Q:
                        inBuyScreen = false;
                        break;
                }
            }
        }

        private static void PrintMainMenu()
        {
            Console.Write("HELLO STRANGER");
            Thread.Sleep(50);
            Console.Write('.');
            Thread.Sleep(150);
            Console.Write('.');
            Thread.Sleep(150);
            Console.Write('.');
            Thread.Sleep(150);
            Console.WriteLine("");
            Console.WriteLine("--------------");
            Console.WriteLine("");
            Console.WriteLine("[1] BUY");
            Console.WriteLine("[2] SELL");
            Console.WriteLine("[3] EXIT");
            Console.Write('>');
        }
    }
}
