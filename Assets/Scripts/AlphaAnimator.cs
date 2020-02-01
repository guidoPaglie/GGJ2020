using System;
using UnityEngine;

public class AlphaAnimator : MonoBehaviour
{
    [SerializeField] private float _from;
    [SerializeField] private float _to;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _timer;
    private bool _start;
    private Action _onFinished;
    private bool _shakeAudio;
    
    public void OnStart(Action onFinished = null)
    {
        _start = true;
        _onFinished = onFinished;
    }

    private void Update()
    {
        if (!_start) return;

        var color = _spriteRenderer.color;

        var alpha = Mathf.Lerp(_from, _to, _timer);
        color.a = alpha;

        _spriteRenderer.color = color;

        _timer += Time.deltaTime;

        if ( _onFinished != null && _timer >= 0.8f && !_shakeAudio) // it's calling from Game ... :D
        {
            _shakeAudio = true;
            AudioController.Instance.Play(AudioKeys.ShakeKey);
        }

        if (_timer >= 1.0f)
        {
            enabled = false;

            if (_onFinished != null)
                _onFinished();
        }
    }
}