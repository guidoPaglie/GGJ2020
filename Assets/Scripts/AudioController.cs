using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AudioKeys
{
    public const string ShakeKey = "ShakeKey";
    public const string Explosion = "Explosion";
    public const string BreakLetter = "BreaLetter";
    public const string Error  = "Error";
    public const string OkLetter  = "OkLetter";
    public const string FinishLetter  = "FinishLetter";
}

public class AudioController : MonoBehaviour
{
    private static readonly Dictionary<string, AudioClip> _clipsByKey = new Dictionary<string, AudioClip>();

    public static AudioController Instance;

    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private List<AudioClip> _letters;

    private void Awake()
    {
        Instance = this;

        _clipsByKey.Add(AudioKeys.ShakeKey, _clips[0]);
        _clipsByKey.Add(AudioKeys.Explosion, _clips[1]);
        _clipsByKey.Add(AudioKeys.BreakLetter, _clips[2]);
        _clipsByKey.Add(AudioKeys.Error, _clips[3]);
        _clipsByKey.Add(AudioKeys.OkLetter, _clips[4]);
        _clipsByKey.Add(AudioKeys.FinishLetter, _clips[5]);
    }

    public void Play(string key)
    {
        var source = _audioSources.First(src => !src.isPlaying);
        source.clip = _clipsByKey[key];
        source.Play();
    }

    public void PlayLetter(int currentLetter)
    {
        var source = _audioSources.First(src => !src.isPlaying);
        source.clip = _letters[currentLetter];
        source.Play();
    }
}