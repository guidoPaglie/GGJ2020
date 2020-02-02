using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Letters
{
    public class LetterR : Letter, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private float _minDistanceToRepair;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector3 delta;
        private Vector3 lastPos;

        private bool _clicked;
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

        
        void FixedUpdate()
        {
            if (!_clicked || _finished) return;

            delta = Input.mousePosition - lastPos;

            var mousePositionX = Input.mousePosition.x;
            var mousePositionY = Input.mousePosition.y;

            var mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, 10));
            var direction = (mousePosWorld - transform.position) * delta.magnitude;
            _rigidbody.AddForce(direction);

            if (delta.magnitude <= 0.0f)
            {
                _rigidbody.velocity = Vector3.zero;
            }
        
            lastPos = Input.mousePosition;
            
            if (Vector3.Distance(transform.position, _initialPosition) <= _minDistanceToRepair)
                Finished();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            lastPos = eventData.position;

            _clicked = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_finished)
                return;
            
            ResetRigidbody();

            _clicked = false;
            CalculateNewRandomPosition();
        }

        private void Finished()
        {
            _finished = true;
            ResetRigidbody();

            _particleSystemFix.SetActive(true);
            _letterRepaired();
        }

        private void ResetRigidbody()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.inertia = 0;
            _rigidbody.angularVelocity = 0;
        }

        /*   
        private void OnMouseDrag()
        {
            if (_finished)
                return;

            var mousePositionX = Input.mousePosition.x;
            var mousePositionY = Input.mousePosition.y;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, 10));

            if (Vector3.Distance(transform.position, _initialPosition) <= _minDistanceToRepair)
                Finished();
        }

        private void OnMouseUp()
        {
            if (_finished)
                return;
            
            CalculateNewRandomPosition();
        }*/
    }
}