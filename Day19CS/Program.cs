using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19CS
{
    class Program
    {
        public static Dictionary<int, Elf> elves = new Dictionary<int, Elf>();
        static void Main(string[] args)
        {
            

            int elfCount = 63;

            for (int i = 0; i < elfCount; i++)
            {
                elves.Add(i, new Elf() {Id = i, NumPresents = 1});
            }

            int counter = 0;

            while (elves.Count(e => e.Value.NumPresents != 0) > 1)
            {
                int elfNumber = counter%elfCount;
                if (elves[elfNumber].NumPresents == 0)
                {
                    counter++;
                    continue;
                }
                var currentElf = elves[elfNumber];
                var next = NextElfInLine(currentElf);
                currentElf.StealPresents(next);
                //elves.Remove(next.Id);
                counter++;
            }

            var elf = elves.Single(e => e.Value.NumPresents > 0);
            Console.WriteLine("The elf is: {0}", elf.Key+1);
        }

        static Elf NextElfInLine(Elf elf)
        {
            var outElves = elves.Where(e => e.Value.NumPresents > 0 && e.Key > elf.Id).OrderBy(e => e.Key);

            
            if (outElves.Any())
            {

                return outElves.First().Value;
            }
            else
            {
                outElves = elves.Where(e => e.Value.NumPresents > 0 && e.Key < elf.Id).OrderBy(e => e.Key);
            }

            if (outElves.Any())
            {
                return outElves.First().Value;
            }

            return null;
        }
    }

    public class Elf
    {
        public int Id { get; set; }
        public int NumPresents { get; set; }

        public void StealPresents(Elf elf)
        {
            this.NumPresents += elf.NumPresents;
            elf.NumPresents = 0;
        }
    }
}
