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

    [Inject]
    private void Init(PlayerData data, TimeManager timeManager)
    {
        _timeManager = timeManager;
        TimeManager.onNewMinute += EveryMinuteAction;

        _data = data;
    }

    private void EveryMinuteAction(int minute)
    {
        dateAndTimeText.text = $"���� {CalendarManager.Day} | {CalendarManager.GetRuDayType(CalendarManager.GetDayType)}\n{_timeManager.GetFormattedHours()}:{_timeManager.GetFormattedMinutes()}";
    }

    private void Update()
    {
        moneyText.text = $"������: {_data.Money}$";
        skillPointsText.text = $"���� �������: {_data.SkillPoints}";
    }
}
