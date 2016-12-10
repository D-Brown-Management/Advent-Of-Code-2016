using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MoreLinq;

namespace Day10CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var botRegex = new Regex("bot (?<botnum>\\d+)");
            var valRegex = new Regex("value (?<val>\\d+) goes to bot (?<botid>\\d+)");
            var operationRegex =
                new Regex(
                    "bot (?<botA>\\d+) gives (?<opA>low|high) to (?<destA>bot|output) (?<destANum>\\d+) and (?<opB>low|high) to (?<destB>bot|output) (?<destBNum>\\d+)");
            List<int> finishedLines = new List<int>();
            Dictionary<int, Bot> bots = new Dictionary<int, Bot>();
            Dictionary<int, List<Chip>> Outputs = new Dictionary<int, List<Chip>>();
            foreach (var line in input)
            {
                var matches = botRegex.Matches(line);
                foreach (Match match in matches)
                {
                    int botId = int.Parse(match.Groups["botnum"].Value);
                    if (!bots.ContainsKey(botId))
                    {
                        bots.Add(botId, new Bot(botId));
                    }
                }

            }

            for (int i = 0; i < input.Length; i++)
            {

                if (!valRegex.IsMatch(input[i]))
                {
                    continue;
                }
                var matches = valRegex.Match(input[i]);
                int botId = int.Parse(matches.Groups["botid"].Value);
                int chipVal = int.Parse(matches.Groups["val"].Value);

                bots[botId].GiveChipValue(chipVal);
                finishedLines.Add(i);
            }
            int finishedCount = 0;
            do
            {
                finishedCount = finishedLines.Count;
                for (int i = 0; i < input.Length; i++)
                {
                    if (finishedLines.Contains(i))
                    {
                        continue;
                    }
                    if (!operationRegex.IsMatch(input[i]))
                    {
                        continue;
                    }

                    var match = operationRegex.Match(input[i]);
                    var inputBotId = int.Parse(match.Groups["botA"].Value);
                    var opA = match.Groups["opA"].Value;
                    var destA = match.Groups["destA"].Value;
                    var destAId = int.Parse(match.Groups["destANum"].Value);

                    var opB = match.Groups["opB"].Value;
                    var destB = match.Groups["destB"].Value;
                    var destBId = int.Parse(match.Groups["destBNum"].Value);

                    if (!bots[inputBotId].IsReadyToGive)
                    {
                        continue;
                    }

                    // do A
                    if (destA == "bot")
                    {
                        switch (opA)
                        {
                            case "high":
                                bots[inputBotId].GiveHigherToBot(bots[destAId]);
                                break;
                            case "low":
                                bots[inputBotId].GiveLowerToBot(bots[destAId]);
                                break;
                        }

                        bots[destAId].CheckOutput();


                    }
                    else if (destA == "output")
                    {
                        if (!Outputs.ContainsKey(destAId))
                        {
                            Outputs[destAId] = new List<Chip>();
                        }
                        switch (opA)
                        {
                            case "high":
                                bots[inputBotId].GiveHigherToOutput(Outputs[destAId]);
                                break;
                            case "low":
                                bots[inputBotId].GiveLowerToOutput(Outputs[destAId]);
                                break;
                        }
                    }

                    // do A
                    if (destB == "bot")
                    {
                        switch (opB)
                        {
                            case "high":
                                bots[inputBotId].GiveHigherToBot(bots[destBId]);
                                break;
                            case "low":
                                bots[inputBotId].GiveLowerToBot(bots[destBId]);
                                break;
                        }

                        bots[destBId].CheckOutput();

                    }
                    else if (destB == "output")
                    {
                        if (!Outputs.ContainsKey(destBId))
                        {
                            Outputs[destBId] = new List<Chip>();
                        }
                        switch (opB)
                        {
                            case "high":
                                bots[inputBotId].GiveHigherToOutput(Outputs[destBId]);
                                break;
                            case "lower":
                                bots[inputBotId].GiveLowerToOutput(Outputs[destBId]);
                                break;
                        }
                    }
                    finishedLines.Add(i);
                }
            }
            while (finishedCount != finishedLines.Count) ;

            int outputMul = Outputs[0].First().Value*Outputs[1].First().Value*Outputs[2].First().Value;
            Console.WriteLine("The output mul is: {0}", outputMul);

        }
    }

    public class Bot
    {
        private readonly int _id;
        private readonly List<Chip> _chips;

        public Bot(int botId)
        {
            _id = botId;
            _chips = new List<Chip>();
            
        }

        public void GiveChipValue(int value)
        {
            var chip = new Chip() {Value = value};
            this.GiveChip(chip);
        }
        public void GiveChip(Chip input)
        {
            if(_chips.Count < 2) { 
                this._chips.Add(input);
            }
        }
        public int ChipCount => _chips.Count;
        public bool IsReadyToGive => _chips.Count == 2;
        public Chip Higher => _chips.MaxBy(c=>c.Value);
        public Chip Lower => _chips.MinBy(c => c.Value);

        public void GiveLowerToBot(Bot b)
        {            
            b.GiveChip(Lower);
            this._chips.Remove(Lower);
        }

        public void GiveHigherToBot(Bot b)
        {            
            b.GiveChip(Higher);
            this._chips.Remove(Higher);
        }

        public void GiveHigherToOutput(List<Chip> chips)
        {
            chips.Add(Higher);
            this._chips.Remove(Higher);
        }

        public void GiveLowerToOutput(List<Chip> chips)
        {
            chips.Add(Lower);
            this._chips.Remove(Lower);
        }

        public void CheckOutput()
        {
            if (Higher != null && Lower != null && Higher.Value == 61 && Lower.Value == 17)
            {
                Console.WriteLine("I have the value! My name is {0}", _id);
            }
        }
    }

    public class Chip
    {
        public int Value { get; set; }
    }
}
