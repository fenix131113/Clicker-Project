using UnityEngine;
using UnityEngine.SceneManagement;

public class DieButton : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }
}
