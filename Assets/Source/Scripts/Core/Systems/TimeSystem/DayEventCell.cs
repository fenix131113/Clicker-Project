using TMPro;
using UnityEngine;

public class DayEventCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _eventNameText;
    [SerializeField] private TMP_Text _eventDescriptionText;
    [SerializeField] private TMP_Text _eventTimeText;

    public DayEventCell SetEventName(string text)
    {
        _eventNameText.text = text;
        return this;
    }
    public DayEventCell SetEventDescription(string text)
    {
        _eventDescriptionText.text = text;
        return this;
    }

    public DayEventCell SetEventTime(string text)
    {
        _eventTimeText.text = text;
        return this;
    }
}