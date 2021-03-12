using Microsoft.Xna.Framework;
using MonoECS.Engine.GUI.UIElements;
using MonoECS.Engine.Interactive;
using MonoECS.Engine.Physics;
using MonoECS.Entities;
using System;

namespace MonoECS.Engine.GUI
{
    public class LabelTemplate : EntityTemplate
    {
        public override Entity Build()
        {
            return CreateEntity();
        }

        public Entity Build(string text, Vector2 position, GUIStyle style)
        {
            var entity = Build();

            entity.SetName("Text");

            entity.AttachComponent(new Transform2D(position)
            {
                Size = new Vector2(style.Width, style.Height)
            });

            entity.AttachComponent(style);

            entity.AttachComponent(new Label()
            {
                Text = text,
            });

            return entity;
        }
    }

    public class ButtonTemplate : EntityTemplate
    {
        public override Entity Build()
        {
            return CreateEntity();
        }
        
        public Entity Build(string text, Vector2 position, GUIStyle style)
        {
            var entity = Build();

            var label = GetTemplate<LabelTemplate>().Build(text, Vector2.Zero, style);

            entity.SetName("Button");

            var transform2D = entity.AttachComponent(new Transform2D(position)
            {
                Size = new Vector2(style.Width, style.Height)
            });

            entity.AttachComponent(style);

            entity.AttachComponent(new Box());

            entity.AttachComponent(new Interaction2D());

            label.GetComponent<Transform2D>().SetParent(transform2D);

            return entity;
        }

    }
}
