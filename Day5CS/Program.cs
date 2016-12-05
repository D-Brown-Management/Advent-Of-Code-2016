using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day5CS
{
    class Program
    {
        static void Main(string[] args)
        {
            MD5Cng cn = new MD5Cng();
            string input = "ugkcyxxp";
            List<char> charsFound = new List<char>();
            int counter = 0;
            do
            {
                var newStr = $"{input}{counter}";
                var bytes = cn.ComputeHash(Encoding.ASCII.GetBytes(newStr));
                var output = BitConverter.ToString(bytes).Replace("-",string.Empty);
                if (output.StartsWith("00000"))
                {
                    charsFound.Add(output[6]);
                    Console.WriteLine($"Match found! {output[5]}");
                }

                counter++;
            } while (charsFound.Count < 8);
            Console.WriteLine($"P1 Pass: {charsFound}");

            int p2counter = 0;
            char[] p2Password = new char[8];
            int p2CharsFound = 0;

            do
            {
                var newStr = $"{input}{p2counter}";
                var bytes = cn.ComputeHash(Encoding.ASCII.GetBytes(newStr));
                var output = BitConverter.ToString(bytes).Replace("-", string.Empty);
                if (output.StartsWith("00000"))
                {
                    if (output[5] >= 48 && output[5] <= 55)
                    {
                        int idx = output[5] - 48;
                        if (p2Password[idx] == '\0')
                        {
                            p2Password[idx] = output[6];
                            Console.WriteLine($"Match found! {output[6]}");
                            p2CharsFound++;
                        }

                    }                    
                }

                p2counter++;
            } while (p2CharsFound < 8);

            Console.WriteLine($"PW: {new string(p2Password)}");
        }
    }
}
