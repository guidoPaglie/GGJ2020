using System;
using System.Collections;
using System.Collections.Generic;
using Letters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Game : MonoBehaviour
    {
        private enum GameState
        {
            Animating,
            BreakAnimation,
            Playing,
            Waiting,
            Finish,
            End
        }

        [SerializeField] private AlphaAnimator _alphaAnimator;
        [SerializeField] private List<Letter> _letters;
        [SerializeField] private GameObject _lettersContainer;
        [SerializeField] private LettersAnimator _letterAnimator;
        [SerializeField] private GameObject _music;
        [SerializeField] private float _finishGameTimer = 4.0f;

        [Header("Cheats")] [SerializeField] private bool _useCheats;
        [SerializeField] private int _startWithIndex;
        [SerializeField] private bool _startGameplay;

        private GameState _gameState = GameState.Animating;
        private int _letterIndex;
        private float _finishTimer;

        private void Awake()
        {
            _letters.ForEach(letter => letter.OnAwake());

            if (_useCheats && _startGameplay)
            {
                _letterIndex = _startWithIndex;
                _lettersContainer.SetActive(true);

                StartCoroutine(BreakAnimation());
            }
            else
            {
                _alphaAnimator.OnStart(() =>
                {
                    _lettersContainer.SetActive(true);
                    _letterAnimator.OnStart(() =>
                    {
                        _music.SetActive(true);
                        StartCoroutine(BreakAnimation());
                    });
                });
            }
        }

        private IEnumerator BreakAnimation()
        {
            _gameState = GameState.BreakAnimation;
            float timer = 1.0f; 

            while (timer >= 0.0f)
            {
                yield return new WaitForEndOfFrame();
                timer -= Time.deltaTime;
                Vector3 aux = Random.insideUnitCircle * 0.1f;
                _letters[_letterIndex].transform.position = aux + _letters[_letterIndex]._initialPosition;
            }
            
            _gameState = GameState.Playing;
            _letters[_letterIndex].transform.position = _letters[_letterIndex]._initialPosition;
            BreakNextLetter();
        }

        private void BreakNextLetter()
        {
            _letters[_letterIndex].Break(LetterRepaired);
        }

        private void LetterRepaired()
        {
            ResetCurrentLetter();

            if (_letterIndex >= _letters.Count - 1)
            {
                GameFinished();
                return;
            }

            _letterIndex++;
            StartCoroutine(BreakAnimation());
        }

        private void ResetCurrentLetter()
        {
            _letters[_letterIndex].Reset();
        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.Animating:
                    break;
                case GameState.Playing:
                    _letters[_letterIndex].OnUpdate();
                    break;
                case GameState.Waiting:
                    break;
                case GameState.Finish:
                    _letters.ForEach(MergeWithSky);
                    break;
                case GameState.End:
                    break;
                case GameState.BreakAnimation:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GameFinished()
        {
            Debug.Log("Game finish");
            _gameState = GameState.Waiting;
            Invoke(nameof(ChangeState), _finishGameTimer);
        }

        private void ChangeState()
        {
            _gameState = GameState.Finish;
        }

        private void MergeWithSky(Letter letter)
        {
            var color = letter._spriteRenderer.color;

            var alpha = Mathf.Lerp(1, 0, _finishTimer);
            color.a = alpha;
            letter._spriteRenderer.color = color;

            _finishTimer += Time.deltaTime/2;

            var scale = letter.transform.localScale;
            scale.x -= alpha;
            scale.y -= alpha;
            letter.transform.localScale = scale;

            
            if (_finishTimer >= 1.0f)
            {
                Debug.Log("FINISH");
                _gameState = GameState.Finish;
            }
        }
    }
}