using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day8CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var regex = new Regex(@"^(?<op>rect|rotate)(?: (?<dimx>\d+)x(?<dimy>\d+))?(?: (?<rowcol>row|column))?(?: (?<axis>[xy])=(?<coord>\d+) by )?(?<dist>\d+)?");

            var lines = File.ReadAllLines("input.txt");
            //string[] lines = new string[]
            //{
            //    "rect 3x2",
            //    "rotate column x=1 by 1",
            //    "rotate row y=0 by 4"
            //};
            
            bool[][] screenAry = new bool[50][];
            for (int i = 0; i < 50; i++)
            {
                screenAry[i] = new bool[6];
            }
            foreach (var line in lines)
            {
                var match = regex.Match(line);

                if (match.Groups["op"].Value == "rect")
                {
                    // do rectangle
                    int xDim = int.Parse(match.Groups["dimx"].Value);
                    int yDim = int.Parse(match.Groups["dimy"].Value);

                    for (int i = 0; i < xDim; i++)
                    {
                        for (int j = 0; j < yDim; j++)
                        {
                            if (i > 49 || j > 5)
                            {
                                continue; 
                                
                            }
                                
                            screenAry[i][j] = true;
                        }
                    }
                    
                }
                else
                {
                    // do rotate
                    string rowOrCol = match.Groups["rowcol"].Value;
                    string axis = match.Groups["axis"].Value;
                    int coord = int.Parse(match.Groups["coord"].Value);
                    int moveAmt = int.Parse(match.Groups["dist"].Value);

                    screenAry = Move(rowOrCol, axis, coord, moveAmt, screenAry);
                }

                PrintScreenAry(screenAry, 50, 6);
            }

            int count = 0;

            for (int i = 0; i < screenAry.Length; i++)
            {
                for (int j = 0; j < screenAry[i].Length; j++)
                {
                    if (screenAry[i][j])
                    {
                        count++;
                    }
                }
            }


            Console.ReadLine();
        }

        private static bool[][] Move(string rowOrCol, string axis, int coord, int moveAmt, bool[][] screenAry)
        {
            if (moveAmt < 1)
            {
                return screenAry;                
            }          
            
            for (int i = 0; i < moveAmt; i++)
            {               
                if (rowOrCol == "row")
                {
                    var aryInProgress = screenAry.Select(s => s[coord]).ToArray();
                    var temp = aryInProgress[aryInProgress.Length - 1];
                    Array.Copy(aryInProgress, 0, aryInProgress, 1, aryInProgress.Length - 1);
                    aryInProgress[0] = temp;

                    for (int j = 0; j < aryInProgress.Length; j++)
                    {
                        screenAry[j][coord] = aryInProgress[j];
                    }
                }
                else
                {
                    var aryInProgress = screenAry[coord];

                    var temp = aryInProgress[aryInProgress.Length - 1];
                    Array.Copy(aryInProgress, 0, aryInProgress, 1, aryInProgress.Length - 1);
                    aryInProgress[0] = temp;

                    screenAry[coord] = aryInProgress;
                }
                PrintScreenAry(screenAry, 50, 6);
            }

            return screenAry;            
        }

        static void PrintScreenAry(bool[][] ary, int xSize, int ySize)
        {
            Console.Clear();
            DrawBorder(xSize, ySize);
            Console.CursorTop = 1;
            Console.CursorLeft = 1;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Console.CursorLeft = i + 1;
                    Console.CursorTop = j + 1;

                    if (ary[i][j])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        //Console.Write(".");
                    }
                }
            }
        }

        static void DrawBorder(int x, int y)
        {
            StringBuilder midString = new StringBuilder();
            midString.Append("|");
            for (int i = 0; i < x; i++)
            {
                midString.Append(" ");
            }
            midString.Append("|");

            var fullMidstring = midString.ToString();

            for (int i = 0; i < x + 2; i++)
            {
                Console.Write("-");
            }
            Console.Write("\r\n");

            for (int i = 0; i < y; i++)
            {
                Console.WriteLine(fullMidstring);
            }

            for (int i = 0; i < x + 2; i++)
            {
                Console.Write("-");
            }
            Console.Write("\r\n");
                      
        }
    }
}
