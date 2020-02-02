using System;
using UnityEngine;

namespace Letters
{
    public class LetterE : Letter
    {
        [SerializeField] private float _onUpdateSubstract;
        [SerializeField] private float _onClickSum;

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
            ChangeAlpha(_onClickSum);

            if (_spriteRenderer.color.a >= 1)
            {
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