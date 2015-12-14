using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayingManager : MonoBehaviour
{
    private Stack<AudioSource> audioSources = new Stack<AudioSource>();

    private Dictionary<string, AudioClip> loadedAudio = new Dictionary<string, AudioClip>();

    private HashSet<AudioSource> playingAudioSources = new HashSet<AudioSource>();

    private float volume = 1f;

    public float Volume
    {
        get
        {
            return volume;
        }

        set
        {
            volume = value;
            foreach (var plr in playingAudioSources)
            {
                plr.volume = volume;
            }
        }
    }

    public void Awake()
    {
        if (Game.Instance.SoundPlayer != null) return;
        DontDestroyOnLoad(gameObject);
        Game.Instance.SoundPlayer = this;
    }

    public void PlaySound(string path)
    {
        AudioClip audio;
        bool found = loadedAudio.TryGetValue(path, out audio);

        if (!found)
        {
            audio = Resources.Load<AudioClip>(path);
            loadedAudio.Add(path, audio);
        }

        PlaySound(audio);
    }

    public void PlaySound(AudioClip audio)
    {
        AudioSource player;

        if (audioSources.Count > 0)
        {
            player = audioSources.Pop();
        }
        else
        {
            player = CreateSource();
        }
        player.clip = audio;
        player.volume = Volume;
        player.Play();
        AddNewPlayingSource(player);
    }

    private void AddNewPlayingSource(AudioSource player)
    {
        playingAudioSources.Add(player);
        List<AudioSource> playersToRemove = new List<AudioSource>();
        foreach (var src in playingAudioSources)
        {
            if (!src.isPlaying)
            {
                playersToRemove.Add(src);
            }
        }
        foreach (var src in playersToRemove)
        {
            playingAudioSources.Remove(src);
            audioSources.Push(src);
        }
    }

    private AudioSource CreateSource()
    {
        return gameObject.AddComponent<AudioSource>();
    }
    
    // Use this for initialization
    private void Start()
    {
        Volume = 1.0f;
    }
    // Update is called once per frame
    private void Update()
    {
    }
}