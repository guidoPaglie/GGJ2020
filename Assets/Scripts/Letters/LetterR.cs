using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Letters
{
    public class LetterR : Letter
    {
        [SerializeField] private float _minDistanceToRepair;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;
        
        private bool _finished;
        
        public override void Break(Action letterRepaired)
        {
            base.Break(letterRepaired);

            CalculateNewRandomPosition();
        }

        private void CalculateNewRandomPosition()
        {
            var randomPos = Random.Range(0, 3);
            var newPosition = Vector3.zero;
            if (randomPos == 0)
            {
                //arriba
                newPosition = new Vector3(Random.Range(0.0f, Screen.width / 2), Screen.height, 10);
            }
            else if (randomPos == 1)
            {
                //derecha
                newPosition = new Vector3(Screen.width, Random.Range(0.0f, Screen.height), 10);
            }
            else if (randomPos == 2)
            {
                //izq
                newPosition = new Vector3(0, Random.Range(0.0f, Screen.height), 10);
            }

            transform.position = Camera.main.ScreenToWorldPoint(newPosition);
        }

        private void OnMouseDrag()
        {
            if (_finished)
                return;

            var mousePositionX = Input.mousePosition.x - _offsetX;
            var mousePositionY = Input.mousePosition.y - _offsetY;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, 10));

            if (Vector3.Distance(transform.position, _initialPosition) <= _minDistanceToRepair)
                Finished();
        }

        private void OnMouseUp()
        {
            if (_finished)
                return;
            
            CalculateNewRandomPosition();
        }

        private void Finished()
        {
            _finished = true;

            _letterRepaired();
        }
    }
}