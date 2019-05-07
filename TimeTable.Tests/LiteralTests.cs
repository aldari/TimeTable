using NUnit.Framework;
using TimeTable.Core;

namespace TimeTable.Tests
{
    [TestFixture]
    public class LiteralTests
    {
        [Test]
        public void Test1()
        {
            var literal = new Literal("12");

            var result = literal.HasInSchedule(12);

            Assert.True(result);
        }

        [Test]
        public void Test2()
        {
            var literal = new Literal("12");

            var result = literal.NearestValue(12);

            Assert.AreEqual(12, result);
        }

        [Test]
        public void Test3()
        {
            var literal = new Literal("12");

            var result = literal.NearestPrevValue(12);

            Assert.AreEqual(12, result);
        }
    }
}
