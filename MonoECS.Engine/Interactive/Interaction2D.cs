using Microsoft.Xna.Framework.Input;
using MonoECS.Ecs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Interactive
{
    public class MouseEventArgs : EventArgs
    {
        public MouseState MouseState { get; set; }
        public int EntityId { get; set; }
    }

    public class DragEventArgs : MouseEventArgs
    {
    }

    public class Interaction2D : IEntityComponent
    {
        // Properties
        public bool IsHovered { get; private set; }
        public bool IsPressed { get; private set; }
        public bool IsDraged { get; private set; }

        // Mouse Events
        public event EventHandler<MouseEventArgs> Hover;
        public event EventHandler<MouseEventArgs> Over;
        public event EventHandler<MouseEventArgs> Press;
        public event EventHandler<MouseEventArgs> Release;
        public event EventHandler<MouseEventArgs> Click;

        // Drag Events
        public event EventHandler<DragEventArgs> DragStart;
        public event EventHandler<DragEventArgs> DragOver;
        public event EventHandler<DragEventArgs> Drop;
       
        // Trigger Mouse Events

        internal void OnHover(MouseEventArgs e)
        {
            IsHovered = true;
            Hover?.Invoke(this, e);
        }
        internal void OnOver(MouseEventArgs e)
        {
            IsHovered = false;
            Over?.Invoke(this, e);
        }

        internal void OnPress(MouseEventArgs e)
        {
            IsPressed = true;
            Press?.Invoke(this, e);
        }

        internal void OnRelease(MouseEventArgs e)
        {
            IsPressed = false;
            Release?.Invoke(this, e);
        }
        internal void OnClick(MouseEventArgs e)
        {
            IsPressed = false;
            Click?.Invoke(this, e);
        }


        // Trigger Drag Events

        internal void OnDragStart(DragEventArgs e)
        {
            IsDraged = true;
            DragStart?.Invoke(this, e);
        }

        internal void OnDragOver(DragEventArgs e)
        {
            IsDraged = false;
            DragOver?.Invoke(this, e);
        }

        internal void OnDrop(DragEventArgs e)
        {
            IsDraged = false;
            Drop?.Invoke(this, e);
        }
    }
}
