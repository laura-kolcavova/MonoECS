using MonoECS.Ecs.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Events
{
    public interface IEventListener
    {
        ISystem System { get; }
        Type EventType { get; }
        void Invoke(IEvent routedEvent);
    }

    public class EventListener<T> : IEventListener where T : IEvent
    {
        public ISystem System { get; private set; }
      
        public Type EventType { get; private set; }

        public event Action<T> Actions;

        internal EventListener(ISystem system)
        {
            System = system;
            EventType = typeof(T);
          
        }
        public void Invoke(IEvent routedEvent)
        {
            Actions?.Invoke((T)routedEvent);
        }
    }
}
