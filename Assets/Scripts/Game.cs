using System.Collections.Generic;
using Letters;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private List<Letter> _letters;

    [Header("Cheats")] 
    [SerializeField] private bool _useCheats;
    [SerializeField] private int _startWithIndex;

    private int _letterIndex;
    private bool _finished;
    
    private void Start()
    {
        BreakNextLetter();
    }

    private void BreakNextLetter()
    {
        if (_useCheats)
            _letterIndex = _startWithIndex;

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

        Debug.Log("Next letter");
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