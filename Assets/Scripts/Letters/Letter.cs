using System;
using System.Collections;
using Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Letters
{
    public class Letter : MonoBehaviour
    {
        [NonSerialized] public Vector3 _initialPosition;
        [NonSerialized] public SpriteRenderer _spriteRenderer;
        
        [SerializeField] protected TextAnimation _textAnimation;
        
        [SerializeField] private GameObject _particleSystem;
        [SerializeField] private string _txt;

        protected Action _letterRepaired;
        
        private BoxCollider2D _collider2D;
        private Quaternion _initialRotation;
        private Vector3 _initialScale;
        public virtual void OnUpdate() {}
            
        public void OnAwake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _initialScale = transform.localScale;
            
            _collider2D.enabled = false;
        }

        public virtual void Break(Action letterRepaired)
        {
            _particleSystem.SetActive(true);
            
            _collider2D.enabled = true;
            
            _letterRepaired = letterRepaired;

            _textAnimation.OnStart(_txt);
        }

        protected void Wrong()
        {
            _collider2D.enabled = false;
            _spriteRenderer.color = Color.red;

            StartCoroutine(Wrongness());
        }

        private IEnumerator Wrongness()
        {
            float timer = 0.0f;
            while (timer <= 1.0f)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                Vector3 aux = Random.insideUnitCircle * 0.1f;
                transform.position = aux + _initialPosition;
            }
            
            ResetWrongness();
        }

        private void ResetWrongness()
        {
            _spriteRenderer.color = Color.white;
            _collider2D.enabled = true;
        }

        public virtual void Reset()
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            transform.localScale = _initialScale;
            
            _collider2D.enabled = false;
        }
    }
}