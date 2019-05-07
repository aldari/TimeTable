using NUnit.Framework;
using System;
using TimeTable.Core;

namespace TimeTable.Tests
{
    [TestFixture]
    public class IncreasingDateTimeTests
    {
        [Test]
        public void Test1()
        {
            var date = new IncreasingDateTime(new DateTime(2019, 01, 1), 
                new[] {
                    new MaskInterval("*", TimerTypes.Year),
                    new MaskInterval("*", TimerTypes.Month),
                    new MaskInterval("*", TimerTypes.Day) },
                new bool[] { false, true, true, true, true, true, false });
            for (int i = 0; i < 365; i++)
            {
                date.Kick();
                Console.WriteLine(date.ToString());
            }
        }
    }
}
