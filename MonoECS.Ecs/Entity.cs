using MonoECS.Ecs.Managers;
using System;
using System.Collections.Generic;

namespace MonoECS.Ecs
{
    public sealed class Entity
    {
        private readonly EntityManager _entityManager;
        private readonly ComponentManager _componentManager;

        public Entity(int id, EntityManager entityManager, ComponentManager componentManager)
        {
            Id = id;
            _entityManager = entityManager;
            _componentManager = componentManager;

            Name = string.Empty;

            Enabled = true;
        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public IEnumerable<Type> ComponentTypes => _entityManager.GetComponentTypes(Id);

        public T AttachComponent<T>(T component) where T : IEntityComponent
        {
            _componentManager.GetMapper<T>().Put(Id, component);
            return component;
        }

        public void DettachComponent<T>() where T : IEntityComponent
        {
            _componentManager.GetMapper<T>().Remove(Id);
        }

        public T GetComponent<T>() where T : IEntityComponent
        {
            var component = _componentManager.GetMapper<T>().Get(Id);
            return component;
        }

        public bool HasComponent<T>() where T : IEntityComponent
        {
            return _componentManager.GetMapper<T>().Has(Id);
        }

        public void Destroy()
        {
            _entityManager.Destroy(Id);
        }
    }
}
