using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Components;

namespace MonoECS.Engine.GUI
{
    public enum GUIState
    {
        DEFAULT,
        HOVER
    }

    public class GUIStyle : IEntityComponent
    {
        private GUIState _state;

        public GUIStyleState Default;
        public GUIStyleState Hover;

        private GUIStyleState GetStyleState(GUIState state)
        {
            switch (state)
            {
                case GUIState.DEFAULT: return Default;
                case GUIState.HOVER: return Hover;
                default: return Default;
            }
        }

        public GUIState State
        {
            get => _state;
            set => _state = value;
        }

        public GUIStyleState ActiveStyle => GetStyleState(_state);

        public int Width
            => (ActiveStyle.Width ?? Default.Width).Value;

        public int Height
            => (ActiveStyle.Height ?? Default.Height).Value;

        public SpriteFont SpriteFont
            => ActiveStyle.SpriteFont ?? Default.SpriteFont;

        public Color TextColor
            => (ActiveStyle.TextColor ?? Default.TextColor).Value;

        public Color BackgroundColor
            => (ActiveStyle.BackgroundColor ?? Default.BackgroundColor).Value;

        public Color BorderColor
            => (ActiveStyle.BorderColor ?? Default.BorderColor).Value;

        public int BorderWidth
            => (ActiveStyle.BorderWidth ?? Default.BorderWidth).Value;
    }
}
