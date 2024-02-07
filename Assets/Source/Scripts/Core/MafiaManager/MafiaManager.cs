using Clicker.Core.Time;
using Zenject;
using UnityEngine;

public class MafiaManager
{
    #region Save Data
    [SerializeField] private int _mafiaPayExpiredCount;
    public int MafiaPayExpiredCount => _mafiaPayExpiredCount;


    [SerializeField] private bool _isWaitForPayment;
    public bool IsWaitForPayment => _isWaitForPayment;
    #endregion


    private readonly int _takeMoneyCount = 5000;
    public int TakeMoneyCount => _takeMoneyCount;


    private readonly GlobalObjectsContainer objectsContainer;
    private readonly TimeManager timeManager;
    private readonly PlayerData data;

    [Inject]
    public MafiaManager(PlayerData data, GlobalObjectsContainer objectsContainer, TimeManager timeManager)
    {
        this.data = data;
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