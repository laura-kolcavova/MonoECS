using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Components;

namespace MonoECS.Engine.GUI.UIElements
{
    public class Label : IEntityComponent, IBaseElement
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => _text = value;
        }
    }
}
