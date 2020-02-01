using System;
using UnityEngine;

namespace Letters
{
    public class Letter : MonoBehaviour
    {
        protected Action _letterRepaired;

        private BoxCollider2D _collider2D;
        
        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();

            _collider2D.enabled = false;
        }

        public virtual void Break(Action letterRepaired)
        {
            _collider2D.enabled = true;
            
            _letterRepaired = letterRepaired;
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void Reset()
        {
            _collider2D.enabled = false;
        }
    }
}