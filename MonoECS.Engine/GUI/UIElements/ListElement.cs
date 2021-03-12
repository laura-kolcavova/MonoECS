using MonoECS.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.GUI.UIElements
{
    public class ListElement : IEntityComponent, IBaseElement
    {
        private IDictionary<int, IBaseElement> _elements;
        private int _spacing;

        public event Action<int> ElementAdded;
        public event Action<int> ElementRemoved;
        public event Action Changed;

        public ListElement(int spacing)
        {
            _spacing = spacing;
        }

        public int Spacing
        {
            get => _spacing;
            set
            {
                _spacing = value;
                Changed?.Invoke();
            }
        }

        public IEnumerable<IBaseElement> Elements => _elements.Values;

        public void AddElement(int entityId, IBaseElement element)
        {
            _elements.Add(entityId, element);
            ElementAdded?.Invoke(entityId);
        }

        public void RemoveElement(int entityId)
        {
            _elements.Remove(entityId);
            ElementRemoved.Invoke(entityId);
        }
    }
}
