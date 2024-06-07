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

    [JsonIgnore] private readonly int _mafiaVisitPeriod = 14;
    [JsonIgnore] private const int _takeMoneyMultiplier = 4;

    [JsonProperty] private int _takeMoneyCount = 500;
    [JsonIgnore] private GlobalObjectsContainer objectsContainer;
    [JsonIgnore] private TimeManager timeManager;
    [JsonIgnore] private PlayerData data;
    [JsonIgnore] private EarningsManager earningsManager;
    [JsonIgnore] private CalendarManager _calendarManager;

    [JsonIgnore] public int TakeMoneyCount => _takeMoneyCount;
    [JsonIgnore] public int MafiaPayExpiredCount => _mafiaPayExpiredCount;
    [JsonIgnore] public int MafiaVisitPeriod => _mafiaVisitPeriod;

    public delegate void MafiaNoParameterEventHandler();
    public event MafiaNoParameterEventHandler onMafiaPayComplete;

    public void SetData(PlayerData data)
    {
        this.data = data;
        onMafiaPayComplete += data.IncreaseMafiaPayments;
    }

    [Inject]
    public void LoadSavedData(MafiaManager mafiaManager)
    {
        _isWaitForPayment = mafiaManager.IsWaitForPayment;
    }
    [Inject]
    public void Init(GlobalObjectsContainer objectsContainer, TimeManager timeManager, EarningsManager earningsManager, CalendarManager calendarManager)
    {
        this.objectsContainer = objectsContainer;
        this.timeManager = timeManager;
        this.earningsManager = earningsManager;
        _calendarManager = calendarManager;
        objectsContainer.AcceptPaymentToMafiaButton.onHoldComplete += DoPayment;
        objectsContainer.DenyPaymentToMafiaButton.onHoldComplete += DenyPayment;
        if (IsWaitForPayment)
        {
            timeManager.IsTimePaused = true;
            objectsContainer.MafiaTakeMoneyAskPanel.SetActive(true);
        }
    }

    public void InitMafiaVisit()
    {
        _isWaitForPayment = true;
        timeManager.IsTimePaused = true;
        objectsContainer.MafiaPanelText.text = $"Are you ready to pay {TakeMoneyCount}$ to the mafia?";
        objectsContainer.MafiaTakeMoneyAskPanel.SetActive(true);
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
            onMafiaPayComplete?.Invoke();
            data.Money -= TakeMoneyCount;
            timeManager.IsTimePaused = false;
            _isWaitForPayment = false;
            objectsContainer.MafiaTakeMoneyAskPanel.SetActive(false);
            earningsManager.AddOrUpdateHistoryEntry(_calendarManager.Day, "Mafia", 0, TakeMoneyCount);
            _takeMoneyCount *= _takeMoneyMultiplier;
        }
    }

    private void CheckLoseConditions()
    {
        if (MafiaPayExpiredCount >= 2)
        {
            timeManager.IsTimePaused = true;
            data.LooseGame("You failed to repay the mafia's debt twice and were killed!");
        }
    }
}