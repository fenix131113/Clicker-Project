using Clicker.Core.Time;
using UnityEngine;

public class RobberyManager
{
    public float percentMoney = 30;
    public float robberChance = 10;
    public int timeout = 0;

    public RobberyManager()
    {
        TimeManager.onNewHour += TryRobbery;
    }
    private void TryRobbery(int hour)
    {
        if (timeout == 0)
        {
            float currentChance = Random.Range(0, 10001);
            if (currentChance <= robberChance)
            {
                RobberyEnd();
            }
        }
        else
            timeout--;
    }
    public void RobberyEnd()
    {
        //take money percentMoney
        timeout = 48;
    }
}
