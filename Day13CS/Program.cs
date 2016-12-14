using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13CS
{
    class Program
    {
        public static bool[][] wallAry;
        public static List<CubeNode> visited;
        public static Queue<CubeNode> nodesToVisit;
        public static int targetX = 31;
        public static int targetY = 39;
        static void Main(string[] args)
        {
            int maxX = 55;
            int maxY = 60;
            
            wallAry = new bool[maxX][];
            int[][] wallAryInts = new int[maxX][];
            for (int i = 0; i < maxX; i++)
            {
                wallAry[i] = new bool[maxY];
                wallAryInts[i] = new int[maxY];
                for (int j = 0; j < maxY; j++)
                {
                    wallAry[i][j] = GenerateSpace(i, j).boolRep;
                    wallAryInts[i][j] = GenerateSpace(i, j).intRep;

                }
            }

            visited = new List<CubeNode>();
            nodesToVisit = new Queue<CubeNode>();
            PrintScreen(wallAry, maxX, maxY, targetX, targetY);
            CubeNode cn = new CubeNode()
            {
                X = 1,
                Y = 1
            };
            visited.Add(cn);
            BuildAdjacencies(cn);

            WalkNodes();
            //WalkNodesMax(50);
            //bool[] row = GenerateRow(5, 0);
            //wallAry[0] = GenerateSpace(33, 0);

            var nodesV = visited.Count(n => n.Depth <= 50);
            Console.WriteLine("Max nodes @ {0}: {1}", 50, nodesV);
            Console.ReadLine();
        }

        static void WalkNodes()
        {
            while(nodesToVisit.Count > 0)
            { 
                var node = nodesToVisit.Dequeue();
                Console.WriteLine("Visiting Node: {0},{1}", node.X, node.Y);
                if (node.X == targetX && node.Y == targetY)
                {
                    Console.WriteLine("Found Exit Node.  Depth: {0}", node.Depth);
                    return;
                }

                BuildAdjacencies(node);
            }

        }

        //static void WalkNodesMax(int maxDepth)
        //{
        //    while (nodesToVisit.Count > 0)
        //    {
        //        var node = nodesToVisit.Dequeue();
        //        if (node.Depth >= maxDepth)
        //        {
        //            Console.WriteLine("Hit Max Depth. {0} unique nodes", visited.Count);
        //            return;
        //        }
        //        Console.WriteLine("Visiting Node: {0},{1}", node.X, node.Y);
        //        if (node.X == targetX && node.Y == targetY)
        //        {
        //            Console.WriteLine("Found Exit Node.  Depth: {0}", node.Depth);
        //            return;
        //        }

        //        BuildAdjacencies(node);
        //    }

        //}

        static void BuildAdjacencies(CubeNode node)
        {
            
            // North
            if (node.Y > 0 && !wallAry[node.X][node.Y - 1])
            {
                var newNode = new CubeNode() {X = node.X, Y = node.Y - 1};
                if (!visited.Any(n=>n.X == newNode.X && n.Y == newNode.Y) && !(newNode.X == node.X && newNode.Y == node.Y))
                {
                    node.AddChild(newNode);
                    visited.Add(newNode);
                    nodesToVisit.Enqueue(newNode);
                }                
            }

            // South
            if (!wallAry[node.X][node.Y + 1])
            {
                var newNode = new CubeNode() { X = node.X, Y = node.Y + 1 };
                if (!visited.Any(n => n.X == newNode.X && n.Y == newNode.Y) && !(newNode.X == node.X && newNode.Y == node.Y))
                {
                    node.AddChild(newNode);
                    visited.Add(newNode);
                    nodesToVisit.Enqueue(newNode);
                }
            }

            // East
            if (!wallAry[node.X + 1][node.Y])
            {
                var newNode = new CubeNode() { X = node.X + 1, Y = node.Y };
                if (!visited.Any(n => n.X == newNode.X && n.Y == newNode.Y) && !(newNode.X == node.X && newNode.Y == node.Y))
                {
                    node.AddChild(newNode);
                    visited.Add(newNode);
                    nodesToVisit.Enqueue(newNode);
                }
            }

            // West
            if (node.X > 0 && !wallAry[node.X - 1][node.Y])
            {
                var newNode = new CubeNode() { X = node.X - 1, Y = node.Y };
                if (!visited.Any(n => n.X == newNode.X && n.Y == newNode.Y) && !(newNode.X == node.X && newNode.Y == node.Y))
                {
                    node.AddChild(newNode);
                    visited.Add(newNode);
                    nodesToVisit.Enqueue(newNode);
                }
            }
            
        }

        static ReturnThing GenerateSpace(int x, int y)
        {            
            int output = (((x*x) + 3*x + 2*x*y + y + y*y) + 1350);
            string binary = Convert.ToString(output, 2);

            int count = 0;
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                {
                    count++;
                }                
            }

            return new ReturnThing() { boolRep = count % 2 == 1, intRep = output};
        }        

        static void PrintScreen(bool[][] wallAry, int maxX, int maxY, int targetX, int targetY)
        {
            //byte[] b = BitConverter.GetBytes(row);

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    Console.CursorLeft = i;
                    Console.CursorTop = j;
                    if (!wallAry[i][j] && i == targetX && j == targetY)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(".");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (wallAry[i][j])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
            }
        }

        struct ReturnThing
        {
            public int intRep;
            public bool boolRep;
        }
    }
}
