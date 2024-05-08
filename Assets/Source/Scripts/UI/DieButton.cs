using UnityEngine;
using UnityEngine.SceneManagement;

public class DieButton : MonoBehaviour
{
    public void ReplayGame()
    {
        PlayerPrefs.SetInt("died", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
