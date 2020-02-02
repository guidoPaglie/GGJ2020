using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Letters
{
    public class Fin : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _finText;
        [SerializeField] private List<TextMeshProUGUI> _finalTexts;
        [SerializeField] private float _finTimer;
        [SerializeField] private float _otherTextsTimer;

        private void Start()
        {
            StartCoroutine(Anim());
        }

        private IEnumerator Anim()
        {
            float timer = 0.0f;
            while (timer < _finTimer)
            {
                ChangeAlpha(_finText, timer);
                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            timer = 0;
            var index = 0;

            while (index < _finalTexts.Count)
            {
                while (timer < _finTimer)
                {
                    ChangeAlpha(_finalTexts[index], timer);
                    timer += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                timer = 0;
                index++;
            }
        }

        private void ChangeAlpha(TextMeshProUGUI text, float timer)
        {
            var color = text.color;

            var alpha = Mathf.Lerp(0, 1, timer / _finTimer);
            color.a = alpha;

            text.color = color;
        }
    }
}