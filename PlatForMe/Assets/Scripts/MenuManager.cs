using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Header("Player Colors")]
    [SerializeField] private Transform parentGrid;
    [SerializeField] private GameObject colorButton;


    string masterKey = "masterVolume";
    string musicKey = "musicVolume";
    string soundKey = "soundVolume";

    private void Awake()
    {
        if(PlatformManager.instance != null)
        {
            DisplayColors();
        }
    }

    private void DisplayColors()
    {
        foreach (Color color in PlatformManager.instance.collectedColors)
        {
            Image button = Instantiate(colorButton, parentGrid).GetComponent<Image>(); 
            button.color = color;
        }
    }

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
