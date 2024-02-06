using Clicker.Core.Earnings;
using Clicker.Core.Time;
using UnityEngine;

public class RobberyManager
{
    #region Saving Data
    [SerializeField] private int _robberyMoneyPercent = 30;
    public int RobberyMoneyPercent => _robberyMoneyPercent;
    public void SetRobberyMoneyPercent(int count) => _robberyMoneyPercent = count;


    [SerializeField] private float _robberyChance = 0.1f;
    public float RobberyChance => _robberyChance;
    /// <summary>
    /// Chance to robbery per hour
    /// </summary>
    public void SetRobberChance(float count) => _robberyChance = count;

    [SerializeField] private bool _allowRobbery = true;
    public bool AllowRobbery => _allowRobbery;
    public void SetAllowRobbery(bool allow) => _allowRobbery = allow;
    #endregion

    private PlayerData data;
    private readonly EarningsManager earningsManager;


    private int timeout = 0;

    public void SetData(PlayerData data) => this.data = data;
    public RobberyManager(EarningsManager earningsManager)
    {
        this.earningsManager = earningsManager;
        TimeManager.onNewHour += TryRobbery;
    }
    private void TryRobbery(int hour)
    {
        if (_allowRobbery)
        {
            if (timeout == 0)
            {
                float currentChance = (float)System.Math.Round(Random.Range(0f, 100f), 2);
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
        earningsManager.AddOrUpdateHistoryEntry(CalendarManager.Day, "Ограбление", 0, (int)(data.Money * (_robberyMoneyPercent * 0.01f)));
        data.Money -= (int)(data.Money * (_robberyMoneyPercent * 0.01f));
        timeout = 48;
    }
}