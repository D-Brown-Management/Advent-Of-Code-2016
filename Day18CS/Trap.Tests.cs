using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Day18CS
{
    [TestFixture]
    public class TrapTests
    {
        [Test]
        public void OnlyLeftIsTrap()
        {
            bool left = true;
            bool center = false;
            bool right = false;

            var output = Program.TrapTest(left, center, right);

            Assert.IsTrue(output);
        }

        [Test]
        public void OnlyRightIsTrap()
        {
            bool left = false;
            bool center = false;
            bool right = true;

            var output = Program.TrapTest(left, center, right);

            Assert.IsTrue(output);
        }

        [Test]
        public void LeftCenterNotRight()
        {
            bool left = true;
            bool center = true;
            bool right = false;

            var output = Program.TrapTest(left, center, right);

            Assert.IsTrue(output);
        }

        [Test]
        public void CenterRightNotLeft()
        {
            bool left = false;
            bool center = true;
            bool right = true;

            var output = Program.TrapTest(left, center, right);

            Assert.IsTrue(output);
        }

        [Test]
        public void CenterOnlyFalse()
        {
            var output = Program.TrapTest(false, true, false);
            Assert.IsFalse(output);

        }

        [Test]
        public void AllOnFalse()
        {
            var output = Program.TrapTest(true, true, true);
            Assert.IsFalse(output);
        }

        [Test]
        public void AllOffFalse()
        {
            var output = Program.TrapTest(false, false, false);
            Assert.IsFalse(output);
        }
    }
}
