using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;

    private int currentTrackIndex = -1;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (currentTrackIndex == -1)
        {
            currentTrackIndex = Random.Range(0, musicClips.Length);
            musicSource.clip = musicClips[currentTrackIndex];
            musicSource.Play();
        }
    }

    private void Update()
    {
        if(musicSource.clip && musicSource.time == musicSource.clip.length)
        {
            int newCurrent = Random.Range(0, musicClips.Length);
            while(newCurrent == currentTrackIndex)
                newCurrent = Random.Range(0, musicClips.Length);
            musicSource.clip = musicClips[newCurrent];
            musicSource.Play();
        }
    }
}
