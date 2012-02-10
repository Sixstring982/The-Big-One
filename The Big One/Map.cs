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
            byte hallsPlaced = 0;
            byte placementFlag = 0x00;

            Point nextPt = new Point(0, 0);
            while (hallsPlaced < 100 && ptStack.Count > 0)
            {
                m.data[ptStack.Peek().X][ptStack.Peek().Y] = new Room();
                int nextDir = findNextDirection(m, ptStack.Peek());
                if (nextDir == -1)
                {
                    ptStack.Pop();
                    continue;
                }
                nextPt.X = ptStack.Peek().X;
                nextPt.Y = ptStack.Peek().Y;
                for (int i = 0; i < 3; i++)
                {
                    nextPt.X += neighbors[nextDir][0];
                    if (nextPt.X < 0 || nextPt.X > (Map.MapWidth - 1))
                    {
                        nextPt.X -= neighbors[nextDir][0];
                        break;
                    }
                    nextPt.Y += neighbors[nextDir][1];
                    if (nextPt.Y < 0 || nextPt.Y > (Map.MapHeight - 1))
                    {
                        nextPt.Y -= neighbors[nextDir][1];
                        break;
                    }
                    m.data[nextPt.X][nextPt.Y] = new Room();
                    hallsPlaced++;
                }
                ptStack.Push(nextPt);
            }
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (m.data[x][y] != null)
                    {
                        if ((Program.RNG().Next() % 10) > 1)
                        {
                            int nextDir = findNextDirection(m, new Point(x, y));
                            if (nextDir == -1) continue;
                            Point roomPt = new Point(neighbors[nextDir][0] + x, neighbors[nextDir][1] + y);
                            if(EligibleTreasureRoom(m, roomPt))
                            {
                                m.data[roomPt.X][roomPt.Y] = new Room();
                                m.data[roomPt.X][roomPt.Y].treasure = new Treasure();
                                m.data[roomPt.X][roomPt.Y].trap = new Trap();

                                if (Program.RNG().Next() % 10 > 8 && (placementFlag & 1) == 0)
                                {
                                    placementFlag |= 1;
                                    m.data[roomPt.X][roomPt.Y].MakeJanitor();
                                }
                                if (Program.RNG().Next() % 10 > 8 && (placementFlag & 2) == 0)
                                {
                                    placementFlag |= 2;
                                    m.data[roomPt.X][roomPt.Y].MakeElevator();
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int[] mooreCoords = new int[] { -1, 0, 1 };
        private static bool EligibleTreasureRoom(Map m, Point roomPoint)
        {
            if (CountLiveNeighbors(m, roomPoint) == 2)
                return true;
            return false;
        }

        private static int CountLiveNeighbors(Map m, Point rmPt)
        {
            int count = 0;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (rmPt.X + mooreCoords[x] < 0 || rmPt.X + mooreCoords[x] > (Map.MapWidth - 1)) continue;
                    if (rmPt.Y + mooreCoords[y] < 0 || rmPt.Y + mooreCoords[y] > (MapHeight - 1)) continue;
                    if (m.data[rmPt.X + mooreCoords[x]][rmPt.Y + mooreCoords[y]] != null)
                        count++;
                }
            }
            return count;
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
                    else if (data[x][y].IsElevator)
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    else if (data[x][y].IsJanitor)
                        Console.ForegroundColor = ConsoleColor.Cyan;
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
