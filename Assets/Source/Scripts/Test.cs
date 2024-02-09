using Clicker.Core.Earnings;
using UnityEngine;
using Zenject;
using TMPro;
using Clicker.Core.Time;
using UnityEditor;

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
}

public class TestEditor
{
    [MenuItem("test/ClearPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}