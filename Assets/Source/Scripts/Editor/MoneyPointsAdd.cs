using UnityEngine;
using UnityEditor;
using Zenject;

public class MoneyPointsAdd : MonoBehaviour
{
    private static PlayerData _data;
    private NoticeSystem _notices;
    [Inject]
    private void Init(PlayerData data, NoticeSystem notices)
    {
        _data = data;
        _notices = notices;
    }
    [ContextMenu("Notice")]
    public void CreateEditorNotification()
    {
        _notices.CreateNewNotification("Yes i am a notififcation what do you think about me?");
    }
    [MenuItem("Clicker/ Add Money And Points")]
    public static void GiveMoneyAndPoints()
    {
        _data.Money += 50000;
        _data.AddSkillPoints(500);
    }

    [MenuItem("Clicker/ Set Money 0")]
    public static void SetMoneyToZero()
    {
        _data.Money = 0;
    }

    [MenuItem("Clicker/ Set Money -500")]
    public static void SetMoneyToMinus()
    {
        _data.Money = -500;
    }
}