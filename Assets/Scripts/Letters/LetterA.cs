using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Letters
{
    public class LetterA : Letter
    {
        [SerializeField] private float _angle;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;
        
        public override void Break(Action letterRepaired)
        {
            base.Break(letterRepaired);

            _angle *= Random.Range(0,2) == 0 ? 1 : -1;
        }
        
        public override void OnUpdate()
        {
            transform.Rotate(0, 0, _angle);
        }

        private void OnMouseDown()
        {
            if ((transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= _minAngle) ||
                (transform.eulerAngles.z >= _maxAngle && transform.eulerAngles.z <= 360))
            {
                _letterRepaired();
            }
            else
            {
                Wrong();
            }
        }
    }
}