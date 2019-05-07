using System;

namespace TimeTable.Core
{
    internal class MaskInterval : ISchedulePart
    {
        private readonly int step;
        private readonly int min;
        private readonly int max;

        // Obsolete
        public MaskInterval(string init)
        {
            var values = init.Split(new char[] { '/' });
            if (values.Length == 0 || values.Length > 2 || values[0] != "*")
                throw new FormatException();
            if (values.Length == 1)
            {
                step = 1;
            }
            else
            {
                step = Int32.Parse(values[1]);
            }
        }

        public MaskInterval(string init, TimerTypes type) : this(init)
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
            if (step == 1)
                return true;
            else
                return v % step == 0;
        }

        public int NearestValue(int v)
        {
            int result = step * ((v - min + step - 1) / step) + min;
            return (result > max) ? min : result;
        }

        public int NearestPrevValue(int v)
        {
            int result = step * ((v - min) / step) + min;
            return result;
        }

        public int NextValue(int v)
        {
            int result = step * ((v - min + step) / step) + min;
            return (result > max) ? min : result;
        }

        public int PrevValue(int v)
        {
            int result = step * ((v -min - 1) / step) + min;
            return (v == min) ? step * (max / step) + min : result;
        }
    }
}
