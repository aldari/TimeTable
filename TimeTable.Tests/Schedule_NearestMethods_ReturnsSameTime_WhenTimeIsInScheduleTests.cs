using NUnit.Framework;
using System;

namespace TimeTable.Tests
{
    // все эти тесты для NearestEvent справедливы и для NearestPrevEvent пока он используют IfHas метод
    [TestFixture]
    public class Schedule_NearestMethods_ReturnsSameTime_WhenTimeIsInScheduleTests
    {
        [Test]
        public void Test1_1()
        {
            var schedule = new Core.Schedule("11:10:20");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 0);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test1_2()
        {
            var schedule = new Core.Schedule("*:*:*");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 0);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test1_3()
        {
            var schedule = new Core.Schedule("*/11:*/10:*/10");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 0);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test1_4()
        {
            var schedule = new Core.Schedule("2,4,5,10,11,20:2,4,5,10,11,20:2,4,5,10,11,20");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 0);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test1_5()
        {
            var schedule = new Core.Schedule("2,3-5,11-23/11:2,3-5,10-20/2:2,3-5,10-20/2");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 0);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test2_1()
        {
            var schedule = new Core.Schedule("11:10:20.444");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 444);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test2_2()
        {
            var schedule = new Core.Schedule("*:*:*.*");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 444);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test2_3()
        {
            var schedule = new Core.Schedule("*/11:*/10:*/10.*/111");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 444);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test2_4()
        {
            var schedule = new Core.Schedule("2,4,5,10,11,20:2,4,5,10,11,20:2,4,5,10,11,20.100,111,200,300,444,555");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 444);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void Test2_5()
        {
            var schedule = new Core.Schedule("2,3-5,11-23/11:2,3-5,10-20/2:2,3-5,10-20/2.");
            var input = new DateTime(2019, 05, 01, 11, 10, 20, 444);

            var result = schedule.NearestEvent(input);

            Assert.AreEqual(input, result);
        }
    }
}
