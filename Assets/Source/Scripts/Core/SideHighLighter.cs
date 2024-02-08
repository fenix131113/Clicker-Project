using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SideHighLighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform glowRect;
    [SerializeField] private float xStart;
    [SerializeField] private float xEnd;

    public void OnPointerEnter(PointerEventData eventData)
    {
        glowRect.DOMoveX(xEnd, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        glowRect.DOMoveX(xStart, 0.3f);
    }
}