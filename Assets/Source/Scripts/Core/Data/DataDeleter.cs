using UnityEngine;

public class DataDeleter : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("data"))
            gameObject.SetActive(false);
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("data");
    }
}
