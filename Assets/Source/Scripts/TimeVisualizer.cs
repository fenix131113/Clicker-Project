using Clicker.Core.Time;
using TMPro;
using UnityEngine;
using Zenject;

public class TimeVisualizer : MonoBehaviour
{
    private TMP_Text textToDisplay;
    private TimeManager _timeManager;


    private void Start()
    {
        textToDisplay = GetComponent<TMP_Text>();
    }

    [Inject]
    private void Init(TimeManager timeManager)
    {
        _timeManager = timeManager;
    }

    private void Update()
    {
        TimeManager.onNewMinute += (int minute) => textToDisplay.text = $"Δενό {CalendarManager.Day} | {_timeManager.GetFormattedHours()}:{_timeManager.GetFormattedMinutes()}\n{CalendarManager.GetDayType}";
    }
}
