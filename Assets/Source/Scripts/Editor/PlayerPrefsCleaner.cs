using UnityEngine;
using UnityEditor;

public class PlayerPrefsCleaner : MonoBehaviour
{
    [MenuItem("Clicker/Clear Prefs")]
    public static void ClearPrefs() => PlayerPrefs.DeleteAll();
}