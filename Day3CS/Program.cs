using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3CS
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1 p1 = new Part1();
            p1.DoPartOne();

            Part2 p2 = new Part2();
            p2.DoPartTwo();
            Console.ReadLine();
        }
    }
}
