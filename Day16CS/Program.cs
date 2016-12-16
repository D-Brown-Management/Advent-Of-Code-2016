using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16CS
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            int maxLength = 35651584;
            string input = "10001001100000001";
            string formedString = input;
            while(formedString.Length < maxLength)
            {
                formedString = PermuteAndConcatInput(formedString, maxLength);
            }

            string inputChecksum = formedString;
            do
            {
                inputChecksum = CalcChecksum(inputChecksum);
            } while (inputChecksum.Length % 2 != 1);
            sw.Stop();

            Console.WriteLine("Time Taken in Seconds: {0}",sw.Elapsed.TotalSeconds);
            Console.ReadLine();
        }

        static string PermuteAndConcatInput(string input, int maxLength)
        {
            char[] inputAry = input.ToCharArray();
            Array.Reverse(inputAry);

            for(int i=0; i<inputAry.Length; i++)
            {
                if(inputAry[i] == '0')
                {
                    inputAry[i] = '1';
                } else
                {
                    inputAry[i] = '0';
                }
            }

            string inputCopy = new string(inputAry);

            string fullPermute = string.Concat(input, "0", inputCopy);

            if(fullPermute.Length > maxLength)
            {
                return fullPermute.Substring(0, maxLength);
            }
            else
            {
                return fullPermute;
            }
        }

        static string CalcChecksum(string input)
        {
            var ary = input.ToCharArray();

            StringBuilder charOutput = new StringBuilder();

            for (int i = 0; i < (ary.Length / 2); i++)
            {
                if(ary[(i*2)] == ary[(i*2)+1])
                {
                    charOutput.Append('1');
                } else
                {
                    charOutput.Append('0');
                }
            }



            return charOutput.ToString();
        }
    }
}
