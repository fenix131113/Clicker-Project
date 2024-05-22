using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] private UnityEvent _onClick;
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _clickAnimDuration;
    [SerializeField] private float _clickAnimScaleModifire;
    [SerializeField] private Ease _clickAnimEase;
    [SerializeField] private AudioClip _hoverOnSound;
    [SerializeField] private AudioClip _clickSound;

    private GlobalObjectsContainer _objectsContainer;
    private AudioController _audioController;
    private Vector3 _standartLossyScale;

    [Inject]
    private void Init(GlobalObjectsContainer objectsContainer, AudioController audioController)
    {
        _standartLossyScale = _rect.lossyScale;

        _objectsContainer = objectsContainer;
        _audioController = audioController;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonAnimation();
        _onClick?.Invoke();

        if (_clickSound)
            _audioController.PlaySound(_clickSound);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_hoverOnSound)
            _audioController.PlaySound(_hoverOnSound);
    }

    private void ButtonAnimation()
    {
        _rect.DOScale(_rect.lossyScale * _clickAnimScaleModifire, _clickAnimDuration).SetEase(_clickAnimEase).onComplete +=
            () => _rect.DOScale(_standartLossyScale, _clickAnimDuration);
    }

}
