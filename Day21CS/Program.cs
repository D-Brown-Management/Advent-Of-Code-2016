using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day21CS
{
    public class Program
    {
        static void Main(string[] args)
        {
            var initialInput = "abcdefgh";
            var scrambledInput = "fbgdceah";

            var input = File.ReadAllLines("input.txt");
            

            var passwordHash = PermutePassword(initialInput, input);

            var cracked = CrackPassword(scrambledInput, input);
           
            //var match = Regex.Match("sdfsdf", swapPosRegex);
            Console.WriteLine("Part 1 Result Is {0}", passwordHash);
            Console.WriteLine("Part 2 Result Is {0}", cracked);
            Console.ReadLine();

        }

        public static string PermutePassword(string input, string[] instructions)
        {
            var swapPosRegex = @"swap position (?<firstPos>\d+) with position (?<secondPos>\d+)";
            var swapLetterRegex = @"swap letter (?<firstLetter>[a-zA-Z]) with letter (?<secondLetter>[a-zA-Z])";
            var rotateStepRegex = @"rotate (?<dir>left|right) (?<num>\d+) step[s]?";
            var rotatePositionRegex = @"rotate based on position of letter (?<letter>[a-zA-Z])";
            var reverseRegex = @"reverse positions (?<first>\d+) through (?<second>\d+)";
            var movePositionRegex = @"move position (?<first>\d+) to position (?<second>\d+)";
            string initialInput = input;
            foreach (string line in instructions)
            {
                //Console.WriteLine("Input: {0} \r\nInstruction: {1}\r\n", initialInput, line);
                var swapPosMatch = Regex.Match(line, swapPosRegex);
                if (swapPosMatch.Success)
                {
                    //Console.WriteLine("Matched Swap Position");
                    int first = int.Parse(swapPosMatch.Groups["firstPos"].Value);
                    int second = int.Parse(swapPosMatch.Groups["secondPos"].Value);
                    initialInput = SwapCharacterIndex(first, second, initialInput);
                    continue;
                }

                var swapLetterMatch = Regex.Match(line, swapLetterRegex);
                if (swapLetterMatch.Success)
                {
                    //Console.WriteLine("Matched Swap Letter");
                    char first = char.Parse(swapLetterMatch.Groups["firstLetter"].Value);
                    char second = char.Parse(swapLetterMatch.Groups["secondLetter"].Value);
                    initialInput = SwapCharacter(first, second, initialInput);
                    continue;
                }

                var rotateStepMatch = Regex.Match(line, rotateStepRegex);
                if (rotateStepMatch.Success)
                {
                    //Console.WriteLine("Matched Rotate Steps");
                    string dir = rotateStepMatch.Groups["dir"].Value;
                    int num = int.Parse(rotateStepMatch.Groups["num"].Value);
                    initialInput = Rotate(num, dir, initialInput);
                    continue;
                }

                var rotatePosMatch = Regex.Match(line, rotatePositionRegex);
                if (rotatePosMatch.Success)
                {
                   // Console.WriteLine("Matched Rotate Position");
                    char letter = char.Parse(rotatePosMatch.Groups["letter"].Value);
                    initialInput = RotateByChar(letter, initialInput);
                    continue;
                }
                var reverseMatch = Regex.Match(line, reverseRegex);
                if (reverseMatch.Success)
                {
                    //Console.WriteLine("Matched Reverse");
                    int first = int.Parse(reverseMatch.Groups["first"].Value);
                    int second = int.Parse(reverseMatch.Groups["second"].Value);
                    initialInput = ReverseByIndex(first, second, initialInput);
                    continue;
                }

                var movePosMatch = Regex.Match(line, movePositionRegex);
                if (movePosMatch.Success)
                {
                    //Console.WriteLine("Matched Move Position");
                    int first = int.Parse(movePosMatch.Groups["first"].Value);
                    int second = int.Parse(movePosMatch.Groups["second"].Value);
                    initialInput = ExtractAndInsertByIndex(first, second, initialInput);
                    continue;
                }

                Console.WriteLine("---- NO MATCHES FOUND WTF ------");
            }

            return initialInput;
        }



        public static string CrackPassword(string input, string[] instructions)
        {
            string cracked = null;

            var perms = Permutations(input.ToCharArray().ToList());
            foreach (var perm in perms)
            {
                var trial = new string(perm.ToArray());
                Console.WriteLine("Trying Perm {0}", trial);
                var finish = PermutePassword(trial, instructions);
                if (finish == input)
                {
                    cracked = trial;
                    Console.WriteLine("Crack Found!");
                    break;
                }
            }

            //var finish = PermutePassword(str, instructions);
            //if (finish == input)
            //{
            //    Debug.WriteLine("Crack Found!");
            //    cracked = str;
            //}





            return cracked;
            
        }

        private static IList<IList<T>> Permutations<T>(IList<T> list)
        {
            List<IList<T>> perms = new List<IList<T>>();
            if (list.Count == 0)
                return perms; // Empty list.
            int factorial = 1;
            for (int i = 2; i <= list.Count; i++)
                factorial *= i;
            for (int v = 0; v < factorial; v++)
            {
                List<T> s = new List<T>(list);
                int k = v;
                for (int j = 2; j <= list.Count; j++)
                {
                    int other = (k % j);
                    T temp = s[j - 1];
                    s[j - 1] = s[other];
                    s[other] = temp;
                    k = k / j;
                }
                perms.Add(s);
            }
            return perms;
        }


        public static string SwapCharacterIndex(int first, int second, string input)
        {
            char tempChar;
            var inputArray = input.ToCharArray();

            tempChar = inputArray[second];
            inputArray[second] = inputArray[first];
            inputArray[first] = tempChar;

            return new string(inputArray);
        }

        public static string SwapCharacter(char first, char second, string input)
        {
            var charAry = input.ToCharArray();
            List<int> firstMatches = new List<int>();
            List<int> secondMatches = new List<int>();
            
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == first)
                {
                    firstMatches.Add(i);                    
                }
                else if (input[i] == second)
                {
                    secondMatches.Add(i);                    
                }
            }

            foreach (int firstMatch in firstMatches)
            {
                charAry[firstMatch] = second;
            }

            foreach (int secondMatch in secondMatches)
            {
                charAry[secondMatch] = first;
            }

            return new string(charAry);
        }

        public static string ReverseByIndex(int first, int second, string input)
        {
            var inputAry = input.ToCharArray();

            Array.Reverse(inputAry, first, (second - first)+1);
            
            return new string(inputAry);
        }

        public static string ExtractAndInsertByIndex(int extractFrom, int insertTo, string input)
        {
            var inputAry = input.ToList();

            char temp = inputAry[extractFrom];

            inputAry.RemoveAt(extractFrom);
            inputAry.Insert(insertTo, temp);

            return new string(inputAry.ToArray());
        }

        public static string Rotate(int offset, string direction, string input)
        {
            var inputAry = input.ToCharArray();

            if (offset > input.Length)
            {
                offset = offset%input.Length;
            }

            if (direction != "right")
            {
                offset = -offset;
            }

            if (offset == 0) return input;
            if (offset > 0)
            {
                var temp = new char[offset];
                System.Array.Copy(inputAry, inputAry.Length - offset, temp, 0, offset);
                System.Array.Copy(inputAry, 0, inputAry, offset, inputAry.Length - offset);
                System.Array.Copy(temp, 0, inputAry, 0, offset);
            }
            else
            {
                var temp = new char[-offset];
                System.Array.Copy(inputAry, 0, temp, 0, -offset);
                System.Array.Copy(inputAry, -offset, inputAry, 0, inputAry.Length + offset);
                System.Array.Copy(temp, 0, inputAry, inputAry.Length + offset, -offset);
            }

            return new string(inputAry);
        }

        public static string RotateByChar(char first, string input)
        {
            int charIndex = input.IndexOf(first);
            int numRotations = 1+charIndex;
            if (charIndex > 3)
            {
                numRotations = numRotations + 1;
            }

            return Rotate(numRotations, "right", input);
        }

        public static string UndoRotateByChar(char first, string input)
        {
            int charIndex = input.IndexOf(first);
            int numRotations = 1 + charIndex;
            if (charIndex < 3)
            {
                numRotations = numRotations + 1;
            }

            return Rotate(numRotations, "left", input);
        }
    }
}
