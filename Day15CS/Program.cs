using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day15CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            
            var regex = new Regex(@"Disc #(?<dnum>\d) has (?<numPos>\d+) positions; at time=0, it is at position (?<initPos>\d+)");
            List<Disc> discs = new List<Disc>();
            foreach (var line in input)
            {
                var match = regex.Match(line);

                var dnum = int.Parse(match.Groups["dnum"].Value);
                var numPos = int.Parse(match.Groups["numPos"].Value);
                var initPos = int.Parse(match.Groups["initPos"].Value);
                discs.Add(new Disc(dnum, numPos, initPos));
            }

            bool gotOne = false;
            int counter = 0;
            while(!gotOne)
            {
                var outputs = discs.Select(d => new { id=d.GetId(), pos = d.GetPosition(counter)});

                var distincts = outputs.GroupBy(o => o.pos).Count();

                if (distincts == 1)
                {
                    Console.WriteLine("We got one {0}", counter);
                    gotOne = true;
                }
                counter++;
            }
        }
    }

    public class Disc
    {
        private readonly int _initialPosition;
        private readonly int _numPositions;
        private readonly int _id;

        public int GetId()
        {
            return _id;
        }
        public int GetPosition(int timeTick)
        {
            return (_initialPosition + timeTick + _id) % _numPositions;
        }

        public Disc(int id, int numPositions, int initialPosition)
        {
            this._id = id;
            this._initialPosition = initialPosition;
            this._numPositions = numPositions;
        }
    }
}
