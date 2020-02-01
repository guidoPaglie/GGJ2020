using UnityEngine;

public class AlphaAnimator : MonoBehaviour
{
    [SerializeField] private float _from;
    [SerializeField] private float _to;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _timer;
    private bool _start;

    public void OnStart()
    {
        _start = true;
    }

    private void Update()
    {
        if (!_start) return;
            
        var color = _spriteRenderer.color;

        var alpha = Mathf.Lerp(_from, _to, _timer);
        color.a = alpha;

        _spriteRenderer.color = color;

        _timer += Time.deltaTime;

        if (_timer >= 1.0f)
            enabled = false;
    }
}