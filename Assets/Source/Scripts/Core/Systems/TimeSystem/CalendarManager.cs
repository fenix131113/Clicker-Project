namespace Clicker.Core.Time
{
    public class CalendarManager
    {
        private int _day = 1;
        private DayType _dayType = DayType.Monday;
        private TimeManager _timeManager;

        public TimeManager timeManager => _timeManager;
        public int Day => _day;
        public DayType GetDayType => _dayType;

        #region Events
        public delegate void OnNewDay(int dayNum, DayType dayType);
        public OnNewDay onNewDay;
        #endregion

        public void Init(TimeManager timeManager)
        {
            _timeManager = timeManager;
        }
        public string GetEuDayType(DayType dayType)
        {
            switch (dayType)
            {
                case DayType.Monday:
                    return "Monday";
                case DayType.Tuesday:
                    return "Tuesday";
                case DayType.Wednesday:
                    return "Wednesday";
                case DayType.Thursday:
                    return "Thursday";
                case DayType.Friday:
                    return "Friday";
                case DayType.Saturday:
                    return "Saturday";
                case DayType.Sunday:
                    return "Sunday";
            }
            return null;
        }
        public void SetNextDay()
        {
            _day++;

            if ((int)GetDayType == 6)
                _dayType = DayType.Monday;
            else
                _dayType = (DayType)(int)GetDayType + 1;
            onNewDay?.Invoke(Day, GetDayType);
        }

        public void SetDay(int day) => _day = day;
    }
}