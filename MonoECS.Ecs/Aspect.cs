using MonoECS.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoECS.Ecs
{
    public static class AspectBuilder
    {
        public static Aspect All(params Type[] componentTypes)
        {
            return new Aspect().All(componentTypes);
        }

        public static Aspect One(params Type[] componentTypes)
        {
            return new Aspect().One(componentTypes);
        }

        public static Aspect Not(params Type[] componentTypes)
        {
            return new Aspect().Not(componentTypes);
        }
    }

    public sealed class Aspect
    {
        private readonly Bag<Type> AllTypes;
        private readonly Bag<Type> ExcludeTypes;
        private readonly Bag<Type> OneTypes;

        internal Aspect()
        {
            AllTypes = new Bag<Type>();
            ExcludeTypes = new Bag<Type>();
            OneTypes = new Bag<Type>();
        }

        public Aspect All(params Type[] componentTypes)
        {
            foreach(var type in componentTypes)
            {
                AllTypes.Add(type);
            }

            return this;
        }

        public Aspect One(params Type[] componentTypes)
        {
            foreach (var type in componentTypes)
            {
                OneTypes.Add(type);
            }

            return this;
        }

        public Aspect Not(params Type[] componentTypes)
        {
            foreach (var type in componentTypes)
            {
                ExcludeTypes.Add(type);
            }

            return this;
        }

        public bool IsInterested(IEnumerable<Type> componentTypes) 
        {
            if (!componentTypes.Any()) return false;

            if(AllTypes.Any() && AllTypes.All(t => componentTypes.Contains(t)) == false)
            {
                return false;
            }

            if(ExcludeTypes.Any() && ExcludeTypes.Any(t => componentTypes.Contains(t)))
            {
                return false;
            }

            if(OneTypes.Any() && OneTypes.Any(t => componentTypes.Contains(t)) == false )
            {
                return false;
            }

            return true;
        }

    }
}
