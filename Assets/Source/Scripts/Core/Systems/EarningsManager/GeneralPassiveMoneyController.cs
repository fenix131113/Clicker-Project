using Clicker.Core.Earnings;
using Clicker.Core.Time;
using Newtonsoft.Json;

public class GeneralPassiveMoneyController
{
    [JsonIgnore] private int _utilityServiceCost = 25;
    [JsonIgnore] public int UtilityServiceCost => _utilityServiceCost;
    public void SetUtilityServiceCost(int count) => _utilityServiceCost = count;


    [JsonIgnore] private int _consumablesPayPeriod = 7;
    [JsonIgnore] public int ConsumablesPayPeriod => _consumablesPayPeriod;
    public void SetConsumablesPayPeriod(int days) => _consumablesPayPeriod = days;


    [JsonIgnore] private int _consumablesCost = 50;
    [JsonIgnore] public int ConsumablesCost => _consumablesCost;
    public void SetConsumablesCost(int cost) => _consumablesCost = cost;


    [JsonIgnore] private PlayerData data;
    [JsonIgnore] private readonly EarningsManager earningsManager;
    [JsonIgnore] private CalendarManager _calendarManager;

    public void SetData(PlayerData data, CalendarManager calendarManager)
    {
        this.data = data;
        _calendarManager = calendarManager;
    }
    public GeneralPassiveMoneyController(EarningsManager earningsManager)
    {
        this.earningsManager = earningsManager;
    }
    public void UtilitiesPayment()
    {
        //Utility service payment
        data.Money -= UtilityServiceCost;
        earningsManager.AddOrUpdateHistoryEntry(_calendarManager.Day - 1, "Коммунальные услуги", 0, UtilityServiceCost);
    }

    public void ConsumablePayment()
    {
        //Consumables payment
        data.Money -= ConsumablesCost;
        earningsManager.AddOrUpdateHistoryEntry(_calendarManager.Day - 1, "Расходы", 0, ConsumablesCost);
    }
}
