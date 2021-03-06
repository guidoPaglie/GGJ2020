using System;
using System.Collections.Generic;
using Letters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class LettersAnimator : MonoBehaviour
    {
        private enum AnimationState
        {
            Shake,
            Explode,
            Back
        }

        private readonly List<Vector3> _explodePositions = new List<Vector3>();

        [SerializeField] private List<Letter> _letters;
        [SerializeField] private float _shakeAmount = 0.1f;
        [SerializeField] private float _shakeIncrease = 10f;
        [SerializeField] private float _shakeTime = 3f;
        [SerializeField] private float _backVelocity = 2f;
        [SerializeField] private float _explosionAudioTime = 0.5f;
        [SerializeField] private float _backAudioTime = 1.0f;

        private AnimationState _animationState = AnimationState.Shake;
        private bool _started;
        private float _explodeTimer;
        private float _backTimer;
        private int _currentLetter;
        private float _offsetOutside = 500;
        private Action _onFinishedAnim;
        private bool _explodeFinished;
        private bool _explosionAudio;
        private bool _backAudio;

        public void OnStart(Action onFinishedAnim)
        {
            _started = true;

            _onFinishedAnim = onFinishedAnim;
        }

        private void Update()
        {
            if (!_started) return;

            switch (_animationState)
            {
                case AnimationState.Shake:
                    Shake();
                    break;
                case AnimationState.Explode:
                    Explode();
                    break;
                case AnimationState.Back:
                    Back();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void Shake()
        {
            _shakeAmount += _shakeIncrease;
            
            _letters.ForEach(letter =>
            {
                Vector3 aux = Random.insideUnitCircle * _shakeAmount;
                letter.transform.position = aux + letter._initialPosition;
            });

            _shakeTime -= Time.deltaTime;

            if (_shakeTime <= _explosionAudioTime && !_explosionAudio)
            {
                _explosionAudio = true;
                AudioController.Instance.Play(AudioKeys.Explosion);
            }
            
            if (!(_shakeTime <= 0.0f)) return;
            {
                _letters.ForEach(letter => { _explodePositions.Add(GetRandomPosition()); });
                _animationState = AnimationState.Explode;
            }
        }

        private void Explode()
        {
            for (int i = 0; i < _letters.Count; i++)
            {
                var from = _letters[i].transform.position;
                var to = _explodePositions[i];
                _letters[i].transform.position = Vector3.MoveTowards(from, to, _explodeTimer);
                _explodeTimer += Time.deltaTime;
            }

            for (int i = 0; i < _letters.Count; i++)
            {
                if (Vector3.Distance(_letters[i].transform.position, _explodePositions[i]) > 1.0f)
                    break;
                    
                _animationState = AnimationState.Back;
            }
        }

        private void Back()
        {
            _letters[_currentLetter].transform.position = Vector3.MoveTowards(
                _letters[_currentLetter].transform.position, _letters[_currentLetter]._initialPosition, _backTimer * _backVelocity);

            _backTimer += Time.deltaTime;

            if (Vector3.Distance(_letters[_currentLetter].transform.position,
                    _letters[_currentLetter]._initialPosition) <= _backAudioTime && !_backAudio)
            {
                AudioController.Instance.PlayLetter(_currentLetter);
                _backAudio = true;
            }
            
            if (Vector3.Distance(_letters[_currentLetter].transform.position, _letters[_currentLetter]._initialPosition) > 0.5f)
                return;

            _backAudio = false;
            _letters[_currentLetter].transform.position = _letters[_currentLetter]._initialPosition;
                    
            _currentLetter++;
            _backTimer = 0;

            if (_currentLetter >= _letters.Count)
            {
                enabled = false;
                _onFinishedAnim();
            }
        }

        private Vector3 GetRandomPosition()
        {
            var randomPos = Random.Range(0, 4);
            var newPosition = Vector3.zero;
            if (randomPos == 0)
            {
                //arriba
                newPosition = new Vector3(Random.Range(0.0f, Screen.width), Screen.height + _offsetOutside, 10);
            }
            else if (randomPos == 1)
            {
                //abajo
                newPosition = new Vector3(Random.Range(0.0f, Screen.width), 0 - _offsetOutside, 10);
            }
            else if (randomPos == 2)
            {
                //izq
                newPosition = new Vector3(0 - _offsetOutside, Random.Range(0.0f, Screen.height), 10);
            }
            else if (randomPos == 3)
            {
                //der                
                newPosition = new Vector3(Screen.width + _offsetOutside, Random.Range(0.0f, Screen.height), 10);
            }

            return Camera.main.ScreenToWorldPoint(newPosition);
        }
    }
}