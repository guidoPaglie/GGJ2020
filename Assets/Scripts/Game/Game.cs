using System;
using System.Collections.Generic;
using Letters;
using UnityEngine;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private AlphaAnimator _alphaAnimator;
        [SerializeField] private List<Letter> _letters;
        [SerializeField] private GameObject _lettersContainer;

        [Header("Cheats")] 
        [SerializeField] private bool _useCheats;
        [SerializeField] private int _startWithIndex;
        [SerializeField] private bool _startGameplay;

        private int _letterIndex;
        private bool _finished;
        private bool _gameplayStared;
        
        private void Awake()
        {
            _letters.ForEach(letter => letter.OnAwake());
            
            if (_useCheats && _startGameplay)
            {
                _letterIndex = _startWithIndex;
                _lettersContainer.SetActive(true);
                _gameplayStared = true;
                BreakNextLetter();
            }
            else
            {
                _alphaAnimator.OnStart();
            }
        }

        private void BreakNextLetter()
        {
            _letters[_letterIndex].Break(LetterRepaired);
        }

        private void Update()
        {
            if (_finished)
                return;

            _letters[_letterIndex].OnUpdate();
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

        private void GameFinished()
        {
            _finished = true;
            Debug.Log("Game won");
        }
    }
}