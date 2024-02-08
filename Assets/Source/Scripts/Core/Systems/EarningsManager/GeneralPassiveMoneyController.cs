using Clicker.Core.Earnings;
using Clicker.Core.Time;
using Newtonsoft.Json;
using System;
using UnityEngine;

public class GeneralPassiveMoneyController
{
    [JsonIgnore] private int _utilityServiceCost = 50;
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
