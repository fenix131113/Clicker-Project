using Clicker.Core.Earnings;
using UnityEngine;
using Zenject;
using TMPro;
using Clicker.Core.Time;

public class Test : MonoBehaviour
{
    public TMP_Text timeText;

    PlayerData data;
    EarningsManager earningsManager;
    TimeManager timeManager;

    [Inject]
    private void Init(PlayerData data, EarningsManager earningsManager, TimeManager timeManager)
    {
        this.data = data;
        this.earningsManager = earningsManager;
        this.timeManager = timeManager;
    }

    void Start()
    {
        TimeManager.onNewMinute += (int minute) => timeText.text = $"Δενό {CalendarManager.Day} | {timeManager.GetFormattedHours()}:{timeManager.GetFormattedMinutes()} | {CalendarManager.GetDayType}";
    }
}
