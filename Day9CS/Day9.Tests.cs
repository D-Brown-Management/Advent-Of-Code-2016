using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Day9CS
{
    [TestFixture]
    public class Day9
    {
        [Test]
        public void No_Math_Returns_Correct_Length()
        {
            var input = "444";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(input.Length, output);
        }

        [Test]
        public void Simple_Math_Returns_Correct_Length()
        {
            var input = "(2x2)AA";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(4, output);
        }

        [Test]
        public void Simple_Math_With_Suffix_Returns_Correct_Length()
        {
            var input = "(2x2)AAZ";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(5, output);
        }

        [Test]
        public void Simple_Math_With_Prefix_Returns_Correct_Length()
        {
            var input = "Z(2x2)AA";
            var output = Program.DecompressStringV2(input);
            Assert.AreEqual(5, output);
        }

        [Test]
        public void Simple_Math_With_Two_Terms_Returns_Correct_Length()
        {
            var input = "(2x2)AA(2x2)AA";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(8, output);
        }

        [Test]
        public void Simple_Math_With_Two_Terms_And_Prefix_Returns_Correct_Length()
        {
            var input = "Z(2x2)AA(2x2)AA";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(9, output);
        }

        [Test]
        public void Simple_Math_With_Two_Terms_And_Two_Prefix_Returns_Correct_Length()
        {
            var input = "Z(2x2)AAZ(2x2)AA";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(10, output);
        }

        [Test]
        public void Complex_Math_Returns_Correct_Length()
        {
            var input = "(7x2)(2x2)AA";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(8, output);
        }

        [Test]
        public void Complex_Math_Two_Inner_Returns_Correct_Length()
        {
            var input = "(14x2)(2x2)AA(2x2)AA";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(16, output);
        }

        [Test]
        public void Complex_Math_With_Suffix_Returns_Correct_Length()
        {
            var input = "(7x2)(2x2)AAZ";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(9, output);
        }

        [Test]
        public void Complex_Math_Prefix_Returns_Correct_Length()
        {
            var input = "Z(7x2)(2x2)AA";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(9, output);
        }

        [Test]
        public void Complex_Math_Prefix_Suffix_Returns_Correct_Length()
        {
            var input = "Z(7x2)(2x2)AAABC";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(12, output);
        }

        [Test]
        public void Very_Complex_Math_Prefix_Suffix_Returns_Correct_Length()
        {
            var input = "(19x4)SDF(5x33)DFSDFJKDHHGF(16x10)Z(7x2)(2x2)AAABC";
            var output = Program.DecompressStringV2(input);

            Assert.AreEqual(814, output);
        }
    }
}
