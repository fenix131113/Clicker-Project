
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


        public static string GetRuDayType(DayType dayType)
        {
            switch (dayType)
            {
                case DayType.Monday:
                    return "Понедельник";
                case DayType.Tuesday:
                    return "Вторник";
                case DayType.Wednesday:
                    return "Среда";
                case DayType.Thursday:
                    return "Четверг";
                case DayType.Friday:
                    return "Пятница";
                case DayType.Saturday:
                    return "Суббота";
                case DayType.Sunday:
                    return "Воскресенье";
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

        public static void SetDay(int day)
        {
            _day = day;
        }
    }
}