using UnityEngine;
using Zenject;

namespace Clicker.Core.Time
{
    public class TimeManager : MonoBehaviour
    {
        private int _hour;
        private int _minutes;
        private float _seconds;
        private bool _isTimePaused;
        private CalendarManager _calendarManager;

        public int Hour => _hour;
        public int Minutes => _minutes;
        public float Seconds => Mathf.Floor(_seconds);
        public bool IsTimePaused
        {
            get { return _isTimePaused; }
            set { _isTimePaused = value; }
        }

        public delegate void OnNewHour(int hour);
        public static event OnNewHour onNewHour;

        public delegate void OnNewMinute(int minute);
        public static event OnNewMinute onNewMinute;


        // Real time multiplier
        public const int TimeMultiplayer = 2000;


        public TimeManager()
        {
            onNewMinute = null;
            onNewHour = null;
        }
        [Inject]
        private void Init(CalendarManager calendarManager)
        {
            _calendarManager = calendarManager;
            _calendarManager.Init(this);
        }

        private void Update()
        {
            if (!IsTimePaused)
                _seconds += UnityEngine.Time.deltaTime * TimeMultiplayer;
            #region Time Conditions
            if (Mathf.Floor(_seconds) >= 60)
            {
                _seconds = 0;
                _minutes++;
                onNewMinute?.Invoke(_minutes);
            }
            if (_minutes >= 60)
            {
                _minutes = 0;
                _hour++;
                if (_hour != 24)
                    onNewHour?.Invoke(_hour);
            }
            if (_hour == 24)
            {
                _hour = 0;
                onNewHour?.Invoke(_hour);
                _calendarManager.SetNextDay();
            }
            #endregion
        }

        #region Formatted Time
        /// <returns>
        /// Time Formatted Like "XX:XX:XX"
        /// </returns>
        public string GetFormattedTime() { return $"{GetFormattedHours()}:{GetFormattedMinutes()}:{GetFormattedSeconds()}"; }

        /// <returns>
        /// Hours Formatted Like "XX"
        /// </returns>
        public string GetFormattedHours() { return Hour < 10 ? $"0{Hour}" : Hour.ToString(); }

        /// <returns>
        /// Minutes Formatted Like "XX"
        /// </returns>
        public string GetFormattedMinutes() { return Minutes < 10 ? $"0{Minutes}" : Minutes.ToString(); }

        /// <returns>
        /// Seconds Formatted Like "XX"
        /// </returns>
        public string GetFormattedSeconds() { return Seconds < 10 ? $"0{Seconds}" : Seconds.ToString(); }

        /// <returns>
        /// Time Formatted Like "XX:XX:XX"
        /// </returns>
        public string GetFormattedTime(int hours, int minutes, int seconds) { return $"{GetFormattedHours(hours)}:{GetFormattedMinutes(minutes)}:{GetFormattedSeconds(seconds)}"; }

        /// <returns>
        /// Hours Formatted Like "XX"
        /// </returns>
        public string GetFormattedHours(int hours) { return hours < 10 ? $"0{hours}" : hours.ToString(); }

        /// <returns>
        /// Minutes Formatted Like "XX"
        /// </returns>
        public string GetFormattedMinutes(int minutes) { return minutes < 10 ? $"0{minutes}" : minutes.ToString(); }

        /// <returns>
        /// Seconds Formatted Like "XX"
        /// </returns>
        public string GetFormattedSeconds(int seconds) { return seconds < 10 ? $"0{seconds}" : seconds.ToString(); }
        #endregion
    }
}