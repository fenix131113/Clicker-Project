using Clicker.Core.Earnings;
using Clicker.Core.Time;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class MainClicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject[] foodObjects;
    [SerializeField] private float clickAnimDuration;
    [SerializeField] private Image clickProgressBarBack;
    [SerializeField] private Image clickProgressFiller;
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject moneyEarnPrefab;
    private Vector3 _clickerStartScale;
    private Vector3 _progressBarStartPos;
    private Vector3 _cameraStartPos;
    private PlayerData data;
    private EarningsManager earningsManager;
    private RectTransform _rect;

    public delegate void OnFoodCooked(int moneyEarned);
    public OnFoodCooked onFoodCooked;

    [Inject]
    private void Init(PlayerData data, EarningsManager earningsManager)
    {
        this.data = data;
        this.earningsManager = earningsManager;
        _rect = GetComponent<RectTransform>();
        _progressBarStartPos = clickProgressBarBack.rectTransform.position;
        _clickerStartScale = _rect.localScale;
        _cameraStartPos = Camera.main.transform.position;
        onFoodCooked += FoodCooked;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickLogic();
        ClickAnimation();
    }

    private void ClickLogic()
    {
        data.SetCurrentClickerProgress(data.CurrentClickerProgress + data.ClickPower);

        if (data.CurrentClickerProgress >= data.MaxProgressBarClicks)
        {
            data.Money += data.MoneyPerFood * (data.CurrentClickerProgress / data.MaxProgressBarClicks);
            onFoodCooked?.Invoke(data.MoneyPerFood * (data.CurrentClickerProgress / data.MaxProgressBarClicks));
            data.SetCurrentClickerProgress(data.CurrentClickerProgress % data.MaxProgressBarClicks);
        }

        clickProgressFiller.DOFillAmount(1 / (float)data.MaxProgressBarClicks * data.CurrentClickerProgress, 0.1f);
    }

    private void FoodCooked(int earnedMoney)
    {
        CreateEarnedMoneyLabel(earnedMoney);
        earningsManager.AddOrUpdateHistoryEntry(CalendarManager.Day, "Кликер", earnedMoney);

        List<GameObject> unlockedFoodObjects = new();

        for(int i = 0; i < data.UnlockedFood.Count; i++)
        {
            if (data.UnlockedFood.ElementAt(i))
                unlockedFoodObjects.Add(foodObjects[i]);
        }
        foreach (GameObject obj in unlockedFoodObjects)
            obj.SetActive(false);
        unlockedFoodObjects[Random.Range(0, unlockedFoodObjects.Count)].SetActive(true);
    }
    private void CreateEarnedMoneyLabel(int earnedMoney)
    {
        GameObject label = Instantiate(moneyEarnPrefab, canvas);
        label.GetComponent<Text>().text = $"+{earnedMoney}$";
        label.GetComponent<RectTransform>().position = Input.mousePosition;
        label.GetComponent<RectTransform>().DOMove(Input.mousePosition + new Vector3(0, 300), 1f);
        label.GetComponent<Text>().DOFade(0f, 1f).onComplete += () => Destroy(label);
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
