using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTable.Core
{
    internal class IntervalList : ISchedulePart
    {
        short[] nums;

        public IntervalList(string init)
        {
            var values = init.Split(new char[] { ',' });
            var list = new List<short>();
            for(int i = 0; i < values.Length; i++)
            {
                if (values[i].Contains("-"))
                {
                    short step = 1;
                    var divineSplit = values[i].Split(new char[] { '/' });
                    if (divineSplit.Length == 2)
                    {
                        step = short.Parse(divineSplit[1]);
                    }
                    var hyphenSplit = divineSplit[0].Split(new char[] { '-' });
                    for (short j = short.Parse(hyphenSplit[0]); j <= short.Parse(hyphenSplit[1]); j+=step)
                        list.Add(j);
                    
                }
                else
                {
                    list.Add(short.Parse(values[i]));
                }
            }
            nums = list.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in nums)
            {
                sb.Append(i);
                sb.Append(",");                
            }
            return sb.ToString();
        }

        public bool HasInSchedule(int v)
        {
            return Array.BinarySearch(nums, (short)v) >= 0;
        }

        public int NearestValue(int v)
        {
            var index = Array.BinarySearch(nums, (short)v);
            if (index < 0)
                return nums[~index % nums.Length];
            else
                return nums[index];
        }

        public int NearestPrevValue(int v)
        {
            var index = Array.BinarySearch(nums, (short)v);
            if (index < 0)
                return nums[(~index + nums.Length - 1) % nums.Length];
            else
                return nums[index];
        }

        public int NextValue(int v)
        {
            var index = Array.BinarySearch(nums, (short)v);
            if (index < 0)
                return nums[~index % nums.Length];
            else
                return nums[(index+1) % nums.Length];
        }

        public int PrevValue(int v)
        {
            var index = Array.BinarySearch(nums, (short)v);
            if (index < 0)
                return nums[(~index + nums.Length - 1) % nums.Length];
            else
                return nums[(index + nums.Length - 1) % nums.Length];
        }
    }
}
