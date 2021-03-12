using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Graphics.TextureAtlases
{
    public class TextureRegion2D
    {
        public string Name { get; }
        public Texture2D Texture { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
        public Point Size => new Point(Width, Height);

        public TextureRegion2D(string name, Texture2D texture, int x, int y, int width, int height)
        {
            Name = name;
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public TextureRegion2D(string name, Texture2D texture, Rectangle bounds)
            : this(name, texture, bounds.X, bounds.Y, bounds.Width, bounds.Height)

        {
        }

        public TextureRegion2D(Texture2D texture)
            : this(texture.Name, texture, texture.Bounds)
        {

        }
    }
}
