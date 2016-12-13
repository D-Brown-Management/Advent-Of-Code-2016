using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day12CS
{
    class Program
    {
        static Dictionary<char, int> registers = new Dictionary<char, int>();
        private static string[] inputLines;
        static void Main(string[] args)
        {
            inputLines = File.ReadAllLines("input.txt");
            int instPointer = 0;

            

            registers['a'] = 0;
            registers['b'] = 0;
            registers['c'] = 1;
            registers['d'] = 0;

            
            do
            {
                
                var cmdType = GetCommandType(instPointer);
                //RenderScreen(instPointer);
                switch (cmdType)
                {
                    case AdventCommandType.CommandCpy:
                        CpyCommand(instPointer);
                        break;
                    case AdventCommandType.CommandInc:
                        IncCommand(instPointer);
                        break;
                    case AdventCommandType.CommandDec:
                        DecCommand(instPointer);
                        break;
                    case AdventCommandType.CommandJnz:
                        int newInst = JnzCommand(instPointer);
                        if (instPointer != newInst)
                        {
                            instPointer = newInst;
                            continue;
                        }
                        break;
                }

                instPointer++;
            } while (instPointer < inputLines.Length);
        }

        private static void RenderScreen(int ip)
        {
            
            Console.Clear();
            //for (int i = 0; i < instructions.Length; i++)
            //{
                
            //    if (ip == i)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Yellow;
            //        Console.Write(" >");
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.Write($"{i}:");
            //    }
            //    else
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.Write($"  {i}:");
            //    }
                
            //    Console.ForegroundColor = ConsoleColor.Gray;
            //    Console.WriteLine(instructions[i]);
            //}

            //Console.WriteLine("-----------------------");
            foreach (KeyValuePair<char, int> register in registers)
            {                
                Console.WriteLine($"{register.Key}: {register.Value}");
            }
        }

        private static int JnzCommand(int instPointer)
        {
            Regex jnzReg = new Regex("jnz (?<cmp>\\d+|[abcd]) (?<amt>-?\\d+)");

            var match = jnzReg.Match(inputLines[instPointer]);
            int cmpNum = 0;
            bool isNum = int.TryParse(match.Groups["cmp"].Value, out cmpNum);
            int instAmt = int.Parse(match.Groups["amt"].Value);

            if (isNum)
            {
                if (cmpNum != 0)
                {
                    //if (instAmt < 0)
                    //{
                    //    instAmt += 1;
                    //}
                    //else
                    //{
                    //    instAmt -= 1;
                    //}
                    instPointer += instAmt;
                }
            }
            else
            {
                char reg = char.Parse(match.Groups["cmp"].Value);
                if (registers[reg] != 0)
                {
                    //if (instAmt < 0)
                    //{
                    //    instAmt -= 1;
                    //}
                    //else
                    //{
                    //    instAmt += 1;
                    //}
                    instPointer += instAmt;
                }
            }

            return instPointer;            
        }

        private static void DecCommand(int instPointer)
        {
            char reg = inputLines[instPointer][4];
            registers[reg]--;
            
            
        }

        private static void IncCommand(int instPointer)
        {
            char reg = inputLines[instPointer][4];
            registers[reg]++;
            
        }

        private static void CpyCommand(int instPointer)
        {
            Regex cpyReg = new Regex("cpy (?<src>\\d+|[abcd]) (?<dst>[abcd])");

            var match = cpyReg.Match(inputLines[instPointer]);
            int srcNum = 0;
            bool isNum = int.TryParse(match.Groups["src"].Value, out srcNum);
            char dst = char.Parse(match.Groups["dst"].Value);

            if (isNum)
            {
                registers[dst] = srcNum;
            }
            else
            {
                char src = char.Parse(match.Groups["src"].Value);
                registers[dst] = registers[src];
            }
            
        }

        static AdventCommandType GetCommandType(int instPointer)
        {
            string command = inputLines[instPointer].Substring(0, 3);
            switch (command)
            {
                case "cpy":
                    return AdventCommandType.CommandCpy;                    
                case "jnz":
                    return AdventCommandType.CommandJnz;
                case "inc":
                    return AdventCommandType.CommandInc;
                case "dec":
                    return AdventCommandType.CommandDec;                    
            }
            return AdventCommandType.CommandErr;
        }

        enum AdventCommandType
        {
            CommandCpy,
            CommandJnz,
            CommandInc,
            CommandDec,
            CommandErr
        }
    }
}
