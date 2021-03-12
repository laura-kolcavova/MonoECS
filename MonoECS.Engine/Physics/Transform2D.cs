using Microsoft.Xna.Framework;
using MonoECS.Ecs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Physics
{
    public class Transform2D : IEntityComponent
    {
        public Transform2D Parent { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

        public Transform2D()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Size = Vector2.One;
            Rotation = 0;

            Parent = null;
        }

        public bool HasParent => Parent != null;

        public Vector2 AbsolutePosition => HasParent ? Parent.AbsolutePosition + Position : Position;

        public Rectangle Bounds
        {
            get => new Rectangle(Position.ToPoint(), Size.ToPoint());
            set
            {
                Position = new Vector2(value.X, value.Y);
                Size = new Vector2(value.Width, value.Height);
            }
        }

        public Vector2 ScaleSize => Vector2.Multiply(Size, Scale);

        public Rectangle ScaleBounds => new Rectangle(Position.ToPoint(), ScaleSize.ToPoint());

        public void ScaleBy(float scaleX, float scaleY)
        {
            float _scaleX = Scale.X + scaleX;
            float _scaleY = Scale.Y + scaleY;
            Scale = new Vector2(_scaleX, _scaleY);
        }

        public void RotateBy(float amountInDegrees)
        {
            if (amountInDegrees != 0)
            {
                float rotation = (Rotation + amountInDegrees) % 360;
                Rotation = rotation;
            }
        }

        public static Vector2 Up => new Vector2(0, -1);
        public static Vector2 Down => new Vector2(0, 1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);
    }
}
