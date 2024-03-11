using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    class Bullet : GameObjectBase
    {
        Vector2f _velocity;
        public Bullet(Vector2f position, float rotation)
        {
            float bulletSpeed = 500f;

            _body = new RectangleShape(new Vector2f(5,5));
            _body.Origin = new Vector2f(_body.Size.X / 2, _body.Size.Y / 2);
            _body.Position = position;
            _body.Rotation = rotation;

            float angleRad = (float)(Math.PI / 180) * _body.Rotation;

            _velocity = new Vector2f((float)Math.Sin(angleRad) * bulletSpeed, -(float)Math.Cos(angleRad) * bulletSpeed);
            _texture = new Texture("Assets/Objects.png");

            IntRect spriteRect = new IntRect(560, 432, 32, 32);

            _sprite = new Sprite(_texture, spriteRect);

            _sprite.Origin = new Vector2f(_sprite.Position.X + (_sprite.TextureRect.Width / 2),
                _sprite.Position.Y + (_sprite.TextureRect.Width / 2));

            _sprite.Scale = new Vector2f(0.25f, 0.25f);

            CollisionsHandler.Add(this);
        }

        public override void SolveCollision(GameObjectBase other)
        {

        }

        public override void Update(Time deltaTime, RenderWindow playerWindow)
        {
            _body.Position += _velocity * deltaTime.AsSeconds();
            _sprite.Position = _body.Position;
            _sprite.Rotation = _body.Rotation;
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
