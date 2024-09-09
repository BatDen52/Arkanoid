using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound _music;
    [SerializeField] private Sound[] _sounds;

    private Dictionary<string, Sound> _soundsNames = new();

    private void Awake()
    {
        CreateMusicSource();

        foreach (Sound sound in _sounds)
        {
            sound.Init(gameObject.AddComponent<AudioSource>());
            _soundsNames.Add(sound.name, sound);
        }

        RefreshSettings();
    }

    private void CreateMusicSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        _music.Init(source);
        source.loop = true;
        source.Play();
    }

    public void Play(string name)
    {
        if (_soundsNames.TryGetValue(name, out Sound sound))
            sound.Play();
    }

    public void RefreshSettings()
    {
        _music.SetMute(SaveService.MusicIsOn == false);
        _music.SetVolume(SaveService.MusicVolume);

        foreach (Sound sound in _sounds)
        {
            sound.SetMute(SaveService.SoundIsOn == false);
            sound.SetVolume(SaveService.SoundVolume);
        }
    }
}