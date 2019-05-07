namespace TimeTable.Core
{
    internal interface ISchedulePart
    {
        // нужно именно быстрое сравние с int поэтому не переопределяю Equals
        bool HasInSchedule(int v);
        int NearestValue(int v);
        int NearestPrevValue(int v);
        int NextValue(int v);
        int PrevValue(int v);
    }
}
