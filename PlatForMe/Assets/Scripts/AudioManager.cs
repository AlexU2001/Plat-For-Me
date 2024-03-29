using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType { Music, Sound }
public class AudioManager : MonoBehaviour
{
    [Header("Assignments")]
    [SerializeField] private AudioMixerGroup music;
    [SerializeField] private AudioMixerGroup soundFX;

    [SerializeField] private Sound[] clips = new Sound[0];
    [SerializeField] private float fadeTime = 2f;

    private AudioSource musicSource;
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

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void Start()
    {
        foreach (Sound sound in clips)
        {
            switch (sound.audioType)
            {
                case AudioType.Sound:
                    if (sound.audioSource == null)
                    {
                        AudioSource newSource = gameObject.AddComponent<AudioSource>();
                        newSource.clip = sound.clip;

                        sound.audioSource = newSource;
                    }
                    sound.audioSource.outputAudioMixerGroup = soundFX;
                    break;
            }
        }

    }

    public void PlayTarget(string name)
    {

        Sound sound = Array.Find(clips, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Music was not found");
        }
        switch (sound.audioType)
        {
            case AudioType.Music:
                sound.audioSource = musicSource;

                StartCoroutine(changeClip(fadeTime, sound.clip));

                sound.audioSource.loop = sound.loop;

                sound.audioSource.outputAudioMixerGroup = music;
                sound.audioSource.Play();
                break;
            case AudioType.Sound:
                sound.audioSource.loop = sound.loop;
                sound.audioSource.Play();
                break;
        }
    }

    private IEnumerator changeClip(float time, AudioClip targetClip) {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            musicSource.volume = Mathf.Lerp(1f, 0f, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        musicSource.clip = targetClip;

        elapsedTime = 0;
        while (elapsedTime < time)
        {
            musicSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void PlayTarget(AudioClip clip)
    {

        Sound sound = Array.Find(clips, sound => sound.clip == clip);
        if (sound == null)
        {
            Debug.LogWarning("Music was not found");
            return;
        }

        sound.audioSource = musicSource;

        if (sound.audioSource.clip != clip)
        {
            sound.audioSource.clip = sound.clip;
            sound.audioSource.loop = sound.loop;

            sound.audioSource.outputAudioMixerGroup = music;
            sound.audioSource.Play();
        }
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
