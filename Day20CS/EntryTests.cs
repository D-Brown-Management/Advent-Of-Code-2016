using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Day20CS
{
    [TestFixture]
    public class EntryTests
    {
        [Test]
        public void Contiguous_Block_Should_Return_Entire_Block()
        {
            var first = new Entry() { Low = 0, High = 10};
            var second = new Entry() { Low = 11, High = 15};

            var output = Program.CombineEntries(first, second);
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Low);
            Assert.AreEqual(15, output.High);
        }

        [Test]
        public void Offset_Block_Should_Return_Entire_Block()
        {
            var first = new Entry() { Low = 0, High = 15 };
            var second = new Entry() { Low = 12, High = 30 };

            var output = Program.CombineEntries(first, second);
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Low);
            Assert.AreEqual(30, output.High);
        }

        [Test]
        public void Nested_Block_Should_Return_First_Block()
        {
            var first = new Entry() { Low = 0, High = 20 };
            var second = new Entry() { Low = 11, High = 15 };

            var output = Program.CombineEntries(first, second);
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Low);
            Assert.AreEqual(20, output.High);
        }

        [Test]
        public void Same_Block_Should_Return_First_Block()
        {
            var first = new Entry() {Low = 5, High = 20};
            var output = Program.CombineEntries(first, first);

            Assert.IsNotNull(output);
            Assert.AreEqual(5, output.Low);
            Assert.AreEqual(20, output.High);
        }

        [Test]
        public void Non_Contiguous_Block_Should_Return_Null()
        {
            var first = new Entry() {Low = 0, High = 20};
            var second = new Entry() {Low = 25, High = 30};
            var output = Program.CombineEntries(first, second);
            Assert.IsNull(output);
        }
    }
}
