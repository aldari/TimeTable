using System;

namespace TimeTable.Core
{
    internal class Literal : ISchedulePart
    {
        private readonly int Value;

        public Literal(string init)
        {
            Value = Int32.Parse(init);
        }

        public bool HasInSchedule(int v)
        {
            return v == Value;
        }

        public int NearestValue(int v)
        {
            return Value;
        }

        public int NearestPrevValue(int v)
        {
            return Value;
        }

        public int NextValue(int v)
        {
            return Value;
        }

        public int PrevValue(int v)
        {
            return Value;
        }
    }
}
