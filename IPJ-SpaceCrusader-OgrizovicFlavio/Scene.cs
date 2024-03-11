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
    abstract class Scene
    {
        protected Scene() 
        { 
        }

        public abstract void Update(RenderWindow window, Time deltaTime);

        public abstract void Draw(RenderWindow window, Time deltaTime);
    }
}
