using UnityEngine;
using UnityEngine.UI;

public class AutoClickerBase : MonoBehaviour
{
    public Transform autoCookiesKeeper;
    public Button buyAutoClickerButton;
    public float rotateSpeed;

    private int autoClickers = 0;

    private void Update()
    {
        autoCookiesKeeper.Rotate(0, 0, rotateSpeed * Time.deltaTime * 50f);

        for(int i = 0; i < autoCookiesKeeper.childCount; i++)
        {
            autoCookiesKeeper.GetChild(i).transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime * 50f);
        }
    }

    public void ActivateAutoClicker()
    {
        //Activate
        autoClickers++;
        autoCookiesKeeper.GetChild(autoClickers - 1).gameObject.SetActive(true);
    }
}
