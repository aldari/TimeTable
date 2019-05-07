using NUnit.Framework;
using System;

namespace TimeTable.Tests
{
    [TestFixture]
    public class EmptyScheduleMethodTests
    {
        private Core.Schedule schedule;

        [SetUp]
        public void Setup()
        {
            schedule = new Core.Schedule();
        }

        [Test]
        public void NearestEvent_Test()
        {
            //string s = "2019.05.01 11:01:12";
            var dt = new DateTime(2019, 05, 01, 11, 01, 12, 0);

            var result = schedule.NearestEvent(dt);

            Assert.AreEqual(dt, result);
        }

        [Test]
        public void NearestPrevEvent_Test()
        {
            //string s = "2019.05.01 11:01:12";
            var dt = new DateTime(2019, 05, 01, 11, 01, 12, 0);

            var result = schedule.NearestPrevEvent(dt);

            Assert.AreEqual(dt, result);
        }

        [Test]
        public void NextEvent_Test()
        {
            //string s = "2019.05.01 11:01:12";
            var dt = new DateTime(2019, 05, 01, 11, 01, 12, 0);

            var result = schedule.NextEvent(dt);

            Assert.AreEqual(new DateTime(2019, 05, 01, 11, 01, 12, 001), result);
        }

        [Test]
        public void PrevEvent_Test()
        {
            //string s = "2019.05.01 11:01:12";
            var dt = new DateTime(2019, 05, 01, 11, 01, 12, 0);

            var result = schedule.PrevEvent(dt);

            Assert.AreEqual(new DateTime(2019, 05, 01, 11, 01, 11, 999), result);
        }
    }
}
