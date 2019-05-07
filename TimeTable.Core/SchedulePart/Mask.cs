using System;

namespace TimeTable.Core
{
    internal class Mask : ISchedulePart
    {
        private readonly int min;
        private readonly int max;

        public Mask(string init)
        {
            if (init != "*")
                throw new FormatException();
        }

        public Mask(string init, TimerTypes type) : this(init)
        {
            switch (type)
            {
                case TimerTypes.Year:
                    // не опечатка
                    max = Int32.MaxValue;
                    min = 0;
                    break;
                case TimerTypes.Month:
                    max = 12;
                    min = 1;
                    break;
                case TimerTypes.Day:
                    max = 31;
                    min = 1;
                    break;
                case TimerTypes.DayOfWeek:
                    max = 6;
                    min = 0;
                    break;
                case TimerTypes.Hour:
                    max = 23;
                    min = 0;
                    break;
                case TimerTypes.Minute:
                    max = 59;
                    min = 0;
                    break;
                case TimerTypes.Second:
                    max = 59;
                    min = 0;
                    break;
                case TimerTypes.Millisecond:
                    max = 999;
                    min = 0;
                    break;
            }
        }

        public bool HasInSchedule(int v)
        {
            // здесь проверка интервала не нужна так, как объект DateTime всегда корректен
            return true;
        }

        public int NearestValue(int v)
        {
            return v;
        }

        public int NearestPrevValue(int v)
        {
            return v;
        }

        public int NextValue(int v)
        {
            int result = v + 1;
            return (result > max) ? min : result;
        }

        public int PrevValue(int v)
        {
            int result = v - 1;
            return (v == min) ? max : result;
        }
    }
}
