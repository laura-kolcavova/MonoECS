using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.GUI
{
    public struct GUIStyleState
    {
        public int? Width;
        public int? Height;
        public Color? BackgroundColor;
        public SpriteFont SpriteFont;
        public Color? TextColor;
        public int? BorderWidth;
        public Color? BorderColor;
    }
}
