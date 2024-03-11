using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    abstract class GameObjectBase
    {
        protected Texture _texture;
        protected Sprite _sprite;
        protected RectangleShape _body;
        protected bool _isActive = true;

        public bool IsColliding(GameObjectBase other)
        {

            FloatRect thisBounds = _sprite.GetGlobalBounds();
            FloatRect otherBounds = other._sprite.GetGlobalBounds();

            return thisBounds.Intersects(otherBounds);

        }

        public virtual void OnCollisionExit(GameObjectBase otherObject)
        {

        }

        public virtual void OnCollisionEnter(GameObjectBase otherObject)
        {

        }

        public abstract void SolveCollision(GameObjectBase other);

        public bool GetActive ()
        {
            return _isActive;
        }

        public void SetActive (bool active)
        {
            _isActive = active;
        }
        public Sprite GetSprite()
        {
            return _sprite;
        }

        public RectangleShape GetBody()
        {
            return _body;
        }

        public virtual void Update(Time deltaTime, RenderWindow playerWindow)
        {

        }

        public virtual void Draw(RenderWindow objectWindow)
        {
            objectWindow.Draw(_sprite);
        }

        public virtual void WrapCoordenates(RectangleShape rectangle, RenderWindow objectWindow)
        {
            if (rectangle.Position.X - rectangle.Size.X > objectWindow.Size.X)
            {
                rectangle.Position = new Vector2f(0 - rectangle.Size.X, objectWindow.Position.Y);
            }
            if (rectangle.Position.X + rectangle.Size.X < 0)
            {
                rectangle.Position = new Vector2f(objectWindow.Size.X + rectangle.Size.X, rectangle.Position.Y);
            }
            if (rectangle.Position.Y - rectangle.Size.Y > objectWindow.Size.Y)
            {
                rectangle.Position = new Vector2f(rectangle.Position.X, 0 - rectangle.Size.Y);
            }
            if (rectangle.Position.Y + rectangle.Size.Y < 0)
            {
                rectangle.Position = new Vector2f(rectangle.Position.X, objectWindow.Size.Y + rectangle.Size.Y);
            }
        }

        public bool IsInsideWindow (Vector2f position, RenderWindow objectWindow)
        {
            bool isInsideLeft = position.X >= 0;

            bool isInsideRight = position.X <= objectWindow.Size.X;

            bool isInsideTop = position.Y >= 0;

            bool isInsideBottom = position.Y <= objectWindow.Size.Y;

            bool isInsideWindow = isInsideLeft && isInsideRight && isInsideTop && isInsideBottom;

            return isInsideWindow;
        }

    }
}
