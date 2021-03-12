using System;

namespace MonoECS.Ecs.Systems
{
    public interface ISystem : IDisposable
    {
        void Initialize(World world);
    }
}
