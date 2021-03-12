using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Ecs.Systems
{
    public interface IDrawSystem : ISystem
    {
        void Draw(GameTime gameTime);
    }
}
