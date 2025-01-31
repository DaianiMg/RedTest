using System.Collections.Generic;
using UnityEngine;

public enum SoundType { Punch, Jump, Steps, SpecialPower }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public List<SoundData> soundEffects;
    public List<MusicData> oneTimeMusic;


    private Dictionary<SoundType, AudioClip[]> soundDictionary = new Dictionary<SoundType, AudioClip[]>();
    private Dictionary<string, AudioClip> oneTimeMusicDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (var sound in soundEffects)
            {
                soundDictionary[sound.type] = sound.clips;
            }

            foreach (var music in oneTimeMusic)
            {
                oneTimeMusicDictionary[music.name] = music.clip;
            }

            musicSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(SoundType type)
    {
        if (soundDictionary.ContainsKey(type) && soundDictionary[type].Length > 0)
        {
            int index = Random.Range(0, soundDictionary[type].Length);
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(soundDictionary[type][index]);
        }
    }

    public void PlayMusicOnce(string musicName)
    {
        if (oneTimeMusicDictionary.ContainsKey(musicName))
        {
            musicSource.Stop();
            musicSource.clip = oneTimeMusicDictionary[musicName];
            musicSource.Play();
        }
    }
}

[System.Serializable]
public class SoundData
{
    public SoundType type;
    public AudioClip[] clips;
}

[System.Serializable]
public class MusicData
{
    public string name;
    public AudioClip clip;
}