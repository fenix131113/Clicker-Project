using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SideHighLighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform glowRect;
    [SerializeField] private float xStart;
    [SerializeField] private float xEnd;

    public event Action OnSideClicked;

    private bool _isInClickZone;


    private void Start()
    {
        OnSideClicked += () => _isInClickZone = false;
    }
    private void Update()
    {
        if (_isInClickZone && Input.GetMouseButtonDown(0))
        {
            OnSideClicked?.Invoke();
            _isInClickZone = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        glowRect.DOMoveX(xEnd, 0.3f);
        _isInClickZone = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        glowRect.DOMoveX(xStart, 0.3f);
        _isInClickZone = false;
    }
}