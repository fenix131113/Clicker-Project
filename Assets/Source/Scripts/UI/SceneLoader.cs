using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("data"))
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(1);
    }
}
