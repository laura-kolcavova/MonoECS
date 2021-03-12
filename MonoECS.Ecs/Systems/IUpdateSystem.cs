using Microsoft.Xna.Framework;

namespace MonoECS.Ecs.Systems
{
    public interface IUpdateSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}
