using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Ecs;


namespace MonoECS.Engine.Graphics
{
    public class Renderer : IEntityComponent
    {
        public Texture2D MainTexture { get; set; }

        public bool IsVisible { get; set; }

        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }  
        public float Depth { get; set; }  
        public float Alpha { get; set; }

        public Renderer()
        {
            IsVisible = true;
            Color = Color.White;
            Effects = SpriteEffects.None;
            Depth = 0f;
            Alpha = 1.0f;
        }
    }
}
