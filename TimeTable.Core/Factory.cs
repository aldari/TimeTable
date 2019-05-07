namespace TimeTable.Core
{
    internal class Factory
    {
        public ISchedulePart Create(string init)
        {
            if (init.Contains(',') || init.Contains('-'))
                return new IntervalList(init);
            if (init.Contains('*'))
            {
                if (init.Contains('/'))
                    return new MaskInterval(init);
                else
                    return new Mask(init);
            }  
            return new Literal(init);
        }
    }
}
