using System;
using HangmanGame.Models;

namespace HangmanGame.Game
{
    public class HangmanGameEngine
    {
        private GameState _currentState;
        private readonly WordProvider _wordProvider;

        // Events for UI communication
        public event EventHandler<GuessResult> OnLetterGuessed;
        public event EventHandler<string> OnGameWon;
        public event EventHandler<string> OnGameLost;

        public HangmanGameEngine()
        {
            _wordProvider = new WordProvider();
            _currentState = new GameState();
        }

        public void StartNewGame()
        {
            _currentState = new GameState
            {
                WordToGuess = _wordProvider.GetRandomWord(),
                MaxWrongGuesses = GameSettings.MaxWrongGuesses
            };
        }

        public GuessResult GuessLetter(char letter)
        {
            letter = char.ToUpper(letter);

            // Check if letter was already guessed
            if (_currentState.GuessedLetters.Contains(letter))
            {
                return CreateGuessResult(false, letter);
            }

            // Add letter to guessed letters
            _currentState.GuessedLetters.Add(letter);

            // Check if letter is in the word
            bool isCorrect = _currentState.WordToGuess.Contains(letter.ToString());

            if (!isCorrect)
            {
                _currentState.WrongGuesses++;
            }

            // Check win/loss conditions
            CheckGameStatus();

            var result = CreateGuessResult(isCorrect, letter);

            // Raise events
            OnLetterGuessed?.Invoke(this, result);

            if (_currentState.IsGameOver)
            {
                if (_currentState.IsWon)
                {
                    OnGameWon?.Invoke(this, _currentState.WordToGuess);
                }
                else
                {
                    OnGameLost?.Invoke(this, _currentState.WordToGuess);
                }
            }

            return result;
        }

        private void CheckGameStatus()
        {
            // Check if player won
            if (_currentState.IsWordComplete())
            {
                _currentState.IsGameOver = true;
                _currentState.IsWon = true;
            }
            // Check if player lost
            else if (_currentState.WrongGuesses >= _currentState.MaxWrongGuesses)
            {
                _currentState.IsGameOver = true;
                _currentState.IsWon = false;
            }
        }

        private GuessResult CreateGuessResult(bool isCorrect, char letter)
        {
            return new GuessResult
            {
                IsCorrect = isCorrect,
                GuessedLetter = letter,
                IsGameOver = _currentState.IsGameOver,
                IsWon = _currentState.IsWon,
                DisplayWord = _currentState.GetDisplayWord(),
                WrongGuesses = _currentState.WrongGuesses,
                RemainingAttempts = _currentState.RemainingAttempts
            };
        }

        public GameState GetCurrentState()
        {
            return _currentState;
        }

        public string GetDisplayWord()
        {
            return _currentState.GetDisplayWord();
        }

        public int GetWrongGuesses()
        {
            return _currentState.WrongGuesses;
        }

        public int GetRemainingAttempts()
        {
            return _currentState.RemainingAttempts;
        }

        public bool IsGameOver()
        {
            return _currentState.IsGameOver;
        }
    }
}
