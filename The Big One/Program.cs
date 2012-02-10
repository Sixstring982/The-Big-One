using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Big_One
{
    class Program
    {
        private bool gameOver = false;
        private Dictionary<string, string[]> Aliases;

        private static Random rand = new Random(DateTime.Now.Millisecond);
        public static Random RNG()
        {
            return rand;
        }

        private Map currentMap;
        private bool drawingMap;
        public static Point Location;
        private static int Money;
        private static Dictionary<Item.ItemType, int> GadgetInventory = new Dictionary<Item.ItemType, int>();
        public static Dictionary<Item.ItemType, int> GetGadgets()
        {
            return GadgetInventory;
        }
        public static void AddToGadgetInventory(Item.ItemType type)
        {
            GadgetInventory[type]++;
        }
        private static List<Treasure> Inventory = new List<Treasure>();
        public static List<Treasure> GetInventory()
        {
            return Inventory;
        }
        public static void AddToInventory(Treasure t)
        {
            Inventory.Add(t);
        }

        public static  void AddMoney(int amount)
        {
            Money += amount;
        }
        public static int GetMoney()
        {
            return Money;
        }
        public static void ResetMoney()
        {
            Money = 0;
        }

        public Program()
        {
            Treasure.LoadObjectNames();
            GadgetInventory.Add(Item.ItemType.Jack, 0);
            GadgetInventory.Add(Item.ItemType.Deadweight, 0);
            GadgetInventory.Add(Item.ItemType.SprayCan, 0);
            LoadAliases();
            currentMap = Map.GenerateMap();
            Location = new Point(Map.MapWidth / 2, Map.MapHeight / 2);
            ShowSplash();

            while (!gameOver)
            {
                DrawRoom(Location);
                DoInput(ReadInput());
            }
        }

        private void DrawRoom(Point roomID)
        {
            Console.Clear();
            if (drawingMap) currentMap.Print();
            Console.WriteLine(roomID.X + "x [-MUSEUM-] y" + roomID.Y);
            Console.WriteLine("");
            Console.WriteLine("Exits:");
            for (int i = 0; i < 4; i++)
            {
                int dir = (i % 2 == 1 ? i : -i + 2);
                int chx = roomID.X + Map.neighbors[i][0];
                int chy = roomID.Y + Map.neighbors[i][1];
                if (chx < 0 || chx > (Map.MapWidth - 1)) continue;
                if (chy < 0 || chy > (Map.MapHeight - 1)) continue;
                if (currentMap.data[roomID.X + Map.neighbors[i][0]][roomID.Y + Map.neighbors[i][1]] != null)
                {
                    Console.WriteLine(" " + (Direction)dir);
                }
            }
            if (currentMap.data[roomID.X][roomID.Y].treasure != null)
            {
                Console.WriteLine("\nAtop a pedistal in this room sits " + currentMap.data[roomID.X][roomID.Y].treasure + "!");
            }
            if (currentMap.data[roomID.X][roomID.Y].IsElevator)
            {
                Console.WriteLine("A janitor's closet is here.");
            }
            if (currentMap.data[roomID.X][roomID.Y].IsJanitor)
            {
                Console.WriteLine("An elevator to the next floor is here.");
            }
        }

        private void LoadAliases()
        {
            Aliases = new Dictionary<string, string[]>();
            char[] parsers = new char[] {' '};
            Aliases.Add("n", "north".Split(parsers));
            Aliases.Add("e", "east".Split(parsers));
            Aliases.Add("s", "south".Split(parsers));
            Aliases.Add("w", "west".Split(parsers));
            Aliases.Add("quit", "exit".Split(parsers));
        }

        private string[] ReadInput()
        {
            Console.Write("\n>");
            return MatchAliases(Console.ReadLine().Split(" ".ToCharArray()));
        }

        private string[] MatchAliases(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Aliases.ContainsKey(input[0]))
                    return Aliases[input[0]];
            }
            return input;
        }

        private void DoInput(string[] inputString)
        {
            switch(inputString[0].ToLower())
            {
                case "alias":
                    string[] newStrings = new string[inputString.Length - 2];
                    for(int i = 2; i < inputString.Length; i++)
                        newStrings[i-2] = inputString[i];
                    Aliases.Add(inputString[1], newStrings);
                    break;
                case "north":
                    if (Location.Y > 0)
                    {
                        if ((currentMap.data[Location.X][Location.Y - 1] != null))
                            Location.Y--;
                        else
                            Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    }
                    else
                        Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    break;
                case "east":
                    if (Location.X < (Map.MapWidth - 1))
                    {
                        if (currentMap.data[Location.X + 1][Location.Y] != null)
                            Location.X++;
                        else
                            Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    }
                    else
                        Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    break;
                case "south":
                    if (Location.Y < (Map.MapHeight - 1))
                    {
                        if (currentMap.data[Location.X][Location.Y + 1] != null)
                            Location.Y++;
                        else
                            Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    }
                    else
                        Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    break;
                case "west":
                    if (Location.X > 0)
                    {
                        if (currentMap.data[Location.X - 1][Location.Y] != null)
                            Location.X--;
                        else
                            Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    }
                    else
                        Console.WriteLine("YOU CANNOT GO THAT WAY.");
                    break;
                case "exit":
                    Console.WriteLine("GOODBYE");
                    gameOver = true;
                    break;
                case "use":
                    if (inputString[1].ToLower() == "elevator")
                    {
                        if (currentMap.data[Location.X][Location.Y].IsElevator)
                        {
                            UseElevator();
                        }
                    }
                    break;
                case "take":
                    if (inputString[1].ToLower() == "treasure")
                    {
                        if (currentMap.data[Location.X][Location.Y].treasure != null)
                        {
                            AddToInventory(currentMap.data[Location.X][Location.Y].treasure);
                            currentMap.data[Location.X][Location.Y].treasure = null;
                        }
                    }
                    break;
                case "visit":
                    if (currentMap.data[Location.X][Location.Y].IsJanitor)
                    {
                        Janitor.Visit();
                    }
                    break;
                case "drawing":
                    if (inputString.Length > 1)
                    {
                        if (inputString[1] == "on")
                            drawingMap = true;
                        if (inputString[1] == "off")
                            drawingMap = false;
                    }
                    else
                        drawingMap = !drawingMap;
                    break;
            }
        }

        private void UseElevator()
        {
            Location.X = Map.MapWidth / 2;
            Location.Y = Map.MapHeight / 2;
            currentMap = Map.GenerateMap();
        }

        private void ShowSplash()
        {
            Console.WriteLine("The Big One");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("©2012 Trent Small");
            Console.WriteLine("     Zaach Tubb");
            Console.WriteLine("");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Main(string[] args)
        {
            new Program();
        }
    }
}
