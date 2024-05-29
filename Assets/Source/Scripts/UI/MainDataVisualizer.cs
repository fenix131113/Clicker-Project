using Clicker.Core.Time;
using TMPro;
using UnityEngine;
using Zenject;

public class MainDataVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text dateAndTimeText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text skillPointsText;

    private TimeManager _timeManager;
    private PlayerData _data;
    private CalendarManager _calendarManager;

    [Inject]
    private void Init(PlayerData data, TimeManager timeManager, CalendarManager calendarManager)
    {
        _timeManager = timeManager;
        TimeManager.onNewMinute += EveryMinuteAction;

        _calendarManager = calendarManager;
        _data = data;
    }

    private void EveryMinuteAction(int minute)
    {
        dateAndTimeText.text = $"День {_calendarManager.Day} | {_calendarManager.GetRuDayType(_calendarManager.GetDayType)}\n{_timeManager.GetFormattedHours()}:{_timeManager.GetFormattedMinutes()}";
    }

    private void Update()
    {
        moneyText.text = $"Деньги: {_data.Money}$";
        Debug.Log($"Деньги: {_data.Money}$");
        skillPointsText.text = $"Очки навыков: {_data.SkillPoints}";
    }
}
