# Hangman Game - Windows Forms Application

A complete, production-quality Hangman game built with C#, Windows Forms, and .NET Framework 4.8.

## Features

- **Clean Architecture**: Separation between UI, game logic, models, and services
- **Custom Controls**: Styled letter buttons and animated hangman canvas using GDI+
- **Modern UI**: Polished interface with proper spacing, colors, and layout
- **Game Features**:
  - Random word selection from 150+ words
  - Visual hangman drawing with 7 stages
  - Letter guessing with A-Z buttons
  - Score tracking system
  - Win/Loss detection
  - New Game and Restart functionality
  - Remaining attempts display

## Technical Stack

- **Language**: C#
- **Framework**: .NET Framework 4.8
- **UI Technology**: Windows Forms (WinForms)
- **Graphics**: GDI+ (System.Drawing)
- **IDE**: Visual Studio 2019 / 2022

## Project Structure

```
HangmanGame/
├── HangmanGame.sln              # Solution file
├── HangmanGame.csproj           # Project file
├── App.config                   # Application configuration
├── Program.cs                   # Entry point
├── UI/
│   ├── MainForm.cs              # Main game form
│   ├── MainForm.Designer.cs     # Form designer code
│   └── MainForm.resx            # Form resources
├── Controls/
│   ├── LetterButton.cs          # Custom styled button
│   └── HangmanCanvas.cs         # Hangman drawing control
├── Game/
│   ├── HangmanGameEngine.cs     # Core game logic
│   ├── WordProvider.cs          # Word selection
│   └── GameSettings.cs          # Game configuration
├── Models/
│   ├── GameState.cs             # Game state model
│   └── GuessResult.cs           # Guess result model
├── Services/
│   ├── WordRepository.cs        # Word loading service
│   └── ScoreManager.cs          # Score tracking service
├── Resources/
│   └── Words.txt                # Word list (150+ words)
└── Properties/
    ├── AssemblyInfo.cs
    ├── Resources.resx
    ├── Resources.Designer.cs
    ├── Settings.settings
    └── Settings.Designer.cs
```

## HOW TO RUN THE PROJECT

### Step 1: Verify Prerequisites

Ensure you have one of the following installed:
- Visual Studio 2019 (with .NET Framework 4.8)
- Visual Studio 2022 (with .NET Framework 4.8)

### Step 2: Open the Solution

1. Navigate to the `HangmanGame` folder
2. Double-click `HangmanGame.sln` to open it in Visual Studio

### Step 3: Build the Project

1. In Visual Studio, go to **Build** → **Build Solution** (or press `Ctrl+Shift+B`)
2. Wait for the build to complete successfully
3. Check the Output window for any errors (there should be none)

### Step 4: Run the Game

1. Press **F5** or click the **Start** button in Visual Studio
2. The Hangman game window will appear
3. Start playing!

## How to Play

1. **Start**: The game automatically starts with a random word
2. **Guess Letters**: Click on letter buttons (A-Z) to guess
3. **Track Progress**: 
   - Watch the hangman drawing appear with each wrong guess
   - See the word reveal as you guess correct letters
   - Monitor your remaining attempts and score
4. **Win/Lose**: 
   - Win by guessing all letters before running out of attempts
   - Lose if the hangman is completed (7 wrong guesses)
5. **New Game**: Click "New Game" to play with a new word
6. **Restart**: Click "Restart" to reset your score and start fresh

## Game Architecture

### UI Layer
- `MainForm`: Main game window with all UI components
- `LetterButton`: Custom button with rounded corners and hover effects
- `HangmanCanvas`: Custom control for drawing the hangman figure

### Game Engine Layer
- `HangmanGameEngine`: Core game logic, rules, and state management
- `WordProvider`: Provides random words for gameplay
- `GameSettings`: Configuration constants

### Model Layer
- `GameState`: Represents current game state
- `GuessResult`: Result of each letter guess

### Service Layer
- `WordRepository`: Loads and manages word list
- `ScoreManager`: Tracks score and game statistics

## Customization

### Adding More Words
Edit `Resources/Words.txt` and add one word per line (uppercase recommended).

### Changing Game Difficulty
Modify `GameSettings.cs`:
```csharp
public const int MaxWrongGuesses = 7; // Change this value
```

### Adjusting Colors
Modify color values in `MainForm.cs` and `LetterButton.cs`:
```csharp
Color.FromArgb(52, 152, 219) // Blue
Color.FromArgb(46, 204, 113) // Green
Color.FromArgb(230, 126, 34) // Orange
```

## Troubleshooting

### Build Errors
- Ensure .NET Framework 4.8 is installed
- Clean and rebuild: **Build** → **Clean Solution**, then **Build** → **Build Solution**

### Words.txt Not Found
- Verify `Words.txt` is in the `Resources` folder
- Check that "Copy to Output Directory" is set to "PreserveNewest" in file properties

### Form Not Displaying Correctly
- Ensure you're running on Windows with display scaling at 100% or 125%
- The form is designed for minimum resolution of 1024x768

## License

This project is provided as-is for educational and personal use.

## Credits

Developed as a demonstration of clean C# architecture and Windows Forms development.
