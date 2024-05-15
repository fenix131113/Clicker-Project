using Clicker.Core.Earnings;
using Clicker.Core.Tournament;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Clicker.Core.Time
{
    public class CalendarEventsController : MonoBehaviour
    {
        [SerializeField] private CalendarCell[] _calendarCells = new CalendarCell[31];
        [SerializeField] private Transform _dayEventsContentTransform;
        [SerializeField] private GameObject _dayEventPrefab;
        [SerializeField] private GameObject _emptyDayEventsTextObject;
        [SerializeField] private TMP_Text _dayInfoNumText;

        private List<CalendarEvent>[] _daysEvents = new List<CalendarEvent>[31];
        private List<CalendarEvent> _allEvents = new();
        private int _selectedDayIndex = -1;

        private TimeManager _timeManager;
        private TournamentManager _tournaments;
        private MafiaManager _mafiaManager;
        private EarningsManager _earningsManager;
        private PlayerData _data;

        public int SelectedDayIndex => _selectedDayIndex;

        [Inject]
        private void Init(TimeManager timeManager, TournamentManager tournaments, MafiaManager mafiaManager, EarningsManager earningsManager, PlayerData data)
        {
            _timeManager = timeManager;
            _tournaments = tournaments;
            _mafiaManager = mafiaManager;
            _earningsManager = earningsManager;
            _data = data;
        }

        private void Awake()
        {
            ReloadCalendar();

            TimeManager.onNewMinute += CheckEventsData;
            CalendarManager.onNewDay += OnNewDay;
            // DELETE IN FUTURE
            SelectDay(0);
        }

        private void OnNewDay(int dayNum, DayType dayType)
        {
            // Update calendar every new month
            if (dayNum % 31 == 1)
                GenerateEvents();

            // Clear all events complete status every day
            foreach (CalendarEvent c in _allEvents)
                c.EventComplete = false;
        }

        // Check every event data and compare with current data
        private void CheckEventsData(int currentMinute)
        {
            foreach (CalendarEvent dayEvent in _daysEvents[CalendarManager.Day > 31 ? CalendarManager.Day % 31 : CalendarManager.Day - 1])
            {
                if (!dayEvent.EventComplete && dayEvent.Hour == _timeManager.Hour && currentMinute == 1)
                    dayEvent.EventAction();
            }
        }

        // Call this method FIRST OF ALL (for load all events)
        // Also reset all events collection and daysEvents array and generate again
        // Regenerate all events need to apply changes in events that contains dynamic data
        private void ReloadCalendar()
        {
            // Reset daysEvents array
            ResetDaysEvents();
            // And all events collection to regenerate dynamic data
            _allEvents = new()
            {
                new CookTournamentCalendarEvent(_tournaments.TournamentPeriod, 15, _tournaments),
                new MafiaCalendarEvent(_mafiaManager.MafiaVisitPeriod, 15, _mafiaManager),
                new UtilitiesPayEvent(7, 7, _data),
                new ConsumablePaymentEvent(_data.PassiveMoneyController.ConsumablesPayPeriod, 7, _data),
            };


            GenerateEvents();
        }

        // Need to call every month
        // Place event in days by their event period
        private void GenerateEvents()
        {
            ResetDaysEvents();

            int lowestMonthDay = CalendarManager.Day > 31 ? CalendarManager.Day / 31 * 31 + 1 : 1;
            // Place events in "days events array" by they index
            foreach (CalendarEvent e in _allEvents)
            {
                for (int i = lowestMonthDay; i <= lowestMonthDay + 30; i++)
                {
                    if (i % e.EventPeriod == 0)
                    {
                        if (i != lowestMonthDay + 30)
                            _daysEvents[i % 31 - 1].Add(e);
                        else
                            _daysEvents[^1].Add(e);
                    }
                }
            }
            foreach (CalendarCell cell in _calendarCells)
                CheckEventCellIndicator(cell);
        }

        // If event exsit in this day, indicator will turn on
        private void CheckEventCellIndicator(CalendarCell cell)
        {
            if (GetDayEventsByCell(cell).Count > 0)
                cell.EventIndicator.gameObject.SetActive(true);
            else
                cell.EventIndicator.gameObject.SetActive(false);
        }

        // Reset all events collection and daysEvents array
        private void ResetDaysEvents()
        {
            for (int i = 0; i < _daysEvents.Length; i++)
                _daysEvents[i] = new();
        }

        // Add event to specified day
        private void AddDayEvent(int day, CalendarEvent dayEvent)
        {
            _daysEvents[day - 1].Add(dayEvent);
        }

        // Destory all childs of panel with events
        private void ClearDayEventsObjects()
        {
            if (_dayEventsContentTransform.childCount > 0)
                for (int i = 0; i < _dayEventsContentTransform.childCount; i++)
                    Destroy(_dayEventsContentTransform.GetChild(i).gameObject);
        }

        private void DrawDayEventsInfoByIndex(int dayIndex)
        {
            IReadOnlyCollection<CalendarEvent> dayEvents = GetDayEventsByCell(GetCalendarCellByIndex(dayIndex));

            _dayInfoNumText.text = "Δενό " + (dayIndex + 1).ToString();

            if (dayEvents.Count == 0)
            {
                _emptyDayEventsTextObject.SetActive(true);
                return;
            }

            _emptyDayEventsTextObject.SetActive(false);
            foreach (CalendarEvent e in dayEvents)
                Instantiate(_dayEventPrefab, _dayEventsContentTransform).GetComponent<DayEventCell>()
                    .SetEventName(e.EventName)
                    .SetEventDescription(e.Description)
                    .SetEventTime($"{_timeManager.GetFormattedHours(e.Hour)}:00");
        }

        private CalendarCell GetCalendarCellByIndex(int dayIndex) => _calendarCells[dayIndex];
        public IReadOnlyCollection<CalendarEvent> GetDayEventsByCell(CalendarCell cell) => _daysEvents[cell.CellDayNum - 1];

        public void SelectDay(int dayIndex)
        {
            if (_selectedDayIndex == dayIndex)
                return;

            ClearDayEventsObjects();
            _selectedDayIndex = dayIndex;
            DrawDayEventsInfoByIndex(_selectedDayIndex);
        }
    }
}