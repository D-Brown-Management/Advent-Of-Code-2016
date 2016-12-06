using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6CS
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int>[] ary = new Dictionary<char, int>[8];
            ary[0] = new Dictionary<char, int>();
            ary[1] = new Dictionary<char, int>();
            ary[2] = new Dictionary<char, int>();
            ary[3] = new Dictionary<char, int>();
            ary[4] = new Dictionary<char, int>();
            ary[5] = new Dictionary<char, int>();
            ary[6] = new Dictionary<char, int>();
            ary[7] = new Dictionary<char, int>();

            var lines = File.ReadAllLines("input.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                var lineAry = lines[i].ToCharArray();
                for (int j = 0; j < 8; j++)
                {
                    if (ary[j].ContainsKey(lineAry[j]))
                    {
                        ary[j][lineAry[j]] += 1;
                    }
                    else
                    {
                        ary[j].Add(lineAry[j],1);
                    }
                }
            }

            var o1 = ary[0].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);
            var o2 = ary[1].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);
            var o3 = ary[2].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);
            var o4 = ary[3].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);
            var o5 = ary[4].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);
            var o6 = ary[5].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);
            var o7 = ary[6].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);
            var o8 = ary[7].OrderBy(d => d.Value).ToDictionary(t=>t.Key,t=>t.Value);

            Console.Write($"{o1.Take(1).Single().Key}");
            Console.Write($"{o2.Take(1).Single().Key}");
            Console.Write($"{o3.Take(1).Single().Key}");
            Console.Write($"{o4.Take(1).Single().Key}");
            Console.Write($"{o5.Take(1).Single().Key}");
            Console.Write($"{o6.Take(1).Single().Key}");
            Console.Write($"{o7.Take(1).Single().Key}");
            Console.Write($"{o8.Take(1).Single().Key}");
        }
    }
}
