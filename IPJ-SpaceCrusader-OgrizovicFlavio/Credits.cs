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
    internal class Credits : Scene
    {
        Font _gameFont = new Font("PressStart2P-vaV7.ttf");
        Text _gameText;
        Text _artSoundText;
        Text _fontText;
        Text _musicText;
        Text _exitText;

        public Credits ()
        {
            _gameText = new Text("", _gameFont);
            _artSoundText = new Text("", _gameFont);
            _fontText = new Text("", _gameFont);
            _musicText = new Text("", _gameFont);
            _exitText = new Text("", _gameFont);

            _gameText.FillColor = Color.White;
            _artSoundText.FillColor = Color.White;
            _fontText.FillColor = Color.White;
            _musicText.FillColor = Color.White;
            _exitText.FillColor = Color.White;

            _gameText.CharacterSize = 16;
            _artSoundText.CharacterSize = 14;
            _fontText.CharacterSize = 14;
            _musicText.CharacterSize = 13;
            _exitText.CharacterSize = 14;

            FloatRect gameBounds = _gameText.GetLocalBounds();
            FloatRect artBounds = _artSoundText.GetLocalBounds();
            FloatRect fontBounds = _fontText.GetLocalBounds();
            FloatRect musicBounds = _musicText.GetLocalBounds();
            FloatRect exitBounds = _exitText.GetLocalBounds();

            _gameText.Origin = new Vector2f(gameBounds.Width/2, gameBounds.Height/2);
            _artSoundText.Origin = new Vector2f(artBounds.Width/2, artBounds.Height/2);
            _fontText.Origin = new Vector2f(fontBounds.Width/2, fontBounds.Height/2);
            _musicText.Origin = new Vector2f(musicBounds.Width/2, musicBounds.Height/2);
            _exitText.Origin = new Vector2f(exitBounds.Width/2, exitBounds.Height/2);

            _gameText.Position = new Vector2f(10,175);
            _artSoundText.Position = new Vector2f(10, 225);
            _fontText.Position = new Vector2f(10, 275);
            _musicText.Position = new Vector2f(10, 325);
            _exitText.Position = new Vector2f(10, 375);

            _gameText.DisplayedString = "* Creado por Flavio Ogrizovic.";
            _artSoundText.DisplayedString = "* Arte 2D y sonido: kenney.nl/assets";
            _fontText.DisplayedString = "* Fuente: 'Press Start 2P' de codeman38 (fontspace.com).";
            _musicText.DisplayedString = "* Música: 'Dark Matter' de Myrgharok (indiegamemusic.com).";
            _exitText.DisplayedString = "* Presiona 'Q' para volver al menú.";
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
            window.Draw(_gameText);
            window.Draw(_artSoundText);
            window.Draw(_fontText);
            window.Draw(_musicText);
            window.Draw(_exitText);          
        }
    }
}
