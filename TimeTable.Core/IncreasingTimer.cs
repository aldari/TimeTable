namespace TimeTable.Core
{
    public delegate void Callback();

    internal class IncreasingTimer
    {
        private readonly ISchedulePart _schedulePart;
        private readonly Callback _callback;
        private int _value;

        public int Value()
        {
            return _value;
        }

        public void Increment()
        {
            int nextValue = _schedulePart.NextValue(_value);
            if (nextValue < _value)
                _callback();
            _value = nextValue;
        }

        public IncreasingTimer(ISchedulePart schedulePart, int initialValue, Callback callback)
        {
            _schedulePart = schedulePart;
            _callback = callback;
            _value = schedulePart.NearestValue(initialValue);
        }
    }
}
