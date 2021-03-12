using MonoECS.Collections;
using System;
using System.Collections.Generic;

namespace MonoECS.Ecs
{
    public interface IComponentMapper
    {
        int Id { get; }
        Type ComponentType { get; }

        bool Has(int entityId);
        void Remove(int entityId);
    }

    public sealed class ComponentMapper<T> : IComponentMapper 
        where T : IEntityComponent
    {
        private Bag<IEntityComponent> _components;

        private readonly Action<int> _onComponentsChanged;

        public ComponentMapper(int id, Action<int> onComponentsChanged)
        {
            Id = id;
            ComponentType = typeof(T);

            _onComponentsChanged = onComponentsChanged;

            _components = new Bag<IEntityComponent>();
        }

        public int Id { get; private set; }

        public Type ComponentType { get; private set; }

        public IEnumerable<T> Components => _components as IEnumerable<T>;

        public void Put(int entityId, T component)
        {
            _components[entityId] = component;
            _onComponentsChanged.Invoke(entityId);

        }

        public void Remove(int entityId)
        {
            _components[entityId] = null;
            _onComponentsChanged?.Invoke(entityId);
        }

        public T Get (Entity entity)
        {
            return Get(entity.Id);
        }

        public T Get(int entityId)
        {
            return (T) _components[entityId];
        }

        public bool Has(int entityId)
        {
            if (entityId >= _components.Count)
                return false;

            return _components[entityId] != null;
        }

        
    }
}
