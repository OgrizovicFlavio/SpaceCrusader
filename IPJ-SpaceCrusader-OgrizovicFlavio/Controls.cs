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
    internal class Controls : Scene
    {
        Font _gameFont = new Font("PressStart2P-vaV7.ttf");
        Text _shootText;
        Text _movementText;
        Text _jumpText;
        Text _aditionalText;
        Text _exitText;

        public Controls ()
        {
            _shootText = new Text("", _gameFont);
            _movementText = new Text("", _gameFont);
            _jumpText = new Text("", _gameFont);
            _aditionalText = new Text("", _gameFont);
            _exitText = new Text("", _gameFont);

            _shootText.FillColor = Color.White;
            _movementText.FillColor = Color.White;
            _jumpText.FillColor = Color.White;
            _aditionalText.FillColor = Color.White;
            _exitText.FillColor = Color.White;

            _shootText.CharacterSize = 14;
            _movementText.CharacterSize = 14;
            _jumpText.CharacterSize = 14;
            _aditionalText.CharacterSize = 14;
            _exitText.CharacterSize = 14;

            FloatRect shootBounds = _shootText.GetLocalBounds();
            FloatRect movementBounds = _movementText.GetLocalBounds();
            FloatRect jumpBounds = _jumpText.GetLocalBounds();
            FloatRect aditionalBounds = _movementText.GetLocalBounds();
            FloatRect exitBounds = _exitText.GetLocalBounds();

            _shootText.Origin = new Vector2f(shootBounds.Width/2, shootBounds.Height/2);
            _movementText.Origin = new Vector2f(movementBounds.Width/2, movementBounds.Height/2);
            _jumpText.Origin = new Vector2f(jumpBounds.Width/2, jumpBounds.Height/2);
            _aditionalText.Origin = new Vector2f(aditionalBounds.Width/2, aditionalBounds.Height/2);
            _exitText.Origin = new Vector2f(exitBounds.Width/2, exitBounds.Height/2);

            _shootText.Position = new Vector2f(10,150);
            _movementText.Position = new Vector2f(10, 200);
            _jumpText.Position = new Vector2f(10, 250);
            _aditionalText.Position = new Vector2f(10, 300);
            _exitText.Position = new Vector2f(10, 350);

            _movementText.DisplayedString = "* Presiona 'W' para acelerar, y 'A' y 'D' para rotar.";
            _shootText.DisplayedString = "* Presiona 'Espacio' para disparar.";
            _jumpText.DisplayedString = "* Presiona 'S' para esquivar los asteroides.";
            _aditionalText.DisplayedString = "* Ganas puntos al destruir asteroides.";
            _exitText.DisplayedString = "* Presiona 'Q' para volver al menú principal.";

        }
        public override void Update(RenderWindow window, Time deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                Game._currentGameState = GameState.MainMenu;
                MainMenu._currentMenuState = MenuState.Play;
            }
        }

        public override void Draw(RenderWindow window, Time deltaTime)
        {
            window.Draw(_movementText);
            window.Draw(_shootText);
            window.Draw(_jumpText);
            window.Draw(_aditionalText);
            window.Draw(_exitText);
        }
    }
}
