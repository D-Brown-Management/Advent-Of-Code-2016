using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7CS
{
    class Program
    {
        public static Regex regex = new Regex(@"(?<ipv4>[a-z]+)|(?:\[(?<hyper>[a-z]+)\])");
        static void Main(string[] args)
        {


            //string[] input = new string[]
            //{
            //    "aba[bab]xyz" ,
            //    "xyx[xyx]xyx",
            //    "aaa[kek]eke",
            //    "zazbz[bzb]cdb"
            //};


            var input = File.ReadAllLines("input.txt");
            var tlsCount = GetTLSCount(input);
            var sslCount = GetSSLCount(input);


            //bool test = DetectAbba("asasasasafdbddc");

            List<string> emptyAbaList = new List<string>();
            var outputList = GetABA("zazbz", emptyAbaList);
            var transformList = outputList.Select(TransformABA);

            Console.WriteLine($"Part One: {tlsCount}");   
            Console.WriteLine($"Part Two: {sslCount}");   
        }

        public static int GetSSLCount(string[] input)
        {
            int counter = 0;
            foreach (string line in input)
            {
                List<string> emptyAbaList = new List<string>();
                List<string> emptyBabList = new List<string>();
                IEnumerable<string> outputList = new List<string>();
                IEnumerable<string> outputBabList = new List<string>();
                
                List<string> ips = new List<string>();
                List<string> mids = new List<string>();
                var matches = regex.Matches(line);
                bool matchGood = false;
                foreach (Match match in matches)
                {
                    var goodMatches = match.Groups["ipv4"].Captures;
                    var badMatches = match.Groups["hyper"].Captures;

                    foreach (Capture goodMatch in goodMatches)
                    {
                        ips.Add(goodMatch.Value);
                    }

                    foreach (Capture badMatch in badMatches)
                    {
                        mids.Add(badMatch.Value);
                    }
                }

                foreach (var ip in ips)
                {
                    outputList = GetABA(ip, emptyAbaList).Select(TransformABA);
                }
                foreach (var mid in mids)
                {
                    outputBabList = GetABA(mid, emptyBabList);
                }

                bool hasMatch = outputList.Select(x => x).Intersect(outputBabList).Any();
                if (hasMatch)
                {
                    counter++;
                }
            }


            return counter;
        }
        

        private static int GetTLSCount(string[] input)
        {
            int counter = 0;
            foreach (string line in input)
            {
                var matches = regex.Matches(line);
                bool matchGood = false;
                foreach (Match match in matches)
                {
                    var goodMatches = match.Groups["ipv4"].Captures;
                    var badMatches = match.Groups["hyper"].Captures;



                    foreach (Capture goodMatch in goodMatches)
                    {
                        matchGood |= DetectAbba(goodMatch.Value);
                    }

                    foreach (Capture badMatch in badMatches)
                    {
                        if (DetectAbba(badMatch.Value))
                        {
                            matchGood = false;
                            goto stuff;
                        }
                    }


                }
                stuff:
                if (matchGood)
                {
                    counter++;
                }

            }
            return counter;
        }

        static bool DetectAbba(string inputString)
        {
            if (inputString.Length < 4)
                return false;

            if (inputString[0] == inputString[3] && inputString[1] == inputString[2] && inputString[0] != inputString[1])
            {
                return true;
            }

            return DetectAbba(inputString.Substring(1));
        }

        static List<string> GetABA(string input, List<string> foundAba)
        {
            if (input.Length < 3)
            {
                return foundAba;
            }

            if (input[0] == input[2])
            {
                foundAba.Add(input.Substring(0,3));
            }
            return GetABA(input.Substring(1), foundAba);
        }

        static string TransformABA(string input)
        {
            return $"{input[1]}{input[0]}{input[1]}";
        }
    }
}
