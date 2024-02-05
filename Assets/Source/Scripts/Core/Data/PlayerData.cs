using UnityEngine;

// All data in this script will save in database
public class PlayerData
{
    private int _money;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            if(value < 0)
            {
                Debug.LogWarning("Money can't be -value");
                _money = 0;
            }
        }
    }

    private int _clickPower = 1;
    public int ClickPower => _clickPower;
    public void AddClickPower(int count) { _clickPower += count; }


    private int _moneyPerClick = 1;
    public int MoneyPerClick => _moneyPerClick;
    public void AddMoneyPerClick(int count) { _moneyPerClick += count; }


    public int maxProgressBarClicks = 10;
    public int currentClickerProgress = 0;
}
