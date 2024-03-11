using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    enum GameState
    {
        None = 0,
        MainMenu,
        Controls,
        Gameplay,
        Credits,
        Score,
        Exit
    }
    class Game
    {
        public static GameState _currentGameState = GameState.MainMenu;

        //para reiniciar al juego, hacer una funcion que resetee el score, las vidas, limpie los asteroides, reubique al player, y limpie las balas. 
        //Va en el menu principal, apretar "P" y llamar a ResetGameplay() para que el juego sea rejugable.

        Clock _deltaClock;
        Time _deltaTime;

        public static RenderWindow? _window;
        VideoMode _videoMode;

        uint _screenWidth = 800;
        uint _screenHeight = 600;

        Gameplay _gameplay;
        MainMenu _mainMenu;
        Controls _controls;
        Credits _credits;
        Score _score;

        public static bool _gameOver;
        public static bool _resetGame;
        public static float _gameScore = 0;
        public Game()
        {
            _deltaClock = new Clock();
            _deltaTime = new Time();

            _videoMode = new VideoMode(_screenWidth, _screenHeight);
            _window = new RenderWindow(_videoMode, "Space Crusader");

            _window.Closed += Window_Closed;
            _window.SetFramerateLimit(60);
            _window.SetVerticalSyncEnabled(true);

            _gameplay = new Gameplay(_window);
            _mainMenu = new MainMenu();
            _controls = new Controls();
            _credits = new Credits();
            _score = new Score();
        }

        public void Run()
        {
            while (_window.IsOpen)
            {
                _window.DispatchEvents();
                Update();
                Draw();
            }
        }       

        private void Window_Closed(object? sender, EventArgs e)
        {
            _window.Close();
        }
        public void Update()
        {
            _deltaTime = _deltaClock.Restart();

            switch (_currentGameState)
            {
                case GameState.MainMenu:
                    _mainMenu.Update(_window, _deltaTime);
                    ResetGameplay();
                    break;
                case GameState.Controls:
                    _controls.Update(_window, _deltaTime);
                    break;
                case GameState.Gameplay:
                    _gameplay.Update(_window, _deltaTime);
                    break;
                case GameState.Credits:
                    _credits.Update(_window, _deltaTime);
                    break;
                case GameState.Score:
                    _score.Update(_window, _deltaTime);
                    ResetGameplay();
                    break;
                case GameState.Exit:
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        _window.Close();
                    }
                    break;
                default:
                    break;
            }         
        }
        public void Draw()
        {
            _window.Clear();

            switch (_currentGameState)
            {
                case GameState.MainMenu:
                    _mainMenu.Draw(_window, _deltaTime);
                    break;
                case GameState.Controls:
                    _controls.Draw(_window, _deltaTime);
                    break;
                case GameState.Gameplay:
                    _gameplay.Draw(_window, _deltaTime);
                    break;
                case GameState.Credits:
                    _credits.Draw(_window, _deltaTime);
                    break;
                case GameState.Score:
                    _score.Draw(_window, _deltaTime);
                    break;
                case GameState.Exit:
                    break;
                default:
                    break;
            }           
            _window.Display();
        }
       
        public void ResetGameplay()
        {
            if (_resetGame)
            {
                _gameplay.ResetGameplay(_window);
                _resetGame = false;
            }
        }
    }

}
