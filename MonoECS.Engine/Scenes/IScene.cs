using Microsoft.Xna.Framework;
using MonoECS.Ecs.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Scenes
{
    public interface IScene : IDisposable
    {
        void LoadContent();
        void UnloadContent();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
