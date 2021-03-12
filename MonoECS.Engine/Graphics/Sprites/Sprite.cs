using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Ecs;
using MonoECS.Engine.Graphics.TextureAtlases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MonoECS.Engine.Graphics.Sprites
{
    public class Sprite : IEntityComponent
    {
        public TextureRegion2D TextureRegion { get; protected set; }

        public bool IsVisible { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }    
        public float Depth { get; set; }
        public float Alpha { get; set; }

        public Sprite(TextureRegion2D textureRegion)
        {
            TextureRegion = textureRegion;
            IsVisible = true;
            Color = Color.White;
            Effects = SpriteEffects.None;      
            Depth = 0f;
            Alpha = 1.0f;
        }
    }
}
