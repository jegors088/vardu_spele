using HangmanGame.Game;

namespace HangmanGame.Services
{
    public class ScoreManager
    {
        private int _currentScore;
        private int _gamesWon;
        private int _gamesLost;

        public int CurrentScore => _currentScore;
        public int GamesWon => _gamesWon;
        public int GamesLost => _gamesLost;

        public ScoreManager()
        {
            Reset();
        }

        public void AddCorrectLetterPoints()
        {
            _currentScore += GameSettings.PointsPerCorrectLetter;
        }

        public void AddWinPoints()
        {
            _currentScore += GameSettings.PointsPerWin;
            _gamesWon++;
        }

        public void RecordLoss()
        {
            _gamesLost++;
        }

        public void Reset()
        {
            _currentScore = 0;
            _gamesWon = 0;
            _gamesLost = 0;
        }
    }
}
