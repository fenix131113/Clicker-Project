using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class HoldButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float needHoldTime;
    private float _inputTime;
    private bool _isMouseInButton;
    private GlobalObjectsContainer container;
    private RectTransform indicatorRect;

    public delegate void OnHoldComplete();
    public OnHoldComplete onHoldComplete;

    [Inject]
    private void Init(GlobalObjectsContainer objectsContainer)
    {
        container = objectsContainer;
        indicatorRect = container.HoldButtonFiller.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if(_isMouseInButton && Input.GetMouseButtonDown(0))
        {
            container.HoldButtonFiller.fillAmount = 0;
            container.HoldButtonFiller.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(indicatorRect.sizeDelta.x, indicatorRect.sizeDelta.y, 0);
            container.HoldButtonFiller.gameObject.SetActive(true);
        }

        if (_isMouseInButton && Input.GetMouseButton(0))
        {
            _inputTime += Time.deltaTime;
            container.HoldButtonFiller.fillAmount = _inputTime / needHoldTime;

            container.HoldButtonFiller.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(indicatorRect.sizeDelta.x / 2, indicatorRect.sizeDelta.y / 2, 0);

            if (container.HoldButtonFiller.fillAmount == 1)
            {
                onHoldComplete?.Invoke();
                container.HoldButtonFiller.gameObject.SetActive(false);
                _inputTime = 0;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _inputTime = 0;
            container.HoldButtonFiller.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseInButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseInButton = false;
    }
}
