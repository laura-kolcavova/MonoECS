using MonoECS.Collections;
using System;
using System.Collections.Generic;


namespace MonoECS.Ecs.Managers
{
    public sealed class ComponentManager : IComponentMapperService
    {
        private readonly Bag<IComponentMapper> _componentMappers;
        private readonly Dictionary<Type, int> _componentTypes;
        
        private int nextId;

        public event Action<int> ComponentsChanged;

        public ComponentManager()
        {
            _componentMappers = new Bag<IComponentMapper>();
            _componentTypes = new Dictionary<Type, int>();

            nextId = 0;
        }

        private ComponentMapper<T> CreateMapperForType<T>(int componentTypeId)
            where T : IEntityComponent
        {
            var mapper = new ComponentMapper<T>(componentTypeId, ComponentsChanged);

            _componentMappers[componentTypeId] = mapper;

            return mapper;
        }

        private int GetComponentTypeId(Type type)
        {
            if (_componentTypes.TryGetValue(type, out int id))
            {
                return id;
            }

            id = nextId;
            nextId++;

            _componentTypes.Add(type, id);

            return id;
        }

        public ComponentMapper<T> GetMapper<T>() where T : IEntityComponent
        {
            int componentTypeId = GetComponentTypeId(typeof(T));

            var mapper = _componentMappers[componentTypeId];

            if(mapper != null)
            {
                return mapper as ComponentMapper<T>;
            }

            return CreateMapperForType<T>(componentTypeId);
        }

        public void DestroyComponents(int entityId)
        {
            foreach(var mapper in _componentMappers)
            {
                mapper.Remove(entityId);
            }
        }

        public Bag<Type> CreateComponentTypes(int entityId)
        {
            var types = new Bag<Type>();

            foreach (var mapper in _componentMappers)
            {
                if (mapper.Has(entityId)) types.Add(mapper.ComponentType);

            }

            return types;
        }
    }
}
