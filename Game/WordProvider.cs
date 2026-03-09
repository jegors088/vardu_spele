using System;
using HangmanGame.Services;

namespace HangmanGame.Game
{
    public class WordProvider
    {
        private readonly WordRepository _wordRepository;
        private readonly Random _random;

        public WordProvider()
        {
            _wordRepository = new WordRepository();
            _random = new Random();
        }

        public string GetRandomWord()
        {
            var words = _wordRepository.GetAllWords();
            if (words.Count == 0)
            {
                return "HANGMAN"; // Fallback word
            }

            int index = _random.Next(words.Count);
            return words[index].ToUpper();
        }
    }
}
