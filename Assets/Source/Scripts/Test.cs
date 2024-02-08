using Clicker.Core.Earnings;
using UnityEngine;
using Zenject;
using TMPro;
using Clicker.Core.Time;

public class Test : MonoBehaviour
{
    public TMP_Text timeText;

    PlayerData data;
    EarningsManager earningsManager;
    TimeManager timeManager;
    NoticeSystem notifications;

    [Inject]
    private void Init(PlayerData data, EarningsManager earningsManager, TimeManager timeManager, NoticeSystem notifications)
    {
        this.data = data;
        this.earningsManager = earningsManager;
        this.timeManager = timeManager;
        this.notifications = notifications;
    }

    void Start()
    {
        PlayerPrefs.DeleteAll();
        
    }
}
