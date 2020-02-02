using System;
using System.Collections;
using System.Collections.Generic;
using Letters;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private float _scaleVelocity = 2.0f;
        [SerializeField] private SpriteRenderer _backgroundSpriteRenderer;

        [Header("Shader")]
        [SerializeField] private float _shaderVelocity;
        [SerializeField] private float _shaderWaveLength;
        [SerializeField] [Range(0,0.15f)] private float _shaderPower;
        [SerializeField] private float _shaderSpeed;
        
        [Header("Cheats")] [SerializeField] private bool _useCheats;
        [SerializeField] private int _startWithIndex;
        [SerializeField] private bool _startGameplay;

        private GameState _gameState = GameState.Animating;
        private int _letterIndex;
        private float _finishTimer;
        private int _mergeWithSkyIndex;

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
                    MergeWithSky(_letters[_mergeWithSkyIndex]);
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

            _finishTimer += Time.deltaTime * _scaleVelocity;

            var scale = letter.transform.localScale;
            scale.x = alpha;
            scale.y = alpha;
            letter.transform.localScale = scale;

            if (_finishTimer >= 1.0f)
            {
                _mergeWithSkyIndex++;
                _finishTimer = 0;

                if (_mergeWithSkyIndex >= _letters.Count)
                {
                    GameEnd();
                }
            }
        }

        private void GameEnd()
        {
            UnityEngine.Debug.Log("Finished animating");
            _gameState = GameState.End;
            Invoke(nameof(GoToCredits), 1.0f);
            /*_backgroundSpriteRenderer.material.SetFloat("_UnityTime", Time.time);
            _backgroundSpriteRenderer.material.SetFloat("_Velocity", _shaderVelocity);
            _backgroundSpriteRenderer.material.SetFloat("_WaveLength", _shaderWaveLength);
            _backgroundSpriteRenderer.material.SetFloat("_Pow", _shaderPower);
            _backgroundSpriteRenderer.material.SetFloat("_Speed", _shaderSpeed);*/
        }

        private void GoToCredits()
        {
            SceneManager.LoadScene("Fin");
        }
    }
}