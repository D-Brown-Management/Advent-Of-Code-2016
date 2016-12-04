using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3CS
{
    public class Part2
    {
        public void DoPartTwo()
        {
            var intList = new List<Tuple<int, int, int>>();
            var lines = File.ReadAllLines("input.txt");
            int counter = 0;
            for (int i=0; i<lines.Length; i++)
            {
                string num1 = lines[i].Substring(0, 5).Trim();
                string num2 = lines[i].Substring(5, 5).Trim();
                string num3 = lines[i].Substring(10, 5).Trim();

                int[] nums = new int[3];
                nums[0] = int.Parse(num1);
                nums[1] = int.Parse(num2);
                nums[2] = int.Parse(num3);
                
                var t = new Tuple<int,int,int>(nums[0], nums[1], nums[2]);
               
                intList.Add(t);
            }

            for (int j = 0; j < intList.Count; j+=3)
            {
                int[] set1 = new int[3];
                set1[0] = intList[0 + j].Item1;
                set1[1] = intList[1 + j].Item1;
                set1[2] = intList[2 + j].Item1;
                Array.Sort(set1);

                int[] set2 = new int[3];
                set2[0] = intList[0 + j].Item2;
                set2[1] = intList[1 + j].Item2;
                set2[2] = intList[2 + j].Item2;
                Array.Sort(set2);

                int[] set3 = new int[3];
                set3[0] = intList[0 + j].Item3;
                set3[1] = intList[1 + j].Item3;
                set3[2] = intList[2 + j].Item3;
                Array.Sort(set3);

                if (set1[0] + set1[1] > set1[2])
                {
                    counter++;
                }

                if (set2[0] + set2[1] > set2[2])
                {
                    counter++;
                }

                if (set3[0] + set3[1] > set3[2])
                {
                    counter++;
                }
                
               
            }

            Console.WriteLine($"Part 2: {counter}");            
        }
    }
}
