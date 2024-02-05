using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class MainClicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float clickAnimDuration;
    [SerializeField] private Image clickProgressBarBack;
    [SerializeField] private Image clickProgressFiller;
    private Vector3 _clickerStartScale;
    private Vector3 _progressBarStartPos;
    private Vector3 _cameraStartPos;
    private PlayerData data;
    private RectTransform _rect;

    public delegate void OnFoodCooked();
    public OnFoodCooked onFoodCooked;

    [Inject]
    private void Init(PlayerData data)
    {
        this.data = data;
        _rect = GetComponent<RectTransform>();
        _progressBarStartPos = clickProgressBarBack.rectTransform.position;
        _clickerStartScale = _rect.localScale;
        _cameraStartPos = Camera.main.transform.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickLogic();
        ClickAnimation();
    }

    private void ClickLogic()
    {
        data.currentClickerProgress += data.ClickPower;

        if (data.currentClickerProgress >= data.maxProgressBarClicks)
        {
            data.Money += data.MoneyPerClick * (data.currentClickerProgress / data.maxProgressBarClicks);
            data.currentClickerProgress = data.currentClickerProgress % data.maxProgressBarClicks;
            onFoodCooked?.Invoke();
        }

        clickProgressFiller.DOFillAmount(1 / (float)data.maxProgressBarClicks * data.currentClickerProgress, 0.1f);
    }
    private void ClickAnimation()
    {
        Sequence clickAnim = DOTween.Sequence();
        clickAnim.Insert(0, clickProgressBarBack.rectTransform.DOShakePosition(clickAnimDuration, 3f, 1, 90));
        clickAnim.Insert(0, _rect.DOShakeScale(clickAnimDuration, .1f, 1, 0));
        clickAnim.Insert(0, Camera.main.DOShakePosition(clickAnimDuration, .1f, 1, 0));
        clickAnim.onComplete += () =>
        {
            _rect.DOScale(_clickerStartScale, clickAnimDuration);
            clickProgressBarBack.rectTransform.DOMove(_progressBarStartPos, clickAnimDuration);
            Camera.main.transform.DOMove(_cameraStartPos, clickAnimDuration);
        };
    }
}
