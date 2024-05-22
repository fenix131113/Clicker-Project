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
        public static OnNewDay onNewDay;
        #endregion

        public void Init(TimeManager timeManager)
        {
            onNewDay = null;
            _timeManager = timeManager;
        }
        public string GetRuDayType(DayType dayType)
        {
            switch (dayType)
            {
                case DayType.Monday:
                    return "�����������";
                case DayType.Tuesday:
                    return "�������";
                case DayType.Wednesday:
                    return "�����";
                case DayType.Thursday:
                    return "�������";
                case DayType.Friday:
                    return "�������";
                case DayType.Saturday:
                    return "�������";
                case DayType.Sunday:
                    return "�����������";
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