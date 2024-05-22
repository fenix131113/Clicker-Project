using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        _source.PlayOneShot(clip, volume);
    }
}
