using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3CS
{
    public class Part1
    {
        public void DoPartOne()
        {
            var lines = File.ReadAllLines("input.txt");
            int counter = 0;
            foreach (string line in lines)
            {
                string num1 = line.Substring(0, 5).Trim();
                string num2 = line.Substring(5, 5).Trim();
                string num3 = line.Substring(10, 5).Trim();

                int[] nums = new int[3];
                nums[0] = int.Parse(num1);
                nums[1] = int.Parse(num2);
                nums[2] = int.Parse(num3);

                Array.Sort(nums);

                if (nums[0] + nums[1] > nums[2])
                {
                    counter++;
                }
            }

            Console.WriteLine($"Part 1: {counter}");            
        }
    }
}
