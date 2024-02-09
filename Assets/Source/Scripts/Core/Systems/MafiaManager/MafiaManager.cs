using Clicker.Core.Time;
using Zenject;
using UnityEngine;
using Newtonsoft.Json;
using Clicker.Core.Earnings;

public class MafiaManager
{
    #region Save Data
    [JsonProperty][SerializeField] private bool _isWaitForPayment = false;
    [JsonIgnore] public bool IsWaitForPayment => _isWaitForPayment;
    #endregion


    [JsonProperty][SerializeField] private int _mafiaPayExpiredCount = 0;
    [JsonIgnore] public int MafiaPayExpiredCount => _mafiaPayExpiredCount;


    [JsonIgnore] private readonly int _takeMoneyCount = 5000;
    [JsonIgnore] public int TakeMoneyCount => _takeMoneyCount;


    [JsonIgnore] private GlobalObjectsContainer objectsContainer;
    [JsonIgnore] private TimeManager timeManager;
    [JsonIgnore] private PlayerData data;
    [JsonIgnore] private EarningsManager earningsManager;

    public void SetData(PlayerData data) => this.data = data;
    [Inject]
    public void LoadSavedData(MafiaManager mafiaManager)
    {
        _isWaitForPayment = mafiaManager.IsWaitForPayment;
    }
    [Inject]
    public void Init(GlobalObjectsContainer objectsContainer, TimeManager timeManager, EarningsManager earningsManager)
    {
        this.objectsContainer = objectsContainer;
        this.timeManager = timeManager;
        this.earningsManager = earningsManager;
        CalendarManager.onNewDay += CheckMafia;
        objectsContainer.AcceptPaymentToMafiaButton.onHoldComplete += DoPayment;
        objectsContainer.DenyPaymentToMafiaButton.onHoldComplete += DenyPayment;
        if (IsWaitForPayment)
        {
            timeManager.IsTimePaused = true;
            objectsContainer.MafiaTakeMoneyAskPanel.SetActive(true);
        }
    }

    private void CheckMafia(int day, DayType dayType)
    {
        if (day % 31 == 0)
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
            earningsManager.AddOrUpdateHistoryEntry(CalendarManager.Day, "Мафия", 0, TakeMoneyCount);
        }
    }

    private void CheckLoseConditions()
    {
        if (MafiaPayExpiredCount >= 3)
        {
            timeManager.IsTimePaused = true;
            data.LooseGame("Вы 3 раза не отдали долг мафии и были убиты!");
        }
    }
}