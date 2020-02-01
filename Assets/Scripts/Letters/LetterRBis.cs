using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Letters
{
    public class LetterRBis : Letter
    {
        private Vector3 _nextPosition;

        private float _delta;

        public override void Break(Action letterRepaired)
        {
            base.Break(letterRepaired);
            
            CalculateNewRandomPosition();
        }

        public override void OnUpdate()
        {
            _delta += Time.deltaTime;
            
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _delta);
            
            if (Vector3.Distance(transform.position, _nextPosition) <= 1.0f)
            {
                _delta = 0;
                CalculateNewRandomPosition();
            }

        }

        private void CalculateNewRandomPosition()
        {
            var randomPos = Random.Range(0, 4);
            var newPosition = Vector3.zero;
            if (randomPos == 0)
            {
                newPosition = new Vector3(Random.Range(0.0f, Screen.width), Screen.height, 10);
            }
            else if (randomPos == 1)
            {
                newPosition = new Vector3(Random.Range(0.0f, Screen.width), 0, 10);
            }
            else if (randomPos == 2)
            {
                newPosition = new Vector3(0, Random.Range(0.0f, Screen.height), 10);
            }
            else if (randomPos == 3)
            {
                newPosition = new Vector3(Screen.width, Random.Range(0.0f, Screen.height), 10);
            }

            _nextPosition = Camera.main.ScreenToWorldPoint(newPosition);
        }

        private void OnMouseDown()
        {
            _letterRepaired();
        }
    }
}