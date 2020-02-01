using System;
using UnityEngine;

namespace Letters
{
    public class Letter : MonoBehaviour
    {
        protected Action _letterRepaired;
        protected Vector3 _initialPosition;

        private BoxCollider2D _collider2D;
        private Quaternion _initialRotation;
        private Vector3 _initialScale;
            
        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _initialScale = transform.localScale;
            
            _collider2D.enabled = false;
        }

        public virtual void Break(Action letterRepaired)
        {
            _collider2D.enabled = true;
            
            _letterRepaired = letterRepaired;
        }

        public virtual void OnUpdate() {}

        public virtual void Reset()
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            transform.localScale = _initialScale;
            
            _collider2D.enabled = false;
        }
    }
}