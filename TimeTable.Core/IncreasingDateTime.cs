using System;
using System.Collections.Generic;

namespace TimeTable.Core
{
    internal class IncreasingDateTime
    {
        private IncreasingTimer Year;
        private IncreasingTimer Month;
        private IncreasingTimer Day;
        private IncreasingTimer Hour;
        private IncreasingTimer Minute;
        private IncreasingTimer Second;
        private IncreasingTimer Millisecond;

        private Callback DateIncrement;
        private readonly IReadOnlyList<bool> _dayOfWeek;

        public IncreasingDateTime(DateTime t, ISchedulePart[] scheduleParts, IReadOnlyList<bool> dayOfWeek)
        {
            // ! update callback init
            Year = new IncreasingTimer(scheduleParts[(int)TimerTypes.Year], t.Year, null);
            Month = new IncreasingTimer(scheduleParts[(int)TimerTypes.Month], t.Month, new Callback(Year.Increment));
            Day = new IncreasingTimer(scheduleParts[(int)TimerTypes.Day], t.Day, new Callback(Month.Increment));
            Hour = new IncreasingTimer(scheduleParts[(int)TimerTypes.Hour], t.Hour, new Callback(Day.Increment));
            Minute = new IncreasingTimer(scheduleParts[(int)TimerTypes.Minute], t.Minute, new Callback(Hour.Increment));
            Second = new IncreasingTimer(scheduleParts[(int)TimerTypes.Second], t.Second, new Callback(Minute.Increment));
            Millisecond = new IncreasingTimer(scheduleParts[(int)TimerTypes.Millisecond], t.Millisecond, new Callback(Second.Increment));

            _dayOfWeek = dayOfWeek;

            DefineDateIncrement(scheduleParts);
            DefineCommonIncrement(scheduleParts);
        }

        void DefineDateIncrement(ISchedulePart[] scheduleParts)
        {
            DateIncrement = new Callback(Day.Increment);
            if (!(scheduleParts[(int)TimerTypes.Day] is Literal))
                return;

            DateIncrement = new Callback(Month.Increment);
            if (!(scheduleParts[(int)TimerTypes.Month] is Literal))
                return;

            DateIncrement = new Callback(Year.Increment);
        }

        void DefineCommonIncrement(ISchedulePart[] scheduleParts)
        {
            DateIncrement = new Callback(Day.Increment);
            if (!(scheduleParts[(int)TimerTypes.Day] is Literal))
                return;

            DateIncrement = new Callback(Month.Increment);
            if (!(scheduleParts[(int)TimerTypes.Month] is Literal))
                return;

            DateIncrement = new Callback(Year.Increment);
        }

        public void Kick()
        {
            Day.Increment();
            while (true)
            {
                while (Day.Value() > DateTime.DaysInMonth(Year.Value(), Month.Value()))
                    DateIncrement();
                if (_dayOfWeek[(int)new DateTime(Year.Value(), Month.Value(), Day.Value()).DayOfWeek])
                    break;
                else
                    DateIncrement();
            }
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year.Value(), Month.Value(), Day.Value());
        }

        public override string ToString()
        {
            return $"{Year.Value()}.{Month.Value().ToString("D2")}.{Day.Value().ToString("D2")}";
        }

        internal bool EqualDateTime(DateTime t1)
        {
            return Year.Value() == t1.Year
                && Month.Value() == t1.Month
                && Day.Value() == t1.Day
                && Hour.Value() == t1.Hour
                && Minute.Value() == t1.Minute
                && Second.Value() == t1.Second
                && Millisecond.Value() == t1.Millisecond;
        }
    }
}
