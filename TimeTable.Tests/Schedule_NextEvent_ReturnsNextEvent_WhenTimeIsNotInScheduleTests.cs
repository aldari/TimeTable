using NUnit.Framework;
using System;

namespace TimeTable.Tests
{
    [TestFixture]
    public class Schedule_NextEvent_ReturnsNextEvent_WhenTimeIsNotInScheduleTests
    {
        [Test]
        public void Test1_1()
        {
            var schedule = new Core.Schedule("12:13:14");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 29));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 14, 0), result);
        }

        [Test]
        public void Test1_2()
        {
            var schedule = new Core.Schedule("12:13:14.115");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 29));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 14, 115), result);
        }

        [Test]
        public void Test1_3()
        {
            var schedule = new Core.Schedule("2009.10.11 12:13:14");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 29));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 14), result);
        }

        [Test]
        public void Test1_4()
        {
            var schedule = new Core.Schedule("2009.10.11 6 12:13:14.115");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 115));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 14, 115), result);
        }

        [Test]
        public void Test2_1()
        {
            var schedule = new Core.Schedule("*:*:*");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 29));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 15, 0), result);
        }

        [Test]
        public void Test2_2()
        {
            var schedule = new Core.Schedule("*:*:*.*");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 29));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 14, 30), result);
        }

        [Test]
        public void Test2_3()
        {
            var schedule = new Core.Schedule("*.*.* *:*:*");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 29));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 15, 0), result);
        }

        [Test]
        public void Test2_4()
        {
            var schedule = new Core.Schedule("*.*.* * *:*:*.*");

            var result = schedule.NextEvent(new DateTime(2009, 10, 11, 12, 13, 14, 29));

            Assert.AreEqual(new DateTime(2009, 10, 11, 12, 13, 14, 30), result);
        }
    }
}
