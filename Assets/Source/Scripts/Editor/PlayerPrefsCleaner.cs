using UnityEngine;
using UnityEditor;

public class PlayerPrefsCleaner : MonoBehaviour
{
    [MenuItem("Clicker/Clear Prefs")]
    public static void ClearPrefs() => PlayerPrefs.DeleteAll();

    [MenuItem("Clicker/Debug Prefs")]
    public static void DebugPrefs()
    {
        Debug.Log("Data: " + PlayerPrefs.HasKey("data"));
    }    
    [MenuItem("Clicker/Print Prefs")]
    public static void PrintData()
    {
        Debug.Log(PlayerPrefs.GetString("data"));
    }
}