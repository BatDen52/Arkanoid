using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;

    private Dictionary<string, Sound> _soundsNames = new();

    private void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.Init(gameObject.AddComponent<AudioSource>());
            _soundsNames.Add(sound.name, sound);
        }
    }

    public void Play(string name)
    {
        if (_soundsNames.TryGetValue(name, out Sound sound))
            sound.Play();
    }

    public void RefreshSettings()
    {

    }
}