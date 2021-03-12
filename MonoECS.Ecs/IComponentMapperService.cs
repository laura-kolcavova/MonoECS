using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Ecs
{
    public interface IComponentMapperService
    {
        ComponentMapper<T> GetMapper<T>() where T : IEntityComponent;
    }
}
