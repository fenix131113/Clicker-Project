using Clicker.Core.Earnings;
using Clicker.Core.Time;

public class GeneralPassiveMoneyController
{
    readonly PlayerData data;
    readonly EarningsManager earningsManager;

    public GeneralPassiveMoneyController(PlayerData data, EarningsManager earningsManager)
    {
        CalendarManager.onNewDay += OnNewDay;
        this.data = data;
        this.earningsManager = earningsManager;
    }

    private void OnNewDay(int dayIndex, DayType dayType)
    {
        if(dayIndex % 7 == 0)
        {
            data.Money -= data.UtilityServiceCost;
            earningsManager.AddOrUpdateHistoryEntry(dayIndex, "Коммунальные услуги", 0, data.UtilityServiceCost);
        }
    }
}
