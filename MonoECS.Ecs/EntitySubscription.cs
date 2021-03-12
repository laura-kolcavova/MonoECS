using MonoECS.Ecs.Managers;
using System;
using System.Collections.Generic;

namespace MonoECS.Ecs
{
    internal class EntitySubscription : IDisposable
    {
        private readonly EntityManager _entityManager;
        private readonly Aspect _aspect;

        private readonly Dictionary<int, Entity> _entities;

        public IEnumerable<int> Entities => _entities.Keys;

        public event Action<int> EntitySubscribed;
        public event Action<int> EntityUnsubscribed;

        public EntitySubscription(EntityManager entityManager, Aspect aspect)
        {
            _entityManager = entityManager;
            _aspect = aspect;

            _entities = new Dictionary<int, Entity>();

            _entityManager.EntityChanged += OnEntityChanged;
            _entityManager.EntityRemoved += OnEntityRemoved;
            
        }

        public void Dispose()
        {
            _entityManager.EntityChanged -= OnEntityChanged;
            _entityManager.EntityRemoved -= OnEntityRemoved;
        }

        private bool IsInterested(int entityId)
        {
            return _aspect.IsInterested(_entityManager.GetComponentTypes(entityId));
        }

        private void SubscribeEntity(int entityId)
        {
            var entity = _entityManager.GetEntity(entityId);

            _entities.Add(entityId, entity);
            EntitySubscribed?.Invoke(entityId);
        }

        private void UnsubscribeEntity(int entityId)
        {
            _entities.Remove(entityId);
            EntityUnsubscribed?.Invoke(entityId);
        }

        private void OnEntityChanged(int entityId)
        {
            if(_entities.ContainsKey(entityId))
            {
                if (!IsInterested(entityId))
                    UnsubscribeEntity(entityId);
            }
            else
            {
                if (IsInterested(entityId))
                    SubscribeEntity(entityId);
            }
        }

        private void OnEntityRemoved(int entityId)
        {
            if(_entities.ContainsKey(entityId))
            {
                UnsubscribeEntity(entityId);
            }
        }

        public Entity GetEntity(int entityId) => _entities[entityId];
    }
}
