using System;
using UnityEngine;

namespace Letters
{
    public class LetterE : Letter
    {
        [SerializeField] private float _onUpdateSubstract;
        [SerializeField] private AnimationCurve _animationCurve;

        public override void Break(Action letterRepaired)
        {
            base.Break(letterRepaired);

            ChangeAlpha(-1);
        }

        public override void OnUpdate()
        {
            if (_spriteRenderer.color.a > 0)
                ChangeAlpha(_onUpdateSubstract);
        }

        private void OnMouseDown()
        {
            ChangeAlpha(_animationCurve.Evaluate(_spriteRenderer.color.a));

            UnityEngine.Debug.LogError(_animationCurve.Evaluate(_spriteRenderer.color.a) + " " + _spriteRenderer.color.a);
            if (_spriteRenderer.color.a >= 1)
            {
                _particleSystemFix.SetActive(true);

                _letterRepaired();
            }
        }

        private void ChangeAlpha(float diff)
        {
            var spriteRendererColor = _spriteRenderer.color;
            spriteRendererColor.a += diff;

            _spriteRenderer.color = spriteRendererColor;
        }
    }
}