using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    string masterKey = "masterVolume";
    string musicKey = "musicVolume";
    string soundKey = "soundVolume";

    private void Start()
    {
        HasKey(masterKey, masterSlider);
        HasKey(musicKey, musicSlider);
        HasKey(soundKey, soundSlider);
    }

    // Checks if a PlayerPref exists, if not, sets the sliders to match the default options
    void HasKey(string key, Slider slider)
    {
        float keyValue;
        if (PlayerPrefs.HasKey(key))
        {
            keyValue = PlayerPrefs.GetFloat(key);
            audioMixer.SetFloat(key, keyValue);
            Debug.Log(keyValue);
            slider.value = keyValue;
        }
        else if(!PlayerPrefs.HasKey(key))
        {
            audioMixer.GetFloat(key, out keyValue);
            slider.value = keyValue;
        } else
        {
            Debug.LogWarning("Something went wrong");
        }
    }

    public void ChangeMasterVolumeSlider()
    {
        audioMixer.SetFloat(masterKey, masterSlider.value);
        PlayerPrefs.SetFloat(masterKey, masterSlider.value);
    }

    public void ChangeMusicVolumeSlider()
    {
        audioMixer.SetFloat(musicKey, musicSlider.value);
        PlayerPrefs.SetFloat(musicKey, musicSlider.value);
    }

    public void ChangeSoundVolumeSlider()
    {
        audioMixer.SetFloat(soundKey, soundSlider.value);
        PlayerPrefs.SetFloat(soundKey, soundSlider.value);
    }
}
