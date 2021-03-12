using Microsoft.Xna.Framework;
using MonoECS.Ecs.Managers;
using MonoECS.Ecs.Systems;
using System;

namespace MonoECS.Ecs
{
    public sealed class World : IDisposable
    {
        private readonly EntityManager _entityManager;
        private readonly ComponentManager _componentManager;
        private readonly SystemManager _systemManager;

        internal World()
        {
            _componentManager = new ComponentManager();
            _entityManager = new EntityManager(_componentManager);
            _systemManager = new SystemManager();
        }

        internal EntityManager EntityManager => _entityManager;

        internal ComponentManager ComponentManager => _componentManager;

        internal void RegisterSystem(ISystem system)
        {
            _systemManager.Add(system);

            system.Initialize(this);
        }

        public void Dispose()
        {
            _systemManager.Dispose();
        }

        public Entity GetEntity(int entityId)
        {
            return _entityManager.GetEntity(entityId);
        }

        public Entity CreateEntity()
        {
            return _entityManager.Create();
        }

        public void DestroyEntity(int entityId)
        {
            _entityManager.Destroy(entityId);
        }

        public void Update(GameTime gameTime)
        {
            _entityManager.Update(gameTime);
            _systemManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _systemManager.Draw(gameTime);
        }

        public IComponentMapperService ComponentMapperService => ComponentManager;
    }
}
