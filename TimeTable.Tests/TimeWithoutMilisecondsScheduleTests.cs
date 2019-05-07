using NUnit.Framework;
using System;

namespace TimeTable.Tests
{
    [TestFixture]
    public class TimeWithoutMilisecondsScheduleTests
    {
        [Test]
        public void Test1()
        {
            var schedule = new Core.Schedule("*:*:*");

            var result = schedule.NearestEvent(new DateTime(2019, 05, 01, 11, 01, 12, 0));

            Assert.AreEqual(new DateTime(2019, 05, 01, 11, 01, 12, 0), result);
        }

        [Test]
        public void Test2()
        {
            var schedule = new Core.Schedule("*:*:*");

            var result = schedule.NearestPrevEvent(new DateTime(2019, 05, 01, 11, 01, 12, 0));

            Assert.AreEqual(new DateTime(2019, 05, 01, 11, 01, 12, 0), result);
        }

        [Test]
        public void Test3()
        {
            var schedule = new Core.Schedule("*:*:*");

            var result = schedule.NextEvent(new DateTime(2019, 05, 01, 11, 01, 13, 0));

            Assert.AreEqual(new DateTime(2019, 05, 01, 11, 01, 13, 0), result);
        }

        [Test]
        public void Test4()
        {
            var schedule = new Core.Schedule("*:*:*");

            var result = schedule.PrevEvent(new DateTime(2019, 05, 01, 11, 01, 13, 0));

            Assert.AreEqual(new DateTime(2019, 05, 01, 11, 01, 11, 0), result);
        }
    }
}
