using System;
using System.Collections.Generic;
using Letters;
using UnityEngine;

namespace Game
{
    public class Game : MonoBehaviour
    {
        private enum GameState
        {
            Animating,
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

                BreakNextLetter();
            }
            else
            {
                _alphaAnimator.OnStart(() =>
                {
                    _lettersContainer.SetActive(true);
                    _letterAnimator.OnStart(() =>
                    {
                        _gameState = GameState.Playing;
                        _music.SetActive(true);
                        BreakNextLetter();
                    });
                });
            }
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
            BreakNextLetter();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GameFinished()
        {
            UnityEngine.Debug.Log("Game finish");
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