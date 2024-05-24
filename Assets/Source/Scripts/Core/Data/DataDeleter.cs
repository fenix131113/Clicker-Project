using UnityEngine;

public class DataDeleter : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Data"))
            gameObject.SetActive(false);
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("Data");
    }
}
