using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}
	public void ExtiGame()
	{
		Application.Quit();
	}
}
