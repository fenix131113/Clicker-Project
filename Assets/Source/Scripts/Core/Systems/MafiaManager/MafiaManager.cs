using Clicker.Core.Time;
using Zenject;
using UnityEngine;
using Newtonsoft.Json;

public class MafiaManager
{
    #region Save Data
    [JsonProperty][SerializeField] private bool _isWaitForPayment = false;
    [JsonIgnore] public bool IsWaitForPayment => _isWaitForPayment;
    #endregion


    [JsonIgnore] private int _mafiaPayExpiredCount;
    [JsonIgnore] public int MafiaPayExpiredCount => _mafiaPayExpiredCount;


    [JsonIgnore] private readonly int _takeMoneyCount = 5000;
    [JsonIgnore] public int TakeMoneyCount => _takeMoneyCount;


    [JsonIgnore] private GlobalObjectsContainer objectsContainer;
    [JsonIgnore] private TimeManager timeManager;
    [JsonIgnore] private PlayerData data;

    public void SetData(PlayerData data) => this.data = data;
    [Inject]
    public void LoadSavedData(MafiaManager mafiaManager)
    {
        _isWaitForPayment = mafiaManager.IsWaitForPayment;
    }
    [Inject]
    public void Init(GlobalObjectsContainer objectsContainer, TimeManager timeManager)
    {
        this.objectsContainer = objectsContainer;
        this.timeManager = timeManager;
        CalendarManager.onNewDay += CheckMafia;
        objectsContainer.AcceptPaymentToMafiaButton.onClick.AddListener(DoPayment);
        objectsContainer.DenyPaymentToMafiaButton.onClick.AddListener(DenyPayment);
        if (IsWaitForPayment)
        {
            timeManager.IsTimePaused = true;
            objectsContainer.MafiaTakeMoneyAskPanel.SetActive(true);
        }
    }

    private void CheckMafia(int day, DayType dayType)
    {
        if (day % 35 == 0)
        {
            _isWaitForPayment = true;
            timeManager.IsTimePaused = true;
            objectsContainer.MafiaTakeMoneyAskPanel.SetActive(true);
        }
    }

    private void DenyPayment()
    {
        objectsContainer.MafiaTakeMoneyAskPanel.SetActive(false);
        _mafiaPayExpiredCount++;
        _isWaitForPayment = false;
        timeManager.IsTimePaused = false;
        CheckLoseConditions();
    }
    private void DoPayment()
    {
        if (data.Money >= TakeMoneyCount)
        {
            data.Money -= TakeMoneyCount;
            timeManager.IsTimePaused = false;
            _isWaitForPayment = false;
            objectsContainer.MafiaTakeMoneyAskPanel.SetActive(false);
        }
    }

    private void CheckLoseConditions()
    {
        if (MafiaPayExpiredCount >= 3)
        {
            timeManager.IsTimePaused = true;
            Debug.Log("You Loose");
        }
    }
}