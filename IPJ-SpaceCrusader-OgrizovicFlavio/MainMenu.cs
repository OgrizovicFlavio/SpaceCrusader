using SFML.Audio;
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
    enum MenuState
    {
        None = 0,
        Play,
        Controls,
        Credits,
        Exit
    }
    internal class MainMenu : Scene
    {
        public static MenuState _currentMenuState = MenuState.Play;

        Font _gameFont = new Font("PressStart2P-vaV7.ttf");
        Text _titleText;
        Text _playText;
        Text _controlsText;
        Text _creditsText;
        Text _exitText;

        public MainMenu()
        {
            _titleText = new Text(" ", _gameFont);
            _playText = new Text(" ", _gameFont);
            _controlsText = new Text(" ", _gameFont);
            _creditsText = new Text(" ", _gameFont);
            _exitText = new Text(" ", _gameFont);

            _titleText.FillColor = Color.Cyan;
            _titleText.CharacterSize = 50;
            FloatRect titleBounds = _titleText.GetLocalBounds();
            _titleText.Origin = new Vector2f(titleBounds.Width / 2, titleBounds.Height / 2);
            _titleText.Position = new Vector2f(80, 150);

            _playText.FillColor = Color.White;
            _playText.CharacterSize = 18;
            FloatRect playBounds = _titleText.GetLocalBounds();
            _playText.Origin = new Vector2f(playBounds.Width / 2, playBounds.Height / 2);
            _playText.Position = new Vector2f(75, 250);

            _controlsText.FillColor = Color.White;
            _controlsText.CharacterSize = 18;
            FloatRect controlsBounds = _controlsText.GetLocalBounds();
            _controlsText.Origin = new Vector2f(controlsBounds.Width / 2, controlsBounds.Height / 2);
            _controlsText.Position = new Vector2f(60, 300);

            _creditsText.FillColor = Color.White;
            _creditsText.CharacterSize = 18;
            FloatRect creditsBounds = _creditsText.GetLocalBounds();
            _creditsText.Origin = new Vector2f(creditsBounds.Width / 2, creditsBounds.Height / 2);
            _creditsText.Position = new Vector2f(60, 350);

            _exitText.FillColor = Color.White;
            _exitText.CharacterSize = 18;
            FloatRect exitBounds = _exitText.GetLocalBounds();
            _exitText.Origin = new Vector2f(exitBounds.Width / 2, exitBounds.Height / 2);
            _exitText.Position = new Vector2f(60, 400);

            Music.PlayMusic();
            _titleText.DisplayedString = "Space Crusader";
            _playText.DisplayedString = "* Presiona 'P' para jugar.";
            _controlsText.DisplayedString = "* Presiona 'C' para ver los controles.";
            _creditsText.DisplayedString = "* Presiona 'Tab' para ver los créditos.";
            _exitText.DisplayedString = "* Presiona 'Esc' para salir.";
        }

        public override void Update(RenderWindow window, Time deltaTime)
        {
            Music.PlayMusic();
            switch (_currentMenuState)
            {
                case MenuState.Play:
                    if (Keyboard.IsKeyPressed(Keyboard.Key.P))
                    {
                        Music.StopMusic();
                        Music.PlayMusic();
                        Game._currentGameState = GameState.Gameplay;
                        Game._resetGame = true;
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.C))
                    {
                        _currentMenuState = MenuState.Controls;
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
                    {
                        _currentMenuState = MenuState.Credits;
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        _currentMenuState = MenuState.Exit;
                    }
                    break;
                case MenuState.Controls:
                    Game._currentGameState = GameState.Controls;
                    break;
                case MenuState.Credits:
                    Game._currentGameState = GameState.Credits;
                    break;
                case MenuState.Exit:
                    window.Close();
                    break;
                default:
                    break;
            }
        }

        public override void Draw(RenderWindow window, Time deltaTime)
        {
            switch (_currentMenuState)
            {
                case MenuState.Play:
                    window.Draw(_titleText);
                    window.Draw(_playText);
                    window.Draw(_controlsText);
                    window.Draw(_creditsText);
                    window.Draw(_exitText);
                    break;
                case MenuState.Controls:
                    break;
                case MenuState.Credits:
                    break;
                case MenuState.Exit:
                    break;
                default:
                    break;
            }
        }
    }
}
