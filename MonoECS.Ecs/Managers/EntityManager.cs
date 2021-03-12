using Microsoft.Xna.Framework;
using MonoECS.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoECS.Ecs.Managers
{
    public sealed class EntityManager
    {
        private const int _defaultBagSize = 128;

        private readonly ComponentManager _componentManager;   

        private readonly Bag<Entity> _entities;
        private readonly Bag<int> _addedEntities;
        private readonly Bag<int> _changedEntities;
        private readonly Bag<int> _removedEntities;
        private readonly Bag<Bag<Type>> _entityComponentTypes;

        private int nextId;

        public event Action<int> EntityAdded;
        public event Action<int> EntityChanged;
        public event Action<int> EntityRemoved;

        public EntityManager(ComponentManager componentManager)
        {
            _componentManager = componentManager;

            nextId = 0;

            _entities = new Bag<Entity>(_defaultBagSize);
            _addedEntities = new Bag<int>();
            _changedEntities = new Bag<int>();
            _removedEntities = new Bag<int>();

            _entityComponentTypes = new Bag<Bag<Type>>();
            
            _componentManager.ComponentsChanged += OnComponentsChanged;
        }

        public int Capacity => _entities.Capacity;

        public IEnumerable<int> Entities => _entities.Where(e => e != null).Select(e => e.Id);

        public Entity GetEntity(int entityId)
        {
            return _entities[entityId];
        }    

        public Entity Create()
        {
            int id = nextId;
            nextId++;

            var entity = new Entity(id, this, _componentManager);

            _entities[id] = entity;

            _addedEntities.Add(id);

            return entity;
        }

        public void Destroy(int entityId)
        {
            if (!_removedEntities.Contains(entityId))
                _removedEntities.Add(entityId);
        }    

        public IEnumerable<Type> GetComponentTypes(int entityId)
        {
            return _entityComponentTypes[entityId];
        }

        private void OnComponentsChanged(int entityId)
        {
            if (!_changedEntities.Contains(entityId))
                _changedEntities.Add(entityId);
        }

        public void Update(GameTime gameTime)
        {
            foreach(int entityId in _addedEntities)
            {
                EntityAdded?.Invoke(entityId);
            }

            foreach(int entityId in _changedEntities)
            {
                _entityComponentTypes[entityId] = _componentManager.CreateComponentTypes(entityId);
                EntityChanged?.Invoke(entityId);
            }

            foreach(int entityId in _removedEntities)
            {
                _entities[entityId] = null;
                _entityComponentTypes[entityId] = null;
                _componentManager.DestroyComponents(entityId);

                EntityRemoved?.Invoke(entityId);
            }

            _addedEntities.Clear();
            _changedEntities.Clear();
            _removedEntities.Clear();
        }

    }
}
