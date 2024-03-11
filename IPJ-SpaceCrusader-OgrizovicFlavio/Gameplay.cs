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
    internal class Gameplay : Scene
    {
        Player _myPlayer;
        List<Asteroid> _asteroids;
        GameplayHUD _screenData;
        float _asteroidSpawnTimer = 0f;
        float _asteroidSpawnInterval = 2f;

        public Gameplay(RenderWindow window)
        {
            _myPlayer = new Player(window.Size.X / 2, window.Size.Y / 2, 100, 100);
            _asteroids = new List<Asteroid>();
            _screenData = new GameplayHUD();
        }

        public void ResetGameplay(RenderWindow window)
        {
            Game._gameScore = 0;
            _myPlayer.SetLives(3);
            _myPlayer.ResetPosition(window);
            _myPlayer.ResetBullets();
            for (int i = 0; i < _asteroids.Count; i++)
            {
                CollisionsHandler.Remove(_asteroids[i]);
            }
            _asteroids.Clear();
        }

        public void SpawnAsteroids(RenderWindow window, Time deltaTime, int asteroidSizeX, int asteroidSizeY)
        {
            _asteroidSpawnTimer += deltaTime.AsSeconds();

            if (_asteroidSpawnTimer > _asteroidSpawnInterval)
            {
                Random random = new Random();

                for (int i = 0; i < random.Next(1, 2); i++)
                {

                    Vector2f velocity = new Vector2f((float)random.NextDouble() * 100 - 50, (float)random.NextDouble() * 100 - 50);
                    Vector2f position = new Vector2f();

                    int windowSide = random.Next(4);

                    switch (windowSide)
                    {
                        case 0: //Lado izquierdo de la ventana.
                            position = new Vector2f(-(asteroidSizeX / 2), random.Next(0, (int)window.Size.Y));
                            break;
                        case 1: //Lado derecho de la ventana.
                            position = new Vector2f(window.Size.X + (asteroidSizeX / 2), random.Next(0, (int)window.Size.Y));
                            break;
                        case 2: //Lado superior de la ventana.
                            position = new Vector2f(random.Next(0, (int)window.Size.X), -(asteroidSizeY / 2));
                            break;
                        case 3: //Lado inferior de la ventana.
                            position = new Vector2f(random.Next(0, (int)window.Size.X), window.Size.Y + (asteroidSizeY / 2));
                            break;
                    }

                    Asteroid asteroid = new Asteroid(position, velocity, asteroidSizeX, asteroidSizeY);

                    asteroid.AimTowardsPlayer(_myPlayer.GetBody().Position);

                    _asteroids.Add(asteroid);
                }
                _asteroidSpawnTimer = 0f;
            }
        }

        public override void Update(RenderWindow window, Time deltaTime)
        {
            _myPlayer.Update(deltaTime, window);

            _screenData.Update(_myPlayer, deltaTime, window);

            foreach (Asteroid asteroid in _asteroids)
            {
                asteroid.Update(deltaTime, window);

            }

            SpawnAsteroids(window, deltaTime, 70, 70);

            CollisionsHandler.Update();

            _asteroids.RemoveAll(asteroid => !asteroid.GetActive());
        }
        public override void Draw(RenderWindow window, Time deltaTime)
        {
            _myPlayer.Draw(window, deltaTime);

            foreach (Asteroid asteroid in _asteroids)
            {
                asteroid.Draw(window);
            }

            _screenData.Draw(window);
        }
       
    }
}
