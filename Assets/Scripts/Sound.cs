using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    private AudioSource _source;

    public void Init(AudioSource source)
    {
        _source = source;
        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = pitch;
    }

    public void Play()
    {
        _source.Play(); 
    }
}