using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [System.Serializable]
    public class SoundData
    {
        public string id;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [Header("Music")]
    public List<SoundData> musicTracks;

    [Header("SFX")]
    public List<SoundData> sfxClips;

    [Header("Global Volumes")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private Dictionary<string, SoundData> musicDict;
    private Dictionary<string, SoundData> sfxDict;

    private void Awake()
    {
        Debug.Log("AudioManager Awake");

        Debug.Log("MusicSource: " + musicSource);
        Debug.Log("SFXSource: " + sfxSource);

        if (musicSource == null || sfxSource == null)
        {
            Debug.LogError("AudioSource NO asignado en AudioManager");
        }
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource.loop = true;
        musicSource.playOnAwake = false;

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;

        BuildDictionaries();
        ApplyVolumes();
    }

    private void BuildDictionaries()
    {
        musicDict = new Dictionary<string, SoundData>();
        foreach (var track in musicTracks)
        {
            if (!musicDict.ContainsKey(track.id))
                musicDict.Add(track.id, track);
        }

        sfxDict = new Dictionary<string, SoundData>();
        foreach (var sfx in sfxClips)
        {
            if (!sfxDict.ContainsKey(sfx.id))
                sfxDict.Add(sfx.id, sfx);
        }
    }

    private void ApplyVolumes()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    // =========================
    // PUBLIC API
    // =========================

    public void PlayMusic(string id)
    {
        if (!musicDict.TryGetValue(id, out var data))
        {
            Debug.LogWarning($"Music ID not found: {id}");
            return;
        }

        if (musicSource.clip == data.clip)
            return;

        musicSource.clip = data.clip;
        musicSource.volume = data.volume * musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string id)
    {
        if (!sfxDict.TryGetValue(id, out var data))
        {
            Debug.LogWarning($"SFX ID not found: {id}");
            return;
        }

        sfxSource.PlayOneShot(data.clip, data.volume * sfxVolume);
    }

    // =========================
    // VOLUME CONTROL (UI)
    // =========================

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
    }
}
