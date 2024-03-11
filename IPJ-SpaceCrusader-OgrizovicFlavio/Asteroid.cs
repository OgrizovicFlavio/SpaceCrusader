using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    class Asteroid : GameObjectBase
    {
        Vector2f _velocity;
        float _maxSpeed = 50;
        float _asteroidPoints = 10;
        Sound _crashBulletSound;
        Sound _crashPlayerSound;
        SoundBuffer _crashPlayerSoundBuffer;
        SoundBuffer _crashBulletSoundBuffer;

        public Asteroid(Vector2f position, Vector2f velocity, int asteroidSizeX, int asteroidSizeY)
        {
            _body = new RectangleShape(new Vector2f(asteroidSizeX, asteroidSizeY));
            _body.Origin = new Vector2f(_body.Size.X / 2, _body.Size.Y / 2);
            _body.Position = position;
            _velocity = velocity;
            _texture = new Texture("Assets/Objects.png");

            IntRect spriteRect = new IntRect(272,528, 96, 96);

            _sprite = new Sprite(_texture, spriteRect);

            _sprite.Origin = new Vector2f(_sprite.Position.X + (_sprite.TextureRect.Width / 2),
                _sprite.Position.Y + (_sprite.TextureRect.Width / 2));

            _sprite.Scale = new Vector2f(1, 1);

            _sprite.Position = _body.Position;

            _crashBulletSoundBuffer = new SoundBuffer("Audio/explosionCrunch_000.ogg");

            _crashBulletSound = new Sound(_crashBulletSoundBuffer);

            _crashPlayerSoundBuffer = new SoundBuffer("Audio/explosionCrunch_003.ogg");

            _crashPlayerSound = new Sound(_crashPlayerSoundBuffer);

            CollisionsHandler.Add(this);
        }

        public void AimTowardsPlayer(Vector2f playerPosition)
        {
            Vector2f direction = playerPosition - _body.Position;

            float directionMagnitude = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

            direction /= directionMagnitude;

            _velocity = direction * _maxSpeed;

        }

        public override void SolveCollision(GameObjectBase other)
        {
            if (other is Bullet)
            {
                Bullet aBullet = other as Bullet;
                if (aBullet != null)
                {
                    if (aBullet.GetActive())
                    {
                        Console.WriteLine("Me choqué contra una bala");
                    }
                    else
                    {
                        Console.WriteLine("Me choqué contra una bala desactivada");
                    }
                    Game._gameScore += _asteroidPoints;
                    this.SetActive(false);
                    CollisionsHandler.FlagEntityForRemoval(this);
                    aBullet.SetActive(false);
                    CollisionsHandler.FlagEntityForRemoval(aBullet);
                }
            }
            if (other is Player)
            {               
                Player myPlayer = other as Player;
                if (myPlayer != null)
                {
                    myPlayer.LoseLife(Game._window);
                    myPlayer.SetActive(false);
                    CollisionsHandler.FlagEntityForRemoval((Player)myPlayer);
                }
            }
        }

        public override void OnCollisionEnter(GameObjectBase other)
        {
            if (other is Bullet)
            {
                if (_crashBulletSound.Status != SoundStatus.Playing)
                {
                    _crashBulletSound.Play();
                }
            }
            
            if (other is Player)
            {
                if (_crashPlayerSound.Status != SoundStatus.Playing)
                {
                    _crashPlayerSound.Play();
                }
            }
        }

        public override void Update(Time deltaTime, RenderWindow objectWindow)
        {
            if (this._isActive)
            {
                _body.Position += _velocity * deltaTime.AsSeconds();
                _sprite.Position = _body.Position;
                _sprite.Rotation = _body.Rotation;

                WrapCoordenates(_body, objectWindow);
            }   
            
        }

        public override void Draw(RenderWindow objectWindow)
        {
            if (this._isActive)
            {
                objectWindow.Draw(_sprite);
            }
        }
    }
}
