using UnityEngine;
using UnityEditor;
using Zenject;

public class MoneyPointsAdd : MonoBehaviour
{
    private static PlayerData _data;
    [Inject]
    private void Init(PlayerData data)
    {
        _data = data;
    }
    [MenuItem("Clicker/ Add Money And Points")]
    public static void GiveMoneyAndPoints()
    {
        _data.Money += 50000;
        _data.AddSkillPoints(500);
    }
}