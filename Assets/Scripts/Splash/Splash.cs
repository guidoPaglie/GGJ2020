using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splash
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] private float _timerToStart = 1.0f;
        [SerializeField] private float _from;
        [SerializeField] private float _to;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _timer;

        private void Start()
        {
            StartCoroutine(StartAnim());
        }

        private IEnumerator StartAnim()
        {
            yield return new WaitForSeconds(_timerToStart);
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);

            while (_timer < 1.0f)
            {
                var color = _spriteRenderer.color;

                var alpha = Mathf.Lerp(_from, _to, _timer);
                color.a = alpha;

                _spriteRenderer.color = color;

                _timer += Time.deltaTime;
                
                yield return new WaitForEndOfFrame();
            }

            enabled = false;

            Invoke(nameof(DeleteScene), 1);
        }

        private void DeleteScene()
        {
            SceneManager.UnloadSceneAsync("Splash");
        }
    }
}