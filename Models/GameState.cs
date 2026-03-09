using System.Collections.Generic;

namespace HangmanGame.Models
{
    public class GameState
    {
        public string WordToGuess { get; set; }
        public HashSet<char> GuessedLetters { get; set; }
        public int WrongGuesses { get; set; }
        public int MaxWrongGuesses { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWon { get; set; }

        public GameState()
        {
            GuessedLetters = new HashSet<char>();
            WrongGuesses = 0;
            MaxWrongGuesses = 7;
            IsGameOver = false;
            IsWon = false;
        }

        public string GetDisplayWord()
        {
            if (string.IsNullOrEmpty(WordToGuess))
                return string.Empty;

            var display = new System.Text.StringBuilder();
            foreach (char c in WordToGuess.ToUpper())
            {
                if (GuessedLetters.Contains(c))
                    display.Append(c + " ");
                else
                    display.Append("_ ");
            }
            return display.ToString().Trim();
        }

        public bool IsWordComplete()
        {
            if (string.IsNullOrEmpty(WordToGuess))
                return false;

            foreach (char c in WordToGuess.ToUpper())
            {
                if (!GuessedLetters.Contains(c))
                    return false;
            }
            return true;
        }

        public int RemainingAttempts
        {
            get { return MaxWrongGuesses - WrongGuesses; }
        }
    }
}
