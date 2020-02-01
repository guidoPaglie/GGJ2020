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
            Shake, Explode, Back
        }

        [SerializeField] private List<Letter> _letters;
        [SerializeField] private float _shakeAmount = 0.1f;
        [SerializeField] private float _shakeIncrease = 10f;
        [SerializeField] private float _shakeTime = 3f;

        private AnimationState _animationState = AnimationState.Shake;
        private bool _started;
        private float _timer;
        private int _currentLetter;
        private float _offsetOutside = 300;
        
        public void OnStart()
        {
            _started = true;
            
            
            //First Shake
            
            
            //Then Explode
            
            //Then came backs
            
            //_letters.ForEach(letter => letter.transform.position = GetRandomPosition());
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
            _letters.ForEach(letter =>
            {
                Vector3 aux = Random.insideUnitCircle * _shakeAmount;
                _shakeAmount += (Time.deltaTime/ _shakeIncrease);
                letter.transform.position = aux + letter._initialPosition;
            });

            _shakeTime -= Time.deltaTime;

            if (_shakeTime <= 0.0f)
                _animationState = AnimationState.Explode;
        }

        private void Explode()
        {
            _letters.ForEach(letter =>
            {
                
            });
        }

        private void Back()
        {
            _letters[_currentLetter].transform.position = Vector3.MoveTowards(
                _letters[_currentLetter].transform.position, _letters[_currentLetter]._initialPosition, _timer);

            _timer += Time.deltaTime;

            if (_timer >= 1.0f)
            {
                _currentLetter++;
                _timer = 0;

                if (_currentLetter >= _letters.Count)
                {
                    enabled = false;
                }
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
                newPosition = new Vector3(0 -_offsetOutside, Random.Range(0.0f, Screen.height), 10);
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