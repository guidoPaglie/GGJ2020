using System;
using UnityEngine;

namespace Letters
{
    public class LetterP : Letter
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        public override void Break(Action letterRepaired)
        {
            base.Break(letterRepaired);

            _animation.Play();
        }

        private void OnMouseDown()
        {
            var scale = transform.localScale;
            if (scale.x >= _minScale && scale.x <= _maxScale && scale.y >= _minScale && scale.y <= _maxScale)
            {
                _letterRepaired();
            }
        }

        public override void Reset()
        {
            base.Reset();
            
            _animation.Stop();
            
            transform.localScale = Vector3.one;
        }
    }
}