using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Big_One
{
    class Map
    {
        public Room[][] data;
        public static int MapHeight = 32;
        public static int MapWidth = 32;
        public static Map GenerateMap()
        {
            Map m =  new Map();

            GenerateData(m);

            return m;
        }

        public static int[][] neighbors = new int[][] { new int[]{ 0, 1 }, 
            new int[]{ 1, 0 }, new int[]{ 0, -1 }, new int[]{ -1, 0 } };


        private static void GenerateData(Map m)
        {
            m.data = new Room[MapWidth][];
            for (int x = 0; x < MapWidth; x++)
            {
                m.data[x] = new Room[MapHeight];
                for (int y = 0; y < MapHeight; y++)
                {
                    m.data[x][y] = null;
                }
            }

            Point rootPoint = new Point(MapWidth / 2, MapHeight / 2);
            Stack<Point> ptStack = new Stack<Point>();
            ptStack.Push(rootPoint);
            byte placementFlags = 0;

            Point nextPt;
            while (placementFlags != 0xff && ptStack.Count > 0)
            {
                m.data[ptStack.Peek().X][ptStack.Peek().Y] = new Room();

                if(((Program.RNG().Next() % 100) > 90) && (placementFlags & 0x80) == 0)
                {
                    m.data[ptStack.Peek().X][ptStack.Peek().Y].MakeElevator();
                    placementFlags |= 0x80;
                }
                if (((Program.RNG().Next() % 100) > 90) && (placementFlags & 0x40) == 0)
                {
                    m.data[ptStack.Peek().X][ptStack.Peek().Y].MakeJanitor();
                    placementFlags |= 0x40;
                }
                placementFlags++;

                int nextDir = findNextDirection(m, ptStack.Peek());
                if (nextDir == -1)
                {
                    ptStack.Pop();
                    continue;
                }
                nextPt = new Point(ptStack.Peek().X + neighbors[nextDir][0],ptStack.Peek().Y + neighbors[nextDir][1]);
                ptStack.Push(nextPt);
            }
        }

        private static int findNextDirection(Map m, Point location)
        {
            int startCheck = Program.RNG().Next() % 4;
            for (int i = 0; i < 4; i++)
            {
                if (startCheck > 3) startCheck = 0;

                int chkX = location.X + neighbors[startCheck][0];
                if(chkX < 0 || chkX > (MapWidth - 1)) continue;
                int chkY = location.Y + neighbors[startCheck][1];
                if(chkY < 0 || chkY > (MapHeight - 1)) continue;

                if (m.data[chkX][chkY] == null)
                {
                    return startCheck;
                }
                startCheck++;
            }
            return -1;
        }

        public void Print()
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (data[x][y] == null)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else if (x == Program.Location.X && y == Program.Location.Y)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (data[x][y].treasure != null)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (data[x][y].trap != null)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write('█');
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = oldColor;
        }
    }

    class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
