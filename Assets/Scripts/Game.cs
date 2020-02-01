using System.Collections.Generic;
using UnityEngine;

namespace Letters
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<LetterR> _letters;

        private int _letterIndex;
        
        private void Awake()
        {
            BreakNextLetter();
        }

        private void BreakNextLetter()
        {
            _letters[_letterIndex].Break(LetterRepaired);
        }

        private void LetterRepaired()
        {
            _letterIndex++;
            if (_letterIndex >= _letters.Count)
            {
                GameFinished();
                return;
            }

            UnityEngine.Debug.Log("Next letter");
            BreakNextLetter();
        }

        private void GameFinished()
        {
            Debug.Log("Game won");
        }
    }
}