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
    [SerializeField] private Image clickProgressFiller;
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject moneyEarnPrefab;
    [SerializeField] private GameObject craftedFoodPrefab;
    private Vector3 _clickerStartScale;
    private Vector3 _progressBarStartPos;
    private Vector3 _cameraStartPos;
    private PlayerData data;
    private EarningsManager earningsManager;
    private CalendarManager calendarManager;
    private RectTransform _rect;
    private GameObject currentFoodObject;

    public delegate void IntegerParameterClickerEvent(int parameter1);
    public delegate void NoParametersClickerEvent();

    public event IntegerParameterClickerEvent onFoodCookedEarned;
    public event NoParametersClickerEvent onFoodCooked;
    public event NoParametersClickerEvent onClick;

    [Inject]
    private void Init(PlayerData data, EarningsManager earningsManager, CalendarManager calendarManager)
    {
        this.data = data;
        this.earningsManager = earningsManager;
        this.calendarManager = calendarManager;
        _rect = GetComponent<RectTransform>();
        _clickerStartScale = _rect.localScale;
        _cameraStartPos = Camera.main.transform.position;
        onFoodCookedEarned += FoodCooked;
        currentFoodObject = foodObjects.FirstOrDefault();
        onClick += data.IncreaseClicks;
        onFoodCooked += data.IncreaseCookedFood;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();

        ClickLogic();
        ClickAnimation();
    }

    private void ClickLogic()
    {
        data.SetCurrentClickerProgress(data.CurrentClickerProgress + data.ClickPower);

        if (data.CurrentClickerProgress >= data.MaxProgressBarClicks)
        {
            GameObject craftedItem = Instantiate(craftedFoodPrefab, transform.position, Quaternion.identity, transform);
            craftedItem.GetComponent<Image>().sprite = currentFoodObject.GetComponent<Image>().sprite;
            Sequence craftedItemAnims = DOTween.Sequence();
            craftedItemAnims.Insert(0, craftedItem.transform.DOScaleY(0.6f, 0.1f).SetEase(Ease.OutSine));
            craftedItemAnims.Insert(0.1f, craftedItem.transform.DOScaleY(1f, 0.1f).SetEase(Ease.InSine));
            craftedItemAnims.Insert(0, craftedItem.transform.DOLocalMoveX(1500, 0.5f));
            //craftedItemAnims.Insert(0, craftedItem.transform.DOLocalRotate(new Vector3(0, 0, Random.Range(0, 361)), 1f));
            craftedItemAnims.onComplete += () => Destroy(craftedItem);
            data.Money += data.MoneyPerFood * (data.CurrentClickerProgress / data.MaxProgressBarClicks);
            onFoodCookedEarned?.Invoke(data.MoneyPerFood * (data.CurrentClickerProgress / data.MaxProgressBarClicks));
            CreateEarnedMoneyLabel(data.MoneyPerFood, data.CurrentClickerProgress / data.MaxProgressBarClicks);
            data.SetCurrentClickerProgress(data.CurrentClickerProgress % data.MaxProgressBarClicks);
            onFoodCooked?.Invoke();
        }

        clickProgressFiller.DOFillAmount(1 / (float)data.MaxProgressBarClicks * data.CurrentClickerProgress, 0.1f);
    }

    private void FoodCooked(int earnedMoney)
    {
        earningsManager.AddOrUpdateHistoryEntry(calendarManager.Day, "Кликер", earnedMoney);

        List<GameObject> unlockedFoodObjects = new();

        for (int i = 0; i < data.UnlockedFood.Count; i++)
        {
            if (data.UnlockedFood.ElementAt(i))
                unlockedFoodObjects.Add(foodObjects[i]);
        }
        foreach (GameObject obj in unlockedFoodObjects)
            obj.SetActive(false);
        currentFoodObject = unlockedFoodObjects[Random.Range(0, unlockedFoodObjects.Count)];
        clickProgressFiller.GetComponent<Image>().sprite = currentFoodObject.GetComponent<Image>().sprite;
        currentFoodObject.SetActive(true);
    }
    private void CreateEarnedMoneyLabel(int moneyPerFood, int counter)
    {
        GameObject label = Instantiate(moneyEarnPrefab, canvas);
        label.GetComponent<Text>().text = $"+{moneyPerFood}$x{counter}";
        label.GetComponent<RectTransform>().position = Input.mousePosition;
        label.GetComponent<RectTransform>().DOMove(Input.mousePosition + new Vector3(0, 300), 1f);
        label.GetComponent<Text>().DOFade(0f, 1f).onComplete += () => Destroy(label);
    }
    private void ClickAnimation()
    {
        Sequence clickAnim = DOTween.Sequence();
        clickAnim.Insert(0, _rect.DOShakeScale(clickAnimDuration, .1f, 1, 0));
        clickAnim.Insert(0, Camera.main.DOShakePosition(clickAnimDuration, .1f, 1, 0));
        clickAnim.onComplete += () =>
        {
            _rect.DOScale(_clickerStartScale, clickAnimDuration);
            Camera.main.transform.DOMove(_cameraStartPos, clickAnimDuration);
        };
    }
}