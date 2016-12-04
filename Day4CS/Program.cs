using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day4CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            int counter = 0;
            int sum = 0;
            var validStrings = new List<string>();
            foreach (string line in lines)
            {

                PartOne p1 = new PartOne();

                if (p1.ValidateRoom(line))
                {
                    validStrings.Add(line);
                    int sectorNumber = p1.GetSector(line);
                    sum += sectorNumber;
                    counter++;
                    var decoded = p1.DecodeSector(line);
                    Console.WriteLine($"Room: {decoded} Sector: {sectorNumber}");
                }
            }

            

            Console.WriteLine($"Counter: {counter}");
            Console.WriteLine($"Sum: {sum}");
            Console.ReadLine();
        }
    }
}
