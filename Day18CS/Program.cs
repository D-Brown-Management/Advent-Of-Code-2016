using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18CS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StringBuilder totalOutput = new StringBuilder();
            var firstLine = File.ReadAllLines("input.txt");

            

            string workingLine = firstLine[0];
            totalOutput.AppendLine(workingLine);
            for (int i = 0; i < 399999; i++)
            {
                workingLine = GenerateRow(workingLine);
                totalOutput.AppendLine(workingLine);
            }
            
            var outputString = totalOutput.ToString();
            int safeCount = outputString.ToCharArray().Count(c => c == '.');
            Console.WriteLine("Total Safe Count {0}", safeCount);
        }

        public static string GenerateRow(string priorRow)
        {
            StringBuilder newBuilder = new StringBuilder();

            for (int i = 0; i < priorRow.Length; i++)
            {
                bool isTrap = false;
                if (i == 0)
                {
                    isTrap = TrapTestChar('.', priorRow[i], priorRow[i + 1]);
                }
                else if (i == (priorRow.Length - 1))
                {
                    isTrap = TrapTestChar(priorRow[i - 1], priorRow[i], '.');
                }
                else
                {
                    isTrap = TrapTestChar(priorRow[i - 1], priorRow[i], priorRow[i + 1]);
                }

                if (isTrap)
                {
                    newBuilder.Append('^');
                }
                else
                {
                    newBuilder.Append('.');
                }
            }
            

            return newBuilder.ToString();
        }

        public static bool TrapTestChar(char left, char center, char right)
        {
            var leftBool = left == '^';
            var centerBool = center == '^';
            var rightBool = right == '^';

            return TrapTest(leftBool, centerBool, rightBool);
        }

        public static bool TrapTest(bool left, bool center, bool right)
        {
            var onlyLeftTest = left && !(center || right);
            var onlyRightTest = !(left || center) && right;
            var leftCenter = (left && center) && !right;
            var rightCenter = (center && right) && !left;

            return onlyLeftTest || onlyRightTest || leftCenter || rightCenter;
        }
    }
}
