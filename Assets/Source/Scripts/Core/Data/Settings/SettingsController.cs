using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private AudioMixer _volumeMixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _soundsVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private const string prefsAbbreviation = "KCS_";
    private const string masterGroupName = "Master";
    private const string soundsGroupName = "Sounds";
    private const string musicGroupName = "Music";

    private void Awake()
    {
        _masterVolumeSlider.onValueChanged.AddListener((float vol) => SetMixerVolume(masterGroupName, vol));
        _soundsVolumeSlider.onValueChanged.AddListener((float vol) => SetMixerVolume(soundsGroupName, vol));
        _musicVolumeSlider.onValueChanged.AddListener((float vol) => SetMixerVolume(musicGroupName, vol));

        LoadVolumeDataIfExist();
    }

    private void SetMixerVolume(string groupName, float volume)
    {
        _volumeMixer.SetFloat(groupName, GetFormattedMixerFloat(volume));
        SaveNewVolumeData(groupName, volume);
    }

    private void SaveNewVolumeData(string groupName, float volume)
    {
        // KCS - Kitchen Clicker Settings
        PlayerPrefs.SetFloat(prefsAbbreviation + groupName, volume);
    }
    private void LoadVolumeDataIfExist()
    {
        if(PlayerPrefs.HasKey(prefsAbbreviation + masterGroupName))
        {
            float masterValueLoaded = PlayerPrefs.GetFloat(prefsAbbreviation + masterGroupName);
            SetMixerVolume(masterGroupName, GetFormattedMixerFloat(masterValueLoaded));
            _masterVolumeSlider.value = masterValueLoaded;
        }
        if (PlayerPrefs.HasKey(prefsAbbreviation + soundsGroupName))
        {
            float soundsValueLoaded = PlayerPrefs.GetFloat(prefsAbbreviation + soundsGroupName);
            SetMixerVolume(soundsGroupName, GetFormattedMixerFloat(soundsValueLoaded));
            _soundsVolumeSlider.value = soundsValueLoaded;
        }
        if (PlayerPrefs.HasKey(prefsAbbreviation + musicGroupName))
        {
            float musicValueLoaded = PlayerPrefs.GetFloat(prefsAbbreviation + musicGroupName);
            SetMixerVolume(musicGroupName, GetFormattedMixerFloat(musicValueLoaded));
            _musicVolumeSlider.value = musicValueLoaded;
        }
    }

    private float GetFormattedMixerFloat(float volume)
    {
        return Mathf.Log10(volume) * 20;
    }
}
