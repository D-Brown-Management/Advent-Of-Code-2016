using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Day9CS
{
    class Program
    {
        public static Regex DigitRegex = new Regex(@"\((?<a>\d+)x(?<b>\d+)\)");
        static void Main(string[] args)
        {
            var line = File.ReadAllLines("input.txt");
            //var line = "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN";

            var decompressed = DecompressStringV1(line[0], 0);
            var decompressedv2 = DecompressStringV2(line[0]);

            Console.WriteLine($"Decompressed Len: {decompressed.Trim().Length}");
            
            Console.WriteLine($"Decompressed V2 Len: {decompressedv2}");
            Console.ReadLine();
        }

      
        public static string DecompressStringV1(string compressed, int startPosition)
        {
            var preDone = compressed.Substring(0, startPosition);
            var toDecomp = compressed.Substring(startPosition);

            var isCompressed = DigitRegex.IsMatch(toDecomp);
            if (!isCompressed)
            {
                return compressed;
            }

            var match = DigitRegex.Match(toDecomp);
            var str = toDecomp.Substring(match.Index+match.Length, int.Parse(match.Groups["a"].Value));

            StringBuilder sb = new StringBuilder();
            sb.Append(toDecomp.Substring(0, match.Index));
            for (int i = 0; i < int.Parse(match.Groups["b"].Value); i++)
            {
                sb.Append(str);
            }
            var semiDecomp = sb.ToString();
            var remainder = toDecomp.Substring(match.Index + match.Length + int.Parse(match.Groups["a"].Value));

            return DecompressStringV1(string.Concat(preDone, semiDecomp, remainder), startPosition+semiDecomp.Length);
        }

        public static long DecompressStringV2(string compressed)
        {
            var isCompressed = DigitRegex.IsMatch(compressed);
            if (!isCompressed)
            {
                return compressed.Length;
            }
            long count = 0;
            int startNum = 0;

            do
            {
                if (!DigitRegex.IsMatch(compressed, startNum))
                {
                    count += compressed.Length - startNum;
                    break;
                }
                var match = DigitRegex.Match(compressed, startNum);
                int matchLen = int.Parse(match.Groups["a"].Value);
                int numExpansion = int.Parse(match.Groups["b"].Value);

                int matchPosition = match.Index;
                int matchMetaLen = match.Length;

                var subMatch = compressed.Substring(matchPosition + matchMetaLen, matchLen);

                if (startNum != matchPosition)
                {
                    count += matchPosition-startNum;
                }

                if (!DigitRegex.IsMatch(subMatch))
                {
                    count += matchLen*numExpansion;
                }
                else
                {
                    count += numExpansion*DecompressStringV2(subMatch);
                }
                
                

                startNum = matchPosition + matchMetaLen + matchLen;
            } while (startNum < compressed.Length);
            return count;
            //int totalCount = 0;
            //foreach (Match match in matchList)
            //{
            //    var leading = match.Index;
            //    var subMatch = compressed.Substring(match.Index + match.Length, int.Parse(match.Groups["a"].Value));
            //    int subCount = 0;
            //    if (DigitRegex.IsMatch(subMatch))
            //    {
            //        subCount = int.Parse(match.Groups["b"].Value) * DecompressStringV2(subMatch);
            //    }
            //    else
            //    {
            //        subCount = int.Parse(match.Groups["a"].Value) * int.Parse(match.Groups["b"].Value);
            //    }
            //    totalCount += leading + subCount;
            //}



            //return totalCount;

        }
    }
}
