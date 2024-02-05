using DG.Tweening;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    private Tweener scaleAnim;
    GameManager gameManager;
    private float timer;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timer = gameManager.AutoClickRate;
    }
    private void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;
        else
        {
            timer = gameManager.AutoClickRate;
            AutoClicking();
        }

        if (scaleAnim != null && !scaleAnim.active)
            scaleAnim = transform.DOScale(new Vector3(0.35f, 0.35f, 0.35f), 0.15f);
    }
    private void AutoClicking()
    {
        gameManager.AddScore(gameManager.AutoClickPower);
        FindObjectOfType<MainCookie>().CreateFadedCookie();
        ClickAnimation();
    }

    private void ClickAnimation()
    {
        if (transform.lossyScale.x <= 1.09f)
            scaleAnim = transform.DOScale(new Vector3(0.45f, 0.45f, 0.45f), 0.15f);
    }
}
