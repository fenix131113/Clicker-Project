using Clicker.Core.Earnings;
using Clicker.Core.Time;
using TMPro;
using UnityEngine;
using Zenject;

public class MainDataVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text dateAndTimeText;
    [SerializeField] private TMP_Text resourcesInfoText;
    [SerializeField] private TMP_Text shortInfoText;

    private TimeManager _timeManager;
    private PlayerData _data;
    private EarningsManager _earningsManager;

    [Inject]
    private void Init(PlayerData data, TimeManager timeManager, EarningsManager earningManager)
    {
        _timeManager = timeManager;
        TimeManager.onNewMinute += EveryMinuteAction;

        _earningsManager = earningManager;

        _data = data;
    }

    private void EveryMinuteAction(int minute)
    {
        TimeManager.onNewMinute += (int minute) => dateAndTimeText.text = $"���� {CalendarManager.Day} | {CalendarManager.GetRuDayType(CalendarManager.GetDayType)}\n{_timeManager.GetFormattedHours()}:{_timeManager.GetFormattedMinutes()}";
    }

    private void Update()
    {
        resourcesInfoText.text = $"������: {_data.Money}$\n���� �������: {_data.SkillPoints}";
        shortInfoText.text = $"�� ����: {_earningsManager.GetProfitForDay(CalendarManager.Day)}$";
    }
}
