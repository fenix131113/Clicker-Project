using Clicker.Core.Time;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalendarCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public int _cellDayNum;
    [SerializeField] private Image eventIndicator;
    [SerializeField] private CalendarEventsController eventsController;

    public int CellDayNum => _cellDayNum;
    public Image EventIndicator => eventIndicator;

    public void OnPointerClick(PointerEventData eventData)
    {
        eventsController.SelectDay(_cellDayNum - 1);
    }
}
