namespace HangmanGame.Models
{
    public class GuessResult
    {
        public bool IsCorrect { get; set; }
        public char GuessedLetter { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWon { get; set; }
        public string DisplayWord { get; set; }
        public int WrongGuesses { get; set; }
        public int RemainingAttempts { get; set; }

        public GuessResult()
        {
            IsCorrect = false;
            IsGameOver = false;
            IsWon = false;
            DisplayWord = string.Empty;
        }
    }
}
