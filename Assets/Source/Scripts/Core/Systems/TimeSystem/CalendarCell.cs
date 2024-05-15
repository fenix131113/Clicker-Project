using Clicker.Core.Time;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalendarCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _cellDayNum;
    [SerializeField] private Image _eventIndicator;
    [SerializeField] private GameObject _selectionObj;
    [SerializeField] private CalendarEventsController eventsController;

    public int CellDayNum => _cellDayNum;
    public Image EventIndicator => _eventIndicator;

    public void OnPointerClick(PointerEventData eventData)
    {
        eventsController.SelectDay(_cellDayNum - 1);
    }

    public void ActivateSelection() => _selectionObj.SetActive(true);
    public void DeactivateSelection() => _selectionObj.SetActive(false);
}
