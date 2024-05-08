namespace Clicker.Core.Time
{
    public static class CalendarManager
    {
        private static int _day = 1;
        private static DayType _dayType = DayType.Monday;
        private static TimeManager _timeManager;

        public static TimeManager timeManager => _timeManager;
        public static int Day => _day;
        public static DayType GetDayType => _dayType;

        #region Events
        public delegate void OnNewDay(int dayNum, DayType dayType);
        public static OnNewDay onNewDay;
        #endregion

        public static void Init(TimeManager timeManager)
        {
            _timeManager = timeManager;
        }
        public static string GetRuDayType(DayType dayType)
        {
            switch (dayType)
            {
                case DayType.Monday:
                    return "Ïîíåäåëüíèê";
                case DayType.Tuesday:
                    return "Âòîğíèê";
                case DayType.Wednesday:
                    return "Ñğåäà";
                case DayType.Thursday:
                    return "×åòâåğã";
                case DayType.Friday:
                    return "Ïÿòíèöà";
                case DayType.Saturday:
                    return "Ñóááîòà";
                case DayType.Sunday:
                    return "Âîñêğåñåíüå";
            }
            return null;
        }
        public static void SetNextDay()
        {
            _day++;

            if ((int)GetDayType == 6)
                _dayType = DayType.Monday;
            else
                _dayType = (DayType)(int)GetDayType + 1;
            onNewDay?.Invoke(Day, GetDayType);
        }

        public static void SetDay(int day) => _day = day;
    }
}