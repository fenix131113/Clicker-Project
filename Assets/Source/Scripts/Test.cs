using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    PlayerData data;

    [Inject]
    private void Init(PlayerData data)
    {
        this.data = data;
    }

    void Update()
    {
        Debug.Log(data.Money);
        //Debug.Log(data.currentClickerProgress);
    }
}
