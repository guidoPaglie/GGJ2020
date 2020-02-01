﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AudioKeys
{
    public const string ShakeKey = "ShakeKey";
    public const string Explosion = "Explosion";
    public const string PositionLetter = "PositionLetter";
}

public class AudioController : MonoBehaviour
{
    private static readonly Dictionary<string, AudioClip> _clipsByKey = new Dictionary<string, AudioClip>();

    public static AudioController Instance;


    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private List<AudioClip> _clips;

    private void Awake()
    {
        Instance = this;

        _clipsByKey.Add(AudioKeys.ShakeKey, _clips[0]);
        _clipsByKey.Add(AudioKeys.Explosion, _clips[1]);
        _clipsByKey.Add(AudioKeys.PositionLetter, _clips[2]);
    }

    public void Play(string key)
    {
        var source = _audioSources.First(src => !src.isPlaying);
        source.clip = _clipsByKey[key];
        source.Play();
    }
}