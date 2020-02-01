using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Letters
{
    public class LetterR : MonoBehaviour
    {
        private Action _letterRepaired;
        private bool _finished;

        public void Break(Action letterRepaired)
        {
            _letterRepaired = letterRepaired;

            CalculateNewRandomPosition();
        }

        private void CalculateNewRandomPosition()
        {
            var randomPos = Random.Range(0, 3);
            var newPosition = Vector3.zero;
            if (randomPos == 0)
            {
                newPosition = new Vector3(Random.Range(0.0f, Screen.width / 2), Screen.height, 10);
            }
            else if (randomPos == 1)
            {
                newPosition = new Vector3(Random.Range(0.0f, Screen.width / 2), 0, 10);
            }
            else if (randomPos == 2)
            {
                newPosition = new Vector3(0, Random.Range(0.0f, Screen.height), 10);
            }

            transform.position = Camera.main.ScreenToWorldPoint(newPosition);

            //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,0.5f,10));
        }

        private void OnMouseDrag()
        {
            if (_finished)
                return;
            
            var mousePositionX = Input.mousePosition.x;
            var mousePositionY = Input.mousePosition.y;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, 10));

            if (transform.position.x >= -3)
            {
                Finished();
            }
        }

        private void OnMouseUp()
        {
            if (_finished)
                return;

            CalculateNewRandomPosition();
        }

        private void Finished()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            _finished = true;
            
            _letterRepaired();
        }
    }
}