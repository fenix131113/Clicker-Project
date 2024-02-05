using UnityEngine;

public class CoockieClickSoundSource : MonoBehaviour
{
    public void Init(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
