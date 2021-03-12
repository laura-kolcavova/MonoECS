using MonoECS.Ecs.Systems;
using MonoECS.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoECS.Engine.Events
{
    public interface IEventManager
    {
        void Publish(IEvent routedEvent);

        void Subscribe<T>(ISystem system, Action<T> listener) where T : IEvent;

        void Unsubscribe<T>(ISystem system, Action<T> listener) where T : IEvent;
    }

    public sealed class EventManager : IEventManager
    {
        private Bag<IEvent> _eventQueue;
        private Dictionary<Type, Bag<IEventListener>> _eventListeners;
        
        public EventManager()
        {
            _eventQueue = new Bag<IEvent>();
            _eventListeners = new Dictionary<Type, Bag<IEventListener>>();
        }

        private EventListener<T> GetEventListener<T>(ISystem system) where T : IEvent
        {
            var eventType = typeof(T);

            var eventListener = _eventListeners[eventType]
                   .FirstOrDefault(el => el.System
                   .Equals(system));

            return eventListener as EventListener<T>;
        }

        public void Publish(IEvent routedEvent)
        {
            _eventQueue.Add(routedEvent);
        }

        public void Subscribe<T>(ISystem system, Action<T> listener) where T : IEvent
        {
            var type = typeof(T);
            
            if(!_eventListeners.ContainsKey(type))
            {
                _eventListeners.Add(type, new Bag<IEventListener>());
            }

            var eventListener = GetEventListener<T>(system);

            if (eventListener == null)
            {
                eventListener = new EventListener<T>(system);
                _eventListeners[type].Add(eventListener);
            }

            eventListener.Actions += listener;
           
        }

        public void Unsubscribe<T>(ISystem system, Action<T> listener) where T : IEvent
        {
            var type = typeof(T);

            if (_eventListeners.TryGetValue(type, out var eventListenerBag))
            {

                var eventListener = GetEventListener<T>(system);

                if(eventListener != null)
                {
                    eventListener.Actions -= listener;
                }

            }
        }

        public void ProcessEvents()
        {
            if (_eventQueue.Any())
            {
                foreach (var routedEvent in _eventQueue)
                {
                    var type = routedEvent.GetType();

                    if (_eventListeners.Any())
                    {
                        foreach (var eventListener in _eventListeners[type])
                        {
                            eventListener.Invoke(routedEvent);
                        }
                    }
                }

                _eventQueue.Clear();
            }
        }
    }
}
