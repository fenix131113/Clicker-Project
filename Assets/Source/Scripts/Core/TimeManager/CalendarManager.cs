namespace Clicker.Core.Time
{
    public static class CalendarManager
    {
        private static int _day = 1;
        public static int Day => _day;

        private static DayType _dayType = DayType.Monday;
        public static DayType GetDayType => _dayType;

        #region Events
        public delegate void OnNewDay(int dayNum, DayType dayType);
        public static OnNewDay onNewDay;
        #endregion


        public static void SetNextDay()
        {
            _day++;
            if ((int)GetDayType == 6)
                _dayType = DayType.Monday;
            else
                _dayType = (DayType)(int)GetDayType + 1;
            onNewDay?.Invoke(Day, GetDayType);
        }
    }
}