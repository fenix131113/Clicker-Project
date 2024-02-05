using TMPro;
using UnityEngine;

public class ExitBlock : MonoBehaviour
{
    public float timeToOpen;
    void Update()
    {
        if (timeToOpen > 0)
        {
            timeToOpen -= Time.deltaTime;
            transform.GetComponentInChildren<TMP_Text>().text = Mathf.Ceil(timeToOpen).ToString();
        }
        else
            gameObject.SetActive(false);
    }
}
