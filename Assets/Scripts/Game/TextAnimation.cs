using TMPro;
using UnityEngine;

namespace Game
{
    public class TextAnimation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtPsychologic;

        private float _timer;
        private bool _start;
        
        public void OnStart(string txt)
        {
            _txtPsychologic.text = txt;

            _start = true;
        }

        private void Update()
        {
            if (!_start) return;
            
            var color = _txtPsychologic.color;

            var alpha = Mathf.Lerp(0, 1, _timer);
            color.a = alpha;

            _txtPsychologic.color = color;

            _timer += Time.deltaTime;

            if (_timer >= 1.0f)
            {
                _timer = 0;
                _start = false;
            }
        }
    }
}