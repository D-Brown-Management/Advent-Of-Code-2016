using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day14CS
{
    class Program
    {
        static MD5Cng cn = new MD5Cng();
        static Regex tripleRegex = new Regex("(?<digit>[a-zA-Z0-9])\\1\\1");
        static Regex quintupleRegex = new Regex("(?<digit>[a-zA-Z0-9])\\1\\1\\1\\1");
        static Dictionary<string,string> HashHistory = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            var input = "qzyelonm";
            int startIndex = 0;
            int maxMatches = 64;

            //var test = GetMD5("abc89", 2016);


            //var p1Matches = PartOne(input, startIndex, maxMatches);
            var p2Matches = PartTwo(input, startIndex, maxMatches);
            //Console.WriteLine("Part 1 Index: {0}", p1Matches[maxMatches-1].MatchIndex);
            Console.WriteLine("Part 2 Index: {0}", p2Matches[maxMatches-1].MatchIndex);
            Console.ReadLine();
        }

        static List<MatchResult> PartOne(string input , int startIndex, int maxMatches)
        {
            List<MatchResult> matches = new List<MatchResult>();

            while (matches.Count < maxMatches)
            {
                var triple = FindNextTriple(input, startIndex, 1);
                var quint = TestQuintuple(triple, input, 1);
                if (quint)
                {
                    matches.Add(triple);
                }
                startIndex = triple.MatchIndex + 1;
            }

            return matches;
        }

        static List<MatchResult> PartTwo(string input, int startIndex, int maxMatches)
        {
            int iterations = 2016;
            List<MatchResult> matches = new List<MatchResult>();

            while (matches.Count < maxMatches)
            {
                var triple = FindNextTriple(input, startIndex, iterations);
                var quint = TestQuintuple(triple, input, iterations);
                if (quint)
                {
                    matches.Add(triple);
                    Console.WriteLine("Match Found! Index:{0}.  There are now {1} matches.",triple.MatchIndex, matches.Count);
                }
                startIndex = triple.MatchIndex + 1;
            }

            return matches;
        }

        static bool TestQuintuple(MatchResult match, string input, int iterations)
        {
            
            int counter = match.MatchIndex+1;
            for (int i = 0; i < 1000; i++)
            {
                var qm = quintupleRegex.IsMatch(GetMD5($"{input}{counter}", iterations));
                if (qm)
                {
                    var rm = quintupleRegex.Match(GetMD5($"{input}{counter}", iterations));
                    if (rm.Groups["digit"].Value[0] == match.MatchChar)
                    {
                        return true;
                    }
                }
                counter++;
            }

            return false;
        }

        static MatchResult FindNextTriple(string input, int startIndex, int iterations)
        {
            int indexer = startIndex;
            MatchResult result = new MatchResult();
            bool matchFound = false;
            do
            {
                var md5 = GetMD5($"{input}{indexer}", iterations);
                matchFound = tripleRegex.IsMatch(md5);
                if (matchFound)
                {
                    var match = tripleRegex.Match(md5);
                    result.MatchChar = match.Groups["digit"].Value[0];
                    result.MatchIndex = indexer;
                }
                indexer++;
            } while (!matchFound);

            return result;
        }

        static string GetMD5(string input, int iterations)
        {
            string workingString = input.ToLower();
            if (HashHistory.ContainsKey(input))
            {
                return HashHistory[input];
            }

            for (int i = 0; i < iterations+1; i++)
            {            
                var bytes = cn.ComputeHash(Encoding.ASCII.GetBytes(workingString));
                var output = BitConverter.ToString(bytes).Replace("-", string.Empty);
                workingString = output.ToLower();
            }
            HashHistory.Add(input, workingString);
            return workingString;
        }

        struct MatchResult
        {
            public char MatchChar;
            public int MatchIndex;
        }
    }
}
