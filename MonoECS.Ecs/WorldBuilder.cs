using MonoECS.Ecs.Systems;
using System.Collections.ObjectModel;

namespace MonoECS.Ecs
{
    public sealed class WorldBuilder
    {
        private Collection<ISystem> _systems;

        public WorldBuilder()
        {
            _systems = new Collection<ISystem>();
        }

        public WorldBuilder AddSystem(ISystem system)
        {
            _systems.Add(system);
            return this;
        }

        public World Build()
        {
            var world = new World();

            foreach (var system in _systems)
                world.RegisterSystem(system);

  
            return world;
        }
    }
}
