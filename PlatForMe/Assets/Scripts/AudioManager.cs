using System;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType { Music, Sound }
public class AudioManager : MonoBehaviour
{
    [Header("Assignments")]
    [SerializeField] private AudioMixerGroup music;
    [SerializeField] private AudioMixerGroup soundFX;

    [SerializeField] private Sound[] clips = new Sound[0];

    public static AudioManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        foreach (Sound sound in clips)
        {
            if (sound.audioSource == null)
            {
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.clip = sound.clip;

                sound.audioSource = newSource;

                switch (sound.audioType)
                {
                    case AudioType.Music:
                        sound.audioSource.outputAudioMixerGroup = music;
                        break;
                    case AudioType.Sound:
                        sound.audioSource.outputAudioMixerGroup = soundFX;
                        break;
                }

            }
        }
        PlayTarget("Music");
    }

    public void PlayTarget(string name)
    {
        Sound sound = Array.Find(clips, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Music was not found");
        }
        sound.audioSource.loop = sound.loop;
        sound.audioSource.Play();

    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource audioSource;
    [Header("Additional Settings")]
    public AudioType audioType;
    public bool loop;
}
