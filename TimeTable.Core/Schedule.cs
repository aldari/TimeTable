using System;
using System.Collections.Generic;

namespace TimeTable.Core
{
    // должен быть эффективным и не использовать много памяти даже тогда, когда в расписании задано много значений. 
    // Например много значений с шагом в одну миллисекунду.

    /// <summary>
    /// Класс для задания и расчета времени по расписанию.
    /// </summary>
    public class Schedule
    {
        private readonly ISchedulePart _year;
        private readonly ISchedulePart _month;
        private readonly ISchedulePart _day;
        private readonly ISchedulePart _hour;
        private readonly ISchedulePart _minute;
        private readonly ISchedulePart _second;
        private readonly ISchedulePart _millisecond;
        private readonly IReadOnlyList<bool> dayOfWeek;

        /// <summary>
        /// Создает пустой экземпляр, который будет соответствовать
        /// расписанию типа "*.*.* * *:*:*.*" (раз в 1 мс).
        /// </summary>
        public Schedule(): this("*.*.* * *:*:*.*")
        {
        }

        /// <summary>
        /// Создает экземпляр из строки с представлением расписания.
        /// </summary>
        /// <param name="scheduleString">Строка расписания.
        /// Формат строки:
        ///     [yyyy.MM.dd[ w]] HH:mm:ss[.fff]
        ///     yyyy.MM.dd w HH:mm:ss.fff
        ///     yyyy.MM.dd HH:mm:ss.fff
        ///     HH:mm:ss.fff
        ///     yyyy.MM.dd w HH:mm:ss
        ///     yyyy.MM.dd HH:mm:ss
        ///     HH:mm:ss
        /// Где yyyy - год (2000-2100)
        ///     MM - месяц (1-12)
        ///     dd - число месяца (1-31 или 32). 32 означает последнее число месяца
        ///     w - день недели (0-6). 0 - воскресенье, 6 - суббота
        ///     HH - часы (0-23)
        ///     mm - минуты (0-59)
        ///     ss - секунды (0-59)
        ///     fff - миллисекунды (0-999). Если не указаны, то 0
        /// Каждую часть даты/времени можно задавать в виде списков и диапазонов.
        /// Например:
        ///     1,2,3-5,10-20/3
        ///     означает список 1,2,3,4,5,10,13,16,19
        /// Дробью задается шаг в списке.
        /// Звездочка означает любое возможное значение.
        /// Например (для часов):
        ///     */4
        ///     означает 0,4,8,12,16,20
        /// Вместо списка чисел месяца можно указать 32. Это означает последнее
        /// число любого месяца.
        /// Пример:
        ///     *.9.*/2 1-5 10:00:00.000
        ///     означает 10:00 во все дни с пн. по пт. по нечетным числам в сентябре
        ///     *:00:00
        ///     означает начало любого часа
        ///     *.*.01 01:30:00
        ///     означает 01:30 по первым числам каждого месяца
        /// </param>
        public Schedule(string scheduleString)
        {
            if (String.IsNullOrEmpty(scheduleString))
                throw new FormatException("empty string");            
            string[] scheduleSplit = scheduleString.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (scheduleSplit.Length > 3)
                throw new FormatException("too many space separated segments");

            var factory = new Factory();
            string[] list1, list2;
            switch(scheduleSplit.Length)
            {
                case 1:
                    list1 = ParseTime(scheduleString);
                    _year = new MaskInterval("*");
                    _month = new MaskInterval("*");
                    _day = new MaskInterval("*");
                    _hour = factory.Create(list1[0]);
                    _minute = factory.Create(list1[1]);
                    _second = factory.Create(list1[2]);
                    _millisecond = factory.Create(list1[3]);
                    dayOfWeek = ParseDayOfWeek(factory.Create("*"));
                    break;
                case 2:
                    list1 = ParseDate(scheduleSplit[0]);
                    _year = factory.Create(list1[0]);
                    _month = factory.Create(list1[1]);
                    _day = factory.Create(list1[2]);
                    list2 = ParseTime(scheduleSplit[1]);
                    _hour = factory.Create(list2[0]);
                    _minute = factory.Create(list2[1]);
                    _second = factory.Create(list2[2]);
                    _millisecond = factory.Create(list2[3]);
                    dayOfWeek = ParseDayOfWeek(factory.Create("*"));
                    break;
                case 3:
                    list1 = ParseDate(scheduleSplit[0]);
                    _year = factory.Create(list1[0]);
                    _month = factory.Create(list1[1]);
                    _day = factory.Create(list1[2]);
                    
                    list2 = ParseTime(scheduleSplit[2]);
                    _hour = factory.Create(list2[0]);
                    _minute = factory.Create(list2[1]);
                    _second = factory.Create(list2[2]);
                    _millisecond = factory.Create(list2[3]);

                    dayOfWeek = ParseDayOfWeek(factory.Create(scheduleSplit[1]));
                    break;
            }
        }

        private string[] ParseTime(string input)
        {
            var timeSplit = input.Split(new char[] {':'});
            if (timeSplit.Length != 3)
                throw new FormatException("wrong quantity of segments in time separated by ':'");
            var hour = timeSplit[0];
            var minute = timeSplit[1];
            var secondsSplit = timeSplit[2].Split(new char[] {'.'});
            var second = secondsSplit[0];
            var milisecond = (secondsSplit.Length == 2) ? secondsSplit[1] : "0";
            return new string[] { hour, minute, second, milisecond };
        }

        private string[] ParseDate(string input)
        {
            var dateSplit = input.Split(new char[] { '.' });
            if (dateSplit.Length != 3)
                throw new FormatException("wrong quantity of segments in date separated by '.'");
            var year = dateSplit[0];
            var month = dateSplit[1];
            var day = dateSplit[2];
            return new string[] { year, month, day};
        }

        private IReadOnlyList<bool> ParseDayOfWeek(ISchedulePart schedulePart)
        {
            var vals = new bool[7];
            for (int i = 0; i < 7; i++)
                vals[i] = schedulePart.HasInSchedule(i);
            return Array.AsReadOnly(vals);
        }

        /// <summary>
        /// Возвращает следующий ближайший к заданному времени момент в расписании или
        /// само заданное время, если оно есть в расписании.
        /// </summary>
        /// <param name="t1">Заданное время</param>
        /// <returns>Ближайший момент времени в расписании</returns>
        public DateTime NearestEvent(DateTime t1)
        {
            if (HasInSchedule(t1))
                return t1;
            else
                return NextEvent(t1);
        }

        /// <summary>
        /// Возвращает предыдущий ближайший к заданному времени момент в расписании или
        /// само заданное время, если оно есть в расписании.
        /// </summary>
        /// <param name="t1">Заданное время</param>
        /// <returns>Ближайший момент времени в расписании</returns>
        public DateTime NearestPrevEvent(DateTime t1)
        {
            if (HasInSchedule(t1))
                return t1;
            else
                return PrevEvent(t1);
        }

        /// <summary>
        /// Возвращает следующий момент времени в расписании.
        /// </summary>
        /// <param name="t1">Время, от которого нужно отступить</param>
        /// <returns>Следующий момент времени в расписании</returns>
        public DateTime NextEvent(DateTime t1)
        {
            var date = new IncreasingDateTime(t1, new[] { _year, _month, _day, _hour, _minute, _second, _millisecond }, dayOfWeek);

            if (date.EqualDateTime(t1))
            {
                date.Kick();
            }
            return date.ToDateTime();
        }

        /// <summary>
        /// Возвращает предыдущий момент времени в расписании.
        /// </summary>
        /// <param name="t1">Время, от которого нужно отступить</param>
        /// <returns>Предыдущий момент времени в расписании</returns>
        public DateTime PrevEvent(DateTime t1)
        {
            return t1.AddMilliseconds(-1);
        }

        private bool HasInSchedule(DateTime dt)
        {
            return _year.HasInSchedule(dt.Year) && _month.HasInSchedule(dt.Month) && _day.HasInSchedule(dt.Day)
                && _hour.HasInSchedule(dt.Hour) && _minute.HasInSchedule(dt.Minute) && _second.HasInSchedule(dt.Second) 
                && _millisecond.HasInSchedule(dt.Millisecond);
        }
    }
}