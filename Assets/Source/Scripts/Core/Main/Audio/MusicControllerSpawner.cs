using UnityEngine;

public class MusicControllerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject musicControllerPrefab;

    private void Awake()
    {
        if(!FindObjectOfType<MusicController>())
            Instantiate(musicControllerPrefab);
    }
}
