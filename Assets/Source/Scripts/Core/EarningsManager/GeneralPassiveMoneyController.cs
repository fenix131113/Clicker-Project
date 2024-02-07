using Clicker.Core.Earnings;
using Clicker.Core.Time;
using UnityEngine;

public class GeneralPassiveMoneyController
{
    #region Saving Data
    [SerializeField] private int _utilityServiceCost = 50;
    public int UtilityServiceCost => _utilityServiceCost;
    public void SetUtilityServiceCost(int count) => _utilityServiceCost = count;


    [SerializeField] private int _consumablesPayPeriod = 7;
    public int ConsumablesPayPeriod => _consumablesPayPeriod;
    public void SetConsumablesPayPeriod(int days) => _consumablesPayPeriod = days;


    [SerializeField] private int _consumablesCost = 50;
    public int ConsumablesCost => _consumablesCost;
    public void SetConsumablesCost(int cost) => _consumablesCost = cost;
    #endregion

    private PlayerData data;
    private readonly EarningsManager earningsManager;

    public void SetData(PlayerData data) => this.data = data;
    public GeneralPassiveMoneyController(EarningsManager earningsManager)
    {
        CalendarManager.onNewDay += OnNewDay;
        this.earningsManager = earningsManager;
    }

    private void OnNewDay(int dayIndex, DayType dayType)
    {
        if(dayIndex % 7 == 0)
        {
            //Utility service
            data.Money -= UtilityServiceCost;
            earningsManager.AddOrUpdateHistoryEntry(dayIndex, "Коммунальные услуги", 0, UtilityServiceCost);
        }

        if (dayIndex % ConsumablesPayPeriod == 0)
        {
            //Consumables paying
            data.Money -= ConsumablesCost;
            earningsManager.AddOrUpdateHistoryEntry(dayIndex, "Расходы", 0, ConsumablesCost);
        }
    }
}
