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
        public static Point Location;

        public Program()
        {
            Treasure.LoadObjectNames();
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
            currentMap.Print();
            Console.WriteLine(roomID.X + "x [-MUSEUM-] y" + roomID.Y);
            Console.WriteLine("");
            Console.WriteLine("Exits:");
            for (int i = 0; i < 4; i++)
            {
                int dir = (i % 2 == 1 ? i : -i + 2);
                if (currentMap.data[roomID.X + Map.neighbors[i][0]][roomID.Y + Map.neighbors[i][1]] != null)
                {
                    Console.WriteLine(" " + (Direction)dir);
                }
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
            }
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
