using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Ecs.Systems
{
    public abstract class BaseSystem : ISystem
    {
        private World _world;

        protected BaseSystem()
        {
        }

        public virtual void Dispose()
        {
            _world.EntityManager.EntityAdded -= OnEntityAdded;
            _world.EntityManager.EntityChanged -= OnEntityChanged;
            _world.EntityManager.EntityRemoved -= OnEntityRemoved;
        }

        public virtual void Initialize(World world)
        {
            _world = world;

            _world.EntityManager.EntityAdded += OnEntityAdded;
            _world.EntityManager.EntityChanged += OnEntityChanged;
            _world.EntityManager.EntityRemoved += OnEntityRemoved;

            Initialize(_world.ComponentMapperService);
        }

        public abstract void Initialize(IComponentMapperService componentService);

        protected virtual void OnEntityChanged(int entityId) { }
        protected virtual void OnEntityAdded(int entityId) { }
        protected virtual void OnEntityRemoved(int entityId) { }

        protected Entity GetEntity(int entityId)
            => _world.GetEntity(entityId);

        protected Entity CreateEntity()
            => _world.CreateEntity();

        protected void DestroyEntity(int entityId)
            => _world.DestroyEntity(entityId);
    }
}
