using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    internal class GameplayHUD
    {
        Font _gameFont = new Font("PressStart2P-vaV7.ttf");
        Text _playerScore;
        Text _playerLives;
            
        public GameplayHUD()
        {
            _playerScore = new Text(" ", _gameFont);
            _playerScore.FillColor = Color.Yellow;
            _playerScore.CharacterSize = 20;

            _playerLives = new Text(" ", _gameFont);
            _playerLives.FillColor = Color.Green;
            _playerLives.CharacterSize = 20;
            _playerLives.Position = new Vector2f(0f, 5f);
        }

        public void Update (Player myPlayer, Time deltaTime, RenderWindow window) 
        { 
            FloatRect textBounds = _playerScore.GetLocalBounds();

            _playerScore.DisplayedString = $"PUNTAJE: {Game._gameScore}";
            _playerScore.Position = new Vector2f(Math.Clamp(window.Size.X / 2 - textBounds.Width,
                                                      window.Size.X - textBounds.Width, 4000 - textBounds.Width), 5f);
            _playerLives.DisplayedString = $"VIDAS: {myPlayer.GetLives()}";
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(_playerLives);
            window.Draw(_playerScore);
        }
    }
}
