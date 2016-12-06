using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            IPoint point = new PointTwo();
            point.X = 0;
            point.Y = 2;

            //IPoint point = new Point();
            //point.X = 1;
            //point.Y = 1;
            foreach (string line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    switch (line[i])
                    {
                        case 'L':
                            point.MoveLeft();
                            break;
                        case 'R':
                            point.MoveRight();
                            break;
                        case 'U':
                            point.MoveUp();
                            break;
                        case 'D':
                            point.MoveDown();
                            break;

                    }
                }
                point.PrintPosition();
                //Console.WriteLine($" {point.Y}");
            }

        }


        
        public class Point : IPoint
        {
            public int X { get; set; }
            public int Y { get; set; }

            public void MoveLeft()
            {
                if (this.X > 0)
                {
                    this.X--;
                }
            }

            public void MoveRight()
            {
                if (this.X < 2)
                {
                    this.X++;
                }
            }

            public void MoveUp()
            {
                if (this.Y < 2)
                {
                    this.Y++;
                }
            }

            public void MoveDown()
            {
                if (this.Y > 0)
                {
                    this.Y--;
                }
            }

            public void PrintPosition()
            {
                int pointRep = 0;
                if (this.X == 0 && this.Y == 0)
                {
                    pointRep = 7;
                } else if (this.X == 1 && this.Y == 0)
                {
                    pointRep = 8;
                }
                else if (this.X == 2 && this.Y == 0)
                {
                    pointRep = 9;
                }
                else if (this.X == 0 && this.Y == 1)
                {
                    pointRep = 4;
                }
                else if (this.X == 1 && this.Y == 1)
                {
                    pointRep = 5;
                }
                else if (this.X == 2 && this.Y == 1)
                {
                    pointRep = 6;
                }
                else if (this.X == 0 && this.Y == 2)
                {
                    pointRep = 1;
                }
                else if (this.X == 1 && this.Y == 2)
                {
                    pointRep = 2;
                }
                else if (this.X == 2 && this.Y == 2)
                {
                    pointRep = 3;
                }

                Console.WriteLine($"Point: {pointRep}");
            }
        }

        public class PointTwo : IPoint
        {
            public int X { get; set; }
            public int Y { get; set; }
            public void MoveUp()
            {
                if (this.X == 0 || this.X == 4)
                {
                    return;
                }
                else if (this.X == 1 || this.X == 3)
                {
                    if (this.Y < 3)
                    {
                        this.Y++;
                    }
                }
                else if (this.X == 2)
                {
                    if (this.Y < 5)
                    {
                        this.Y++;
                    }
                }
            }

            public void MoveDown()
            {
                if (this.X == 0 || this.X == 4)
                {
                    return;
                }
                else if (this.X == 1 || this.X == 3)
                {
                    if (this.Y > 1)
                    {
                        this.Y--;
                    }
                }
                else if (this.X == 2)
                {
                    if (this.Y > 0)
                    {
                        this.Y--;
                    }
                }
            }

            public void MoveLeft()
            {
                if (this.Y == 0 || this.Y == 4)
                {
                    return;
                }
                else if (this.Y == 1 || this.Y == 3)
                {
                    if (this.X > 1)
                    {
                        this.X--;
                    }
                }
                else if (this.Y == 2)
                {
                    if (this.X > 0)
                    {
                        this.X--;
                    }
                }
            }

            public void MoveRight()
            {
                if (this.Y == 0 || this.Y == 4)
                {
                    return;
                }
                else if (this.Y == 1 || this.Y == 3)
                {
                    if (this.X < 3)
                    {
                        this.X++;
                    }
                }
                else if (this.Y == 2)
                {
                    if (this.X < 5)
                    {
                        this.X++;
                    }
                }
            }

            public void PrintPosition()
            {
                string pointRep = string.Empty;
                if (this.X == 0 && this.Y == 2)
                {
                    pointRep = "5";
                }
                else if (this.X == 1 && this.Y == 1)
                {
                    pointRep = "A";
                }
                else if (this.X == 1 && this.Y == 2)
                {
                    pointRep = "6";
                }
                else if (this.X == 1 && this.Y == 3)
                {
                    pointRep = "2";
                }
                else if (this.X == 2 && this.Y == 0)
                {
                    pointRep = "D";
                }
                else if (this.X == 2 && this.Y == 1)
                {
                    pointRep = "B";
                }
                else if (this.X == 2 && this.Y == 2)
                {
                    pointRep = "7";
                }
                else if (this.X == 2 && this.Y == 3)
                {
                    pointRep = "3";
                }
                else if (this.X == 2 && this.Y == 4)
                {
                    pointRep = "1";
                }
                else if (this.X == 3 && this.Y == 1)
                {
                    pointRep = "C";
                }
                else if (this.X == 3 && this.Y == 2)
                {
                    pointRep = "8";
                }
                else if (this.X == 3 && this.Y == 3)
                {
                    pointRep = "4";
                }
                else if (this.X == 4 && this.Y == 2)
                {
                    pointRep = "9";
                }

                Console.WriteLine($"Point: {pointRep}");
            }
        }
    }

    internal interface IPoint
    {
        int X { get; set; }
        int Y { get; set; }
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void PrintPosition();
    }
}

