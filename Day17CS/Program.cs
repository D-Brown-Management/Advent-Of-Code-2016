using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day17CS
{
    class Program
    {
        static MD5Cng cn = new MD5Cng();
        static Queue<VaultNode> nodesToVisit = new Queue<VaultNode>();
        public static Stack<string> solutions = new Stack<string>();
        private static int targetX = 3;
        private static int targetY = 0;
        
        static void Main(string[] args)
        {
            var input = "pgflpeqp";

            VaultNode vn = new VaultNode();
            vn.X = 0;
            vn.Y = 3;

            BuildAdjacencies(vn, input);
            WalkNodes(input);
            //BuildAdjacencies(nodesToVisit.Dequeue(), input);
            Console.WriteLine("Last Solution {0}.  Length {1}", solutions.Peek(), solutions.Peek().Length);
        }

        static void WalkNodes(string password)
        {
            while (nodesToVisit.Count > 0)
            {
                var node = nodesToVisit.Dequeue();
                Console.WriteLine("Visiting Node: {0},{1}", node.X, node.Y);
                if (node.X == targetX && node.Y == targetY)
                {
                    Console.WriteLine("Found Exit Node.  Depth: {0}. String: {1}", node.Depth, node.DirectionString);
                    solutions.Push(node.DirectionString);
                    continue;
                }

                BuildAdjacencies(node, password);
            }

        }

        static void BuildAdjacencies(VaultNode vn, string originalPassword)
        {
            
            string adjacencyTest = GetMD5(string.Concat(originalPassword, vn.DirectionString)).Substring(0,4);
            // Up 0
            // Down 1
            // Left 2
            // Right 3

            // UP TEST
            if(adjacencyTest[0] >= 98 && adjacencyTest[0] <= 102 && vn.Y < 3)
            {
                // up available
                // swap D/U for visible reflection (start node at 0,0 but will render right)
                var child = new VaultNode() { X=vn.X, Y = vn.Y+1, LastDirection = "U"};
                vn.AddChild(child);
                nodesToVisit.Enqueue(child);
            }

            // DOWN TEST
            if (adjacencyTest[1] >= 98 && adjacencyTest[1] <= 102 && vn.Y > 0)
            {
                // DOWN available
                // swap D/U for visible reflection (start node at 0,0 but will render right)
                var child = new VaultNode() { X = vn.X, Y = vn.Y-1, LastDirection = "D"};
                vn.AddChild(child);
                nodesToVisit.Enqueue(child);
            }

            // LEFT TEST
            if (adjacencyTest[2] >= 98 && adjacencyTest[2] <= 102 && vn.X > 0)
            {
                // LEFT available
                var child = new VaultNode() { X = vn.X-1, Y = vn.Y, LastDirection = "L"};
                vn.AddChild(child);
                nodesToVisit.Enqueue(child);
            }

            // RIGHT TEST
            if (adjacencyTest[3] >= 98 && adjacencyTest[3] <= 102 && vn.X < 3)
            {
                // RIGHT available
                var child = new VaultNode() { X = vn.X+1, Y = vn.Y, LastDirection = "R"};
                vn.AddChild(child);
                nodesToVisit.Enqueue(child);
            }

        }

        static string GetMD5(string input)
        {
            string workingString = input;
            
            
            var bytes = cn.ComputeHash(Encoding.ASCII.GetBytes(workingString));
            var output = BitConverter.ToString(bytes).Replace("-", string.Empty);
            workingString = output.ToLower();
           
            return workingString;
        }
    }
}
