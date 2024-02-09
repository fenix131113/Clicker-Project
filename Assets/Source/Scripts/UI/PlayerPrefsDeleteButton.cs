using UnityEngine;

public class PlayerPrefsDeleteButton : MonoBehaviour
{
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
