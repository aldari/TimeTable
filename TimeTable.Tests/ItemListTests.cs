using NUnit.Framework;
using TimeTable.Core;
using System;

namespace TimeTable.Tests
{
    [TestFixture]
    public class TimerListTests
    {
        [Test]
        public void Test1()
        {
            var list = new IntervalList("5,7,10");

            Assert.True(list.HasInSchedule(5));
            Assert.True(list.HasInSchedule(7));
            Assert.True(list.HasInSchedule(10));
            Assert.False(list.HasInSchedule(11));
            Assert.False(list.HasInSchedule(8));
        }

        [Test]
        public void Test2_1()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestValue(10);

            Assert.AreEqual(10, r);
        }

        [Test]
        public void Test2_2()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestValue(11);

            Assert.AreEqual(12, r);
        }

        [Test]
        public void Test2_3()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestValue(18);

            Assert.AreEqual(5, r);
        }

        [Test]
        public void Test2_4()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestValue(17);

            Assert.AreEqual(17, r);
        }

        [Test]
        public void Test3_1()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestPrevValue(10);

            Assert.AreEqual(10, r);
        }

        [Test]
        public void Test3_2()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestPrevValue(11);

            Assert.AreEqual(10, r);
        }

        [Test]
        public void Test3_3()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestPrevValue(3);

            Assert.AreEqual(17, r);
        }

        [Test]
        public void Test3_4()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NearestPrevValue(5);

            Assert.AreEqual(5, r);
        }

        [Test]
        public void Test4()
        {
            var list = new IntervalList("1,2,3-5,10-20/3");

            Console.WriteLine(list.ToString());

        }

        [Test]
        public void Test5_1()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NextValue(10);

            Assert.AreEqual(12, r);
        }

        [Test]
        public void Test5_2()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NextValue(11);

            Assert.AreEqual(12, r);
        }

        [Test]
        public void Test5_3()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NextValue(18);

            Assert.AreEqual(5, r);
        }

        [Test]
        public void Test5_4()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.NextValue(17);

            Assert.AreEqual(5, r);
        }

        [Test]
        public void Test6_1()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.PrevValue(10);

            Assert.AreEqual(7, r);
        }

        [Test]
        public void Test6_2()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.PrevValue(11);

            Assert.AreEqual(10, r);
        }

        [Test]
        public void Test6_3()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.PrevValue(3);

            Assert.AreEqual(17, r);
        }

        [Test]
        public void Test6_4()
        {
            var list = new IntervalList("5,7,10,12,15,17");

            var r = list.PrevValue(5);

            Assert.AreEqual(17, r);
        }
    }
}
