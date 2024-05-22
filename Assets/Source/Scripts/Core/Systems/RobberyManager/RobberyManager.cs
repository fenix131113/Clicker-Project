using Clicker.Core.Earnings;
using Clicker.Core.Time;
using Newtonsoft.Json;

public class RobberyManager
{
    [JsonIgnore] private int _robberyMoneyPercent = 30;
    [JsonIgnore] public int RobberyMoneyPercent => _robberyMoneyPercent;
    public void SetRobberyMoneyPercent(int count) => _robberyMoneyPercent = count;


    [JsonIgnore] private float _robberyChance = 0.1f;
    [JsonIgnore] public float RobberyChance => _robberyChance;
    /// <summary>
    /// Chance to robbery per hour
    /// </summary>
    public void SetRobberyChance(float count) => _robberyChance = count;


    [JsonIgnore] private bool _allowRobbery = true;
    [JsonIgnore] public bool AllowRobbery => _allowRobbery;
    public void SetAllowRobbery(bool allow) => _allowRobbery = allow;


    [JsonIgnore] private PlayerData data;
    [JsonIgnore] private CalendarManager _calendarManager;
    [JsonIgnore] private readonly EarningsManager earningsManager;
    [JsonIgnore] private readonly NoticeSystem notifications;

        
    [JsonIgnore] private int timeout = 0;

    public void SetData(PlayerData data, CalendarManager calendarManager)
    {
        this.data = data;
        _calendarManager = calendarManager;
    }
    public RobberyManager(EarningsManager earningsManager, NoticeSystem notifications)
    {
        this.earningsManager = earningsManager;
        TimeManager.onNewHour += TryRobbery;

        this.notifications = notifications;
    }
    private void TryRobbery(int hour)
    {
        if (_allowRobbery)
        {
            if (timeout == 0)
            {
                float currentChance = (float)System.Math.Round(UnityEngine.Random.Range(0f, 100f), 2);
                if (currentChance <= _robberyChance && data.Money >= 500)
                    RobberyEnd();
            }
            else
                timeout--;
        }
    }
    private void RobberyEnd()
    {
        //take money percentMoney
        earningsManager.AddOrUpdateHistoryEntry(_calendarManager.Day, "Ограбление", 0, (int)(data.Money * (_robberyMoneyPercent * 0.01f)));
        notifications.CreateNewNotification($"Вас обокрали на {(int)(data.Money * (_robberyMoneyPercent * 0.01f))}$");
        data.Money -= (int)(data.Money * (_robberyMoneyPercent * 0.01f));
        timeout = 48;
    }
}