using System;
using System.Drawing;
using System.Windows.Forms;
using HangmanGame.Controls;
using HangmanGame.Game;
using HangmanGame.Models;
using HangmanGame.Services;

namespace HangmanGame.UI
{
    public partial class MainForm : Form
    {
        private HangmanGameEngine _gameEngine;
        private ScoreManager _scoreManager;
        private LetterButton[] _letterButtons;
        private HangmanCanvas _hangmanCanvas;

        // UI Controls
        private Label _titleLabel;
        private Label _wordDisplayLabel;
        private Label _wrongGuessesLabel;
        private Label _attemptsLabel;
        private Label _scoreLabel;
        private Button _newGameButton;
        private Button _restartButton;
        private Panel _letterPanel;
        private Panel _infoPanel;
        private Panel _canvasPanel;

        public MainForm()
        {
            InitializeComponent();
            InitializeGame();
            InitializeCustomUI();
            StartNewGame();
        }

        private void InitializeGame()
        {
            _gameEngine = new HangmanGameEngine();
            _scoreManager = new ScoreManager();

            // Subscribe to game events
            _gameEngine.OnLetterGuessed += GameEngine_OnLetterGuessed;
            _gameEngine.OnGameWon += GameEngine_OnGameWon;
            _gameEngine.OnGameLost += GameEngine_OnGameLost;
        }

        private void InitializeCustomUI()
        {
            // Form settings
            Text = "Karātuves spēle";
            Size = new Size(950, 780);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(240, 242, 245);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Title Label with gradient effect
            _titleLabel = new Label
            {
                Text = "K A R Ā T U V E S   S P Ē L E",
                Font = new Font("Bahnschrift", 32F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 39, 73),
                AutoSize = false,
                Size = new Size(930, 70),
                Location = new Point(10, 15),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(_titleLabel);

            // Canvas Panel with shadow effect
            _canvasPanel = new Panel
            {
                Location = new Point(30, 100),
                Size = new Size(420, 420),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            _canvasPanel.Paint += (s, e) =>
            {
                // Draw shadow
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, 5, 5, _canvasPanel.Width - 5, _canvasPanel.Height - 5);
                }
                // Draw border
                using (Pen borderPen = new Pen(Color.FromArgb(220, 224, 230), 2))
                {
                    e.Graphics.DrawRectangle(borderPen, 0, 0, _canvasPanel.Width - 1, _canvasPanel.Height - 1);
                }
            };
            Controls.Add(_canvasPanel);

            // Hangman Canvas
            _hangmanCanvas = new HangmanCanvas
            {
                Location = new Point(10, 10),
                Size = new Size(400, 400),
                BackColor = Color.White
            };
            _canvasPanel.Controls.Add(_hangmanCanvas);

            // Word Display Label
            _wordDisplayLabel = new Label
            {
                Text = "_ _ _ _ _",
                Font = new Font("Consolas", 36F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 39, 73),
                AutoSize = false,
                Size = new Size(450, 90),
                Location = new Point(470, 110),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(255, 255, 255)
            };
            _wordDisplayLabel.Paint += (s, e) =>
            {
                // Draw subtle border
                using (Pen borderPen = new Pen(Color.FromArgb(220, 224, 230), 2))
                {
                    e.Graphics.DrawRectangle(borderPen, 0, 0, _wordDisplayLabel.Width - 1, _wordDisplayLabel.Height - 1);
                }
            };
            Controls.Add(_wordDisplayLabel);

            // Info Panel with modern gradient
            _infoPanel = new Panel
            {
                Location = new Point(470, 220),
                Size = new Size(450, 170),
                BackColor = Color.FromArgb(88, 86, 214),
                BorderStyle = BorderStyle.None
            };
            _infoPanel.Paint += (s, e) =>
            {
                // Gradient background
                using (System.Drawing.Drawing2D.LinearGradientBrush brush = 
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                        _infoPanel.ClientRectangle,
                        Color.FromArgb(88, 86, 214),
                        Color.FromArgb(58, 123, 213),
                        45f))
                {
                    e.Graphics.FillRectangle(brush, _infoPanel.ClientRectangle);
                }
            };
            Controls.Add(_infoPanel);

            _wrongGuessesLabel = new Label
            {
                Text = "Nepareizie minējumi: 0",
                Font = new Font("Bahnschrift", 15F, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(430, 35),
                Location = new Point(15, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            _infoPanel.Controls.Add(_wrongGuessesLabel);

            _attemptsLabel = new Label
            {
                Text = "Atlikušie mēģinājumi: 7",
                Font = new Font("Bahnschrift", 15F, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(430, 35),
                Location = new Point(15, 65),
                TextAlign = ContentAlignment.MiddleLeft
            };
            _infoPanel.Controls.Add(_attemptsLabel);

            _scoreLabel = new Label
            {
                Text = "Punkti: 0",
                Font = new Font("Bahnschrift", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 234, 167),
                AutoSize = false,
                Size = new Size(430, 35),
                Location = new Point(15, 115),
                TextAlign = ContentAlignment.MiddleLeft
            };
            _infoPanel.Controls.Add(_scoreLabel);

            // Letter Panel
            _letterPanel = new Panel
            {
                Location = new Point(30, 530),
                Size = new Size(890, 190),
                BackColor = Color.Transparent
            };
            Controls.Add(_letterPanel);

            CreateLetterButtons();

            // New Game Button with modern style
            _newGameButton = new Button
            {
                Text = "Jauna Spēle",
                Font = new Font("Bahnschrift", 13F, FontStyle.Bold),
                Size = new Size(180, 50),
                Location = new Point(470, 410),
                BackColor = Color.FromArgb(72, 219, 151),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _newGameButton.FlatAppearance.BorderSize = 0;
            _newGameButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 199, 131);
            _newGameButton.Click += NewGameButton_Click;
            Controls.Add(_newGameButton);

            // Restart Button with modern style
            _restartButton = new Button
            {
                Text = "Sākt No Jauna",
                Font = new Font("Bahnschrift", 13F, FontStyle.Bold),
                Size = new Size(180, 50),
                Location = new Point(670, 410),
                BackColor = Color.FromArgb(255, 107, 107),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _restartButton.FlatAppearance.BorderSize = 0;
            _restartButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 87, 87);
            _restartButton.Click += RestartButton_Click;
            Controls.Add(_restartButton);
        }

        private void CreateLetterButtons()
        {
            string alphabet = "AĀBCČDEĒFGĢHIĪJKĶLĻMNŅOPRSŠTUŪVZŽ";
            _letterButtons = new LetterButton[alphabet.Length];

            int buttonSize = 50;
            int spacing = 10;
            int buttonsPerRow = 11;

            // Calculate centered starting X position
            int totalWidth = buttonsPerRow * buttonSize + (buttonsPerRow - 1) * spacing;
            int startX = (_letterPanel.Width - totalWidth) / 2;

            for (int i = 0; i < alphabet.Length; i++)
            {
                char letter = alphabet[i];

                int row = i / buttonsPerRow;
                int col = i % buttonsPerRow;

                LetterButton btn = new LetterButton
                {
                    Text = letter.ToString(),
                    Letter = letter,
                    Size = new Size(buttonSize, buttonSize),
                    Location = new Point(
                        startX + col * (buttonSize + spacing),
                        row * (buttonSize + spacing)
                    )
                };

                btn.Click += LetterButton_Click;

                _letterButtons[i] = btn;
                _letterPanel.Controls.Add(btn);
            }
        }

        private void LetterButton_Click(object sender, EventArgs e)
        {
            if (_gameEngine.IsGameOver())
                return;

            LetterButton btn = sender as LetterButton;
            if (btn != null)
            {
                btn.Enabled = false;
                _gameEngine.GuessLetter(btn.Letter);
            }
        }

        private void GameEngine_OnLetterGuessed(object sender, GuessResult result)
        {
            // Update UI
            _wordDisplayLabel.Text = result.DisplayWord;
            _wrongGuessesLabel.Text = $"Nepareizie minējumi: {result.WrongGuesses}";
            _attemptsLabel.Text = $"Atlikušie mēģinājumi: {result.RemainingAttempts}";
            _hangmanCanvas.WrongGuesses = result.WrongGuesses;

            // Update score
            if (result.IsCorrect)
            {
                _scoreManager.AddCorrectLetterPoints();
                _scoreLabel.Text = $"Punkti: {_scoreManager.CurrentScore}";
            }
        }

        private void GameEngine_OnGameWon(object sender, string word)
        {
            _scoreManager.AddWinPoints();
            _scoreLabel.Text = $"Punkti: {_scoreManager.CurrentScore}";
            
            MessageBox.Show($"Apsveicam! Jūs uzvarējāt!\n\nVārds bija: {word}\n\nPunkti: {_scoreManager.CurrentScore}", 
                "Uzvara!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            DisableAllLetterButtons();
        }

        private void GameEngine_OnGameLost(object sender, string word)
        {
            _scoreManager.RecordLoss();
            
            MessageBox.Show($"Spēle beigusies!\n\nVārds bija: {word}\n\nPunkti: {_scoreManager.CurrentScore}", 
                "Zaudējums", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            DisableAllLetterButtons();
            _wordDisplayLabel.Text = word;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            _scoreManager.Reset();
            StartNewGame();
        }

        private void StartNewGame()
        {
            _gameEngine.StartNewGame();
            _hangmanCanvas.Reset();
            
            _wordDisplayLabel.Text = _gameEngine.GetDisplayWord();
            _wrongGuessesLabel.Text = "Nepareizie minējumi: 0";
            _attemptsLabel.Text = "Atlikušie mēģinājumi: 7";
            _scoreLabel.Text = $"Punkti: {_scoreManager.CurrentScore}";

            EnableAllLetterButtons();
        }

        private void EnableAllLetterButtons()
        {
            foreach (var btn in _letterButtons)
            {
                btn.Enabled = true;
            }
        }

        private void DisableAllLetterButtons()
        {
            foreach (var btn in _letterButtons)
            {
                btn.Enabled = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
