using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    class Player : GameObjectBase
    {
        float _hyperSpaceTimer = 0f;
        float _hyperSpaceCooldown = 1f;

        float _shootTimer = 0f;
        float _shootCooldown = 0.2f;

        float _acceleration = 100f;
        float _rotationSpeed = 250f;

        int _lives = 3;

        float _activationTimer = 0f;
        float _activationTime = 3f;

        float _blinkTimer = 0f;
        float _blinkCooldown = 0.1f;
        bool _blink = false;

        Vector2f accelerationVector;
        Vector2f _velocity = new Vector2f(0,0);

        List<Bullet> _bullets;

        Sound _shootSound;
        Sound _hyperSpaceSound;
        SoundBuffer _hyperSpaceSoundBuffer;
        SoundBuffer _shootSoundBuffer;

        public Player(float posX, float posY, float sizeX, float sizeY)
        {
            _body = new RectangleShape(new Vector2f(40, 40));

            _body.Origin = new Vector2f(_body.Size.X / 2, _body.Size.Y / 2);

            _body.Position = new Vector2f(posX, posY);

            _texture = new Texture("Assets/Objects.png");

            IntRect spriteRect = new IntRect(396,268,104,104);

            _sprite = new Sprite(_texture, spriteRect);

            _sprite.Origin = new Vector2f(_sprite.Position.X + (_sprite.TextureRect.Width/2), 
                _sprite.Position.Y + (_sprite.TextureRect.Width/2));

            _sprite.Scale = new Vector2f(0.35f, 0.35f);           

            float angleRad = (float)(_body.Rotation * Math.PI / 180.0f);

            accelerationVector = new Vector2f((float)Math.Sin(angleRad), -(float)Math.Cos(angleRad)) * _acceleration;

            _bullets = new List<Bullet>();

            _shootSoundBuffer = new SoundBuffer("Audio/laserSmall_000.ogg");

            _shootSound = new Sound(_shootSoundBuffer);

            _hyperSpaceSoundBuffer = new SoundBuffer("Audio/lowRandom.ogg");

            _hyperSpaceSound = new Sound(_hyperSpaceSoundBuffer);

            CollisionsHandler.Add(this);
        }

        public int GetLives()
        {
            return _lives;
        }

        public void SetLives(int lives)
        {
            _lives = lives;
        }

        public void LoseLife(RenderWindow window)
        {
            _lives--;
            if (_lives <= 0)
            {
                _lives = 0;
                Game._currentGameState = GameState.Score;
            }
            else
            {
                this.ResetPosition(window);
            }
        }

        public void ResetBullets()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                CollisionsHandler.Remove(_bullets[i]);
            }
            _bullets.Clear();
        }

        public void ResetPosition(RenderWindow window)
        {
            _body.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            _sprite.Position = _body.Position;
        }

        public override void SolveCollision(GameObjectBase other)
        {
 
        }

        public void PlayerInput(Time deltaTime, RectangleShape rectangle, RenderWindow playerWindow)
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {                
                float angleRad = (float)(_body.Rotation * Math.PI / 180.0f);

                accelerationVector = new Vector2f((float)Math.Sin(angleRad), -(float)Math.Cos(angleRad)) * _acceleration;
                
                _velocity += accelerationVector * deltaTime.AsSeconds();          
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                if (_hyperSpaceTimer > _hyperSpaceCooldown)
                {
                    _hyperSpaceSound.Play();
                    HyperSpaceMovement(playerWindow);
                    _hyperSpaceTimer = 0;
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                _body.Rotation -= _rotationSpeed * deltaTime.AsSeconds();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _body.Rotation += _rotationSpeed * deltaTime.AsSeconds();
            }            
            _body.Position += _velocity * deltaTime.AsSeconds();
            _sprite.Position = _body.Position;
            _sprite.Rotation = _body.Rotation;

            WrapCoordenates(rectangle, playerWindow);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                Shoot(deltaTime);                
            }
        }

        public void Shoot(Time deltaTime)
        {          
            if (_shootTimer >= _shootCooldown)
            {
                _bullets.Add(new Bullet(_body.Position, _body.Rotation));
                _shootSound.Play();
                _shootTimer = 0f;
            }
        }

        public void HyperSpaceMovement(RenderWindow playerWindow)
        {
            Random random = new Random();
            _body.Position = new Vector2f(random.Next(0, (int)playerWindow.Size.X), random.Next(0, (int)playerWindow.Size.Y));          
        }

        public List<Bullet> GetBullets ()
        {
            return _bullets;
        }

        public override void Update(Time deltaTime, RenderWindow playerWindow)
        {                      
            _hyperSpaceTimer += deltaTime.AsSeconds();
            _shootTimer += deltaTime.AsSeconds();

            PlayerInput(deltaTime, _body, playerWindow);
            
            if (!Game._gameOver && !_isActive)
            {
                _activationTimer += deltaTime.AsSeconds();
                if (_activationTimer >= _activationTime)
                {
                    _activationTimer -= _activationTime;
                    CollisionsHandler.Add(this);
                    _isActive = true;
                }
            }

            foreach (Bullet bullet in _bullets)
            {
                bullet.Update(deltaTime, playerWindow);
            }

            List<int> _indexesToRemove = new List<int>();

            //_bullets.RemoveAll(bullet => !IsInsideWindow(bullet.GetBody().Position, playerWindow));
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (!IsInsideWindow(_bullets[i].GetBody().Position, playerWindow))
                {
                    CollisionsHandler.Remove(_bullets[i]);
                    _indexesToRemove.Add(i);
                }
            }
            for (int i = 0; i < _indexesToRemove.Count; i++)
            {
                _bullets.Remove(_bullets[_indexesToRemove[i]]);
            }
        }

        public void Draw(RenderWindow window, Time deltaTime)
        {
            if (this._isActive)
            { 
            window.Draw(_sprite);
            }
            else
            {                
                _blinkTimer += deltaTime.AsSeconds();

                if (_blinkTimer >= _blinkCooldown)
                {      
                    _blinkTimer -= _blinkCooldown;
                    _blink = !_blink;                                            
                }   
                if (_blink)
                {
                    window.Draw(_sprite);
                }
            } 

            foreach (Bullet bullet in _bullets) 
            { 
                bullet.Draw(window);
            }
        }
    }
}
