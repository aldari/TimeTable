using NUnit.Framework;
using System;
using System.Linq;
using TimeTable.Core;

namespace TimeTable.Tests
{
    [TestFixture]
    public class MaskIntervalTests
    {
        [Test]
        public void Test1_1()
        {
            var mask = new MaskInterval("*");

            var result = mask.HasInSchedule(12);

            Assert.True(result);
        }

        [Test]
        public void Test1_2()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.HasInSchedule(6);

            Assert.True(result);
        }

        [Test]
        public void Test2_1()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NearestValue(3);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test2_2()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NearestValue(4);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test2_3()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NearestValue(5);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test3_1()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NearestPrevValue(5);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test3_2()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NearestPrevValue(4);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test3_3()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NearestPrevValue(3);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test4_1()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NextValue(3);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test4_2()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NextValue(4);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test4_3()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.NextValue(5);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test5_1()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.PrevValue(5);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test5_2()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.PrevValue(4);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test5_3()
        {
            var mask = new MaskInterval("*/3");

            var result = mask.PrevValue(3);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Test6()
        {
            var mask = new MaskInterval("*/5", TimerTypes.Hour);

            var result = Enumerable.Range(0, 24).Select(x => mask.NextValue(x));

            foreach (var i in result)
                Console.WriteLine(i);
        }

        [Test]
        public void Test7()
        {
            var mask = new MaskInterval("*/5", TimerTypes.Hour);

            var result = Enumerable.Range(0, 24).Select(x => mask.PrevValue(x));

            foreach (var i in result)
                Console.WriteLine(i);
        }

        [Test]
        public void Test8()
        {
            var mask = new Mask("*", TimerTypes.Day);

            var result = mask.NearestPrevValue(1);

            Assert.AreEqual(1, result);
        }
    }
}
