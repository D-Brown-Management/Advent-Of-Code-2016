using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day20CS
{
    class Program
    {
        public static Regex lineRec = new Regex(@"(?<low>\d+)-(?<high>\d+)");
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            //var input = new string[] { "5-8", "0-2", "4-7" };
            uint maxNumber = uint.MaxValue;
            List<Entry> entries = new List<Entry>();

            foreach (var line in input)
            {
                var match = lineRec.Match(line);
                uint lowMatch = uint.Parse(match.Groups["low"].Value);
                uint highMatch = uint.Parse(match.Groups["high"].Value);

                entries.Add(new Entry() { High = highMatch, Low = lowMatch});
            }

            var lowOrdered = entries.OrderBy(e => e.Low).ToList();
            Entry combined = null;
            int currentIndex = 0;
            int newMax = lowOrdered.Count;
            do
            {
                combined = CombineEntries(lowOrdered[currentIndex], lowOrdered[currentIndex+1]);
                if (combined != null)
                {
                    lowOrdered[currentIndex] = combined;
                    lowOrdered.RemoveAt(currentIndex + 1);
                }
                else
                {
                    currentIndex += 1;
                }
                newMax = lowOrdered.Count;
            } while (currentIndex < newMax-1);

            // test last two entries
            var first = lowOrdered[lowOrdered.Count - 2];
            var second = lowOrdered[lowOrdered.Count - 1];
            combined = CombineEntries(first, second);
            if (combined != null)
            {
                lowOrdered[lowOrdered.Count - 2] = combined;
                lowOrdered.RemoveAt(lowOrdered.Count-1);
            }


            Console.WriteLine("Lowest IP: {0}", lowOrdered.First().High+1);
            Console.WriteLine("Total IPs Available: {0}", CalcIps(lowOrdered, 0, maxNumber));
        }

        public static uint CalcIps(List<Entry> entries, uint min, uint max)
        {
            uint blackTotal = 0;
            foreach (Entry entry in entries)
            {
                blackTotal = blackTotal + (entry.High - entry.Low + 1);
            }


            return (max-min)-blackTotal+1;
        }

        public static Entry CombineEntries(Entry first, Entry second)
        {
            if (first.High + 1 == second.Low)
            {
                return new Entry() {Low = first.Low, High = second.High > first.High ? second.High : first.High};
            }
            else if (second.Low > first.Low && second.Low < first.High)
            {
                return new Entry() { Low = first.Low, High = second.High > first.High ? second.High : first.High };
            } else if (first.Low == second.Low && first.High == second.High)
            {
                return first;
            }
            return null;
        }
    }

    public class Entry
    {
        public uint Low { get; set; }
        public uint High { get; set; }
    }
}
