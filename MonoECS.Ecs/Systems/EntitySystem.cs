using System.Collections.Generic;

namespace MonoECS.Ecs.Systems
{
    public abstract class EntitySystem : ISystem
    {
        private readonly Aspect _aspect;

        private World _world;
        private EntitySubscription _entitySubscription;

        protected IEnumerable<int> ActiveEntities => _entitySubscription.Entities;  

        protected EntitySystem(Aspect aspect)
        {
            _aspect = aspect;
        }

        public virtual void Dispose()
        {
            _entitySubscription.Dispose();

            _world.EntityManager.EntityAdded -= OnEntityAdded;
            _world.EntityManager.EntityChanged -= OnEntityChanged;
            _world.EntityManager.EntityRemoved -= OnEntityRemoved;
            _entitySubscription.EntitySubscribed -= OnEntitySubscribed;
            _entitySubscription.EntityUnsubscribed -= OnEntityUnsubsribed;
        }

        public virtual void Initialize(World world)
        {
            _world = world;

            _entitySubscription = new EntitySubscription(_world.EntityManager, _aspect);

            _world.EntityManager.EntityAdded += OnEntityAdded;
            _world.EntityManager.EntityChanged += OnEntityChanged;
            _world.EntityManager.EntityRemoved += OnEntityRemoved;
            _entitySubscription.EntitySubscribed += OnEntitySubscribed;
            _entitySubscription.EntityUnsubscribed += OnEntityUnsubsribed;

            Initialize(_world.ComponentMapperService);
        }

        public abstract void Initialize(IComponentMapperService componentService);

        protected virtual void OnEntityChanged(int entityId) { }
        protected virtual void OnEntityAdded(int entityId) { }
        protected virtual void OnEntityRemoved(int entityId) { }
        protected virtual void OnEntitySubscribed(int entityId) { }
        protected virtual void OnEntityUnsubsribed(int entityId) { }

        protected Entity GetActiveEntity(int entityId)
            => _entitySubscription.GetEntity(entityId);

        protected Entity GetEntity(int entityId)
            => _world.GetEntity(entityId);

        protected Entity CreateEntity()
            => _world.CreateEntity();

        protected void DestroyEntity(int entityId) 
            => _world.DestroyEntity(entityId);
    }
}
