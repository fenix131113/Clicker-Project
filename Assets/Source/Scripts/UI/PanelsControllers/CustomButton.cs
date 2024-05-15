using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public class CustomButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent _onClick;
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _clickAnimDuration;
    [SerializeField] private float _clickAnimScaleModifire;
    [SerializeField] private Ease _clickAnimEase;

    private GlobalObjectsContainer _objectsContainer;
    private Vector3 standartLossyScale;

    [Inject]
    private void Init(GlobalObjectsContainer objectsContainer)
    {
        standartLossyScale = _rect.lossyScale;

        _objectsContainer = objectsContainer;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonAnimation();
        _onClick?.Invoke();
    }

    private void ButtonAnimation()
    {
        _rect.DOScale(_rect.lossyScale * _clickAnimScaleModifire, _clickAnimDuration).SetEase(_clickAnimEase).onComplete +=
            () => _rect.DOScale(standartLossyScale, _clickAnimDuration);
    }
}
