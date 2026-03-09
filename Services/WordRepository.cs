using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame.Services
{
    public class WordRepository
    {
        private List<string> _words;
        private const string WordsFileName = "Words.txt";

        public WordRepository()
        {
            LoadWords();
        }

        private void LoadWords()
        {
            _words = new List<string>();

            try
            {
                string wordsPath = Path.Combine("Resources", WordsFileName);

                // Try multiple paths
                if (!File.Exists(wordsPath))
                {
                    wordsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", WordsFileName);
                }

                if (File.Exists(wordsPath))
                {
                    var lines = File.ReadAllLines(wordsPath);
                    _words = lines
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Select(line => line.Trim().ToUpper())
                        .ToList();
                }
                else
                {
                    // Fallback words if file not found
                    LoadFallbackWords();
                }
            }
            catch (Exception)
            {
                LoadFallbackWords();
            }
        }

        private void LoadFallbackWords()
        {
            _words = new List<string>
            {
                "COMPUTER", "PROGRAMMING", "DEVELOPER", "SOFTWARE", "KEYBOARD",
                "MONITOR", "INTERNET", "DATABASE", "ALGORITHM", "FUNCTION"
            };
        }

        public List<string> GetAllWords()
        {
            return _words;
        }
    }
}
