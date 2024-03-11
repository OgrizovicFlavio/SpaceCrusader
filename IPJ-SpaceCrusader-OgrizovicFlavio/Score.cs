using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    internal class Score : Scene
    {
        Font _gameFont = new Font("PressStart2P-vaV7.ttf");
        Text _scoreText;
        Text _replayText;
        Text _exitText;
        public Score ()
        {
            _scoreText = new Text("", _gameFont);

            _scoreText = new Text("", _gameFont);
            _replayText = new Text("", _gameFont);
            _exitText = new Text("", _gameFont);

            _scoreText.FillColor = Color.Yellow;
            _replayText.FillColor = Color.White;
            _exitText.FillColor = Color.White;

            _scoreText.CharacterSize = 25;
            _replayText.CharacterSize = 15;
            _exitText.CharacterSize = 15;

            FloatRect scoreBounds = _scoreText.GetLocalBounds();
            FloatRect replayBounds = _replayText.GetLocalBounds();
            FloatRect exitBounds = _exitText.GetLocalBounds();

            _scoreText.Origin = new Vector2f(scoreBounds.Width / 2, scoreBounds.Height / 2);
            _replayText.Origin = new Vector2f(replayBounds.Width / 2, replayBounds.Height / 2);
            _exitText.Origin = new Vector2f(exitBounds.Width / 2, exitBounds.Height / 2);

            _scoreText.Position = new Vector2f(20, 175);
            _replayText.Position = new Vector2f(20, 250);
            _exitText.Position = new Vector2f(20, 300);

            _replayText.DisplayedString = "* Presiona 'P' para volver a jugar.";
            _exitText.DisplayedString = "* Presiona 'Q' para volver al menú.";
        }
        public override void Update(RenderWindow window, Time deltaTime)
        {
            _scoreText.DisplayedString = $"PUNTAJE FINAL: {Game._gameScore}";
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                Game._currentGameState = GameState.Gameplay;
                Game._resetGame = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                Game._currentGameState = GameState.MainMenu;
                MainMenu._currentMenuState = MenuState.Play;
            }
        }
        public override void Draw(RenderWindow window, Time deltaTime)
        {
            window.Draw(_scoreText);
            window.Draw(_replayText);
            window.Draw(_exitText);
        }
    }
}
