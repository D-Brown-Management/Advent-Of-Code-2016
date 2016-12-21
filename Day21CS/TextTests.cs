using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Day21CS
{
    [TestFixture]
    public class TextTests
    {
        [Test]
        public void Swap_Low_To_High_Correct()
        {
            int firstChar = 0;
            int lastChar = 4;
            var output = Program.SwapCharacterIndex(firstChar, lastChar, "abcde");

            Assert.AreEqual("ebcda", output);
        }

        [Test]
        public void Swap_High_To_Low_Correct()
        {
            int firstChar = 4;
            int lastChar = 0;
            var output = Program.SwapCharacterIndex(firstChar, lastChar, "abcde");

            Assert.AreEqual("ebcda", output);
        }

        [Test]
        public void Swap_By_Chars_Single_Correct()
        {
            char firstChar = 'b';
            char lastChar = 'd';
            var output = Program.SwapCharacter(firstChar, lastChar, "abcde");

            Assert.AreEqual("adcbe", output);
        }

        [Test]
        public void Swap_By_Chars_Multiple_Correct()
        {
            char firstChar = 'b';
            char lastChar = 'd';
            var output = Program.SwapCharacter(firstChar, lastChar, "abababcdcdcde");

            Assert.AreEqual("adadadcbcbcbe", output);
        }

        [Test]
        public void Swap_By_Chars_MultipleSimultaneous_Correct()
        {
            char firstChar = 'b';
            char lastChar = 'd';
            var output = Program.SwapCharacter(firstChar, lastChar, "abbbcddde");

            Assert.AreEqual("adddcbbbe", output);
        }

        [Test]
        public void Reverse_By_Index_Start()
        {
            int first = 0;
            int last = 4;

            var output = Program.ReverseByIndex(first, last, "abcde");

            Assert.AreEqual("edcba", output);
        }


        [Test]
        public void Reverse_By_Index_Mid()
        {
            int first = 1;
            int last = 3;

            var output = Program.ReverseByIndex(first, last, "abcde");

            Assert.AreEqual("adcbe", output);
        }

        [Test]
        public void Reverse_By_Index_End()
        {
            int first = 2;
            int last = 4;

            var output = Program.ReverseByIndex(first, last, "abcde");

            Assert.AreEqual("abedc", output);
        }

        [Test]
        public void Extract_And_Insert_Edge()
        {
            int extractFrom = 0;
            int insertTo = 3;

            var output = Program.ExtractAndInsertByIndex(extractFrom, insertTo, "abcde");
            Assert.AreEqual("bcdae", output);
        }

        [Test]
        public void Extract_And_Insert_Mid()
        {
            int extractFrom = 1;
            int insertTo = 3;

            var output = Program.ExtractAndInsertByIndex(extractFrom, insertTo, "abcde");
            Assert.AreEqual("acdbe", output);
        }

        [Test]
        public void Rotate_String_Direction_Right_None()
        {
            //abcde
            int rotateNum = 0;
            string rotateDir = "right";

            var output = Program.Rotate(rotateNum, rotateDir, "abcde");

            Assert.AreEqual("abcde", output);

        }

        [Test]
        public void Rotate_String_Direction_Right_Once()
        {
            //abcde
            int rotateNum = 1;
            string rotateDir = "right";

            var output = Program.Rotate(rotateNum, rotateDir, "abcde");

            Assert.AreEqual("eabcd", output);

        }

        [Test]
        public void Rotate_String_Direction_Right_Twice()
        {
            //abcde
            int rotateNum = 2;
            string rotateDir = "right";

            var output = Program.Rotate(rotateNum, rotateDir, "abcde");

            Assert.AreEqual("deabc", output);

        }


        [Test]
        public void Rotate_String_Direction_Left_Once()
        {
            //abcde
            int rotateNum = 1;
            string rotateDir = "left";

            var output = Program.Rotate(rotateNum, rotateDir, "abcde");

            Assert.AreEqual("bcdea", output);

        }

        [Test]
        public void Rotate_String_Direction_Left_Twice()
        {
            //abcde
            int rotateNum = 2;
            string rotateDir = "left";

            var output = Program.Rotate(rotateNum, rotateDir, "abcde");

            Assert.AreEqual("cdeab", output);

        }


        [Test]
        public void Rotate_String_Position_Less_Than_Four()
        {
            char first = 'b';
            var output = Program.RotateByChar(first, "abcde");

            Assert.AreEqual("deabc", output);
        }

        [Test]
        public void Rotate_String_Position_Greater_Than_Four()
        {
            char first = 'd';
            var output = Program.RotateByChar(first, "ecabd");

            Assert.AreEqual("decab", output);
        }

        [Test]
        public void Rotate_String_Position_Big()
        {
            char first = 'g';
            var output = Program.RotateByChar(first, "fbhdecga");

            Assert.AreEqual("fbhdecga", output);
        }

        [Test]
        public void RotateByChar_Then_Undo_Should_Be_Original()
        {
            var input = "abcdefgh";

            var output = Program.RotateByChar('f', input);

            var realOutput = Program.UndoRotateByChar('f', output);
            Console.WriteLine("Input: {0}", input);
            Console.WriteLine("First Output: {0}", output);
            Console.WriteLine("Undone Output: {0}", realOutput);
            Assert.AreEqual(input, realOutput);
        }
    }
}
