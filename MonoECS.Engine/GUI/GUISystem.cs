using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoECS.Components;
using MonoECS.Engine.Graphics;
using MonoECS.Engine.GUI.UIElements;
using MonoECS.Engine.Interactive;
using MonoECS.Engine.Physics;
using MonoECS.Systems;
using MonoECS.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MonoECS.Engine.GUI
{
    public class GUISystem : EntitySystem, IUpdateSystem, IDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _sb;

        private ComponentMapper<GUIStyle> _guiStyleMapper;
        private ComponentMapper<Transform2D> _transform2DMapper;
        private ComponentMapper<Label> _labelMapper;
        private ComponentMapper<Box> _boxMapper;

        private MouseState _mouseState;

        public GUISystem(GraphicsDevice graphicsDevice) 
            : base(Aspect
                  .All(typeof(Transform2D), typeof(GUIStyle)))

        {
            _graphicsDevice = graphicsDevice;
            _sb = new SpriteBatch(_graphicsDevice);
        }

        //private Bag<Bag<IBaseElement>> GetElements()
        //{
        //    var elements = new Bag<Bag<IBaseElement>>();

        //    foreach (int entityId in ActiveEntities)
        //    {
        //        var elementBag = new Bag<IBaseElement>();

        //        if (_boxMapper.Has(entityId))
        //            elementBag.Add(_boxMapper.Get(entityId));

        //        if (_labelMapper.Has(entityId))
        //            elementBag.Add(_labelMapper.Get(entityId));

        //        elements[entityId] = elementBag;

        //    }

        //    return elements;
        //}

        public override void Initialize(IComponentMapperService componentManager)
        {
            _guiStyleMapper = componentManager.GetMapper<GUIStyle>();
            _transform2DMapper = componentManager.GetMapper<Transform2D>();
            _labelMapper = componentManager.GetMapper<Label>();
            _boxMapper = componentManager.GetMapper<Box>();
        }

        public void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();

            foreach(int entityId in ActiveEntities)
            {
                var transform2D = _transform2DMapper.Get(entityId);
                var guiStyle = _guiStyleMapper.Get(entityId);

                if (InteractionHelper.IsEntityHovered(transform2D, _mouseState))
                {              
                    guiStyle.State = GUIState.HOVER;
                }
                else
                {
                    guiStyle.State = GUIState.DEFAULT;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
           
            _sb.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            foreach (int entityId in ActiveEntities)
            {
                var transform2D = _transform2DMapper.Get(entityId);
                 GUIStyle guiStyle = _guiStyleMapper.Get(entityId);

                // Box
                if (_boxMapper.Has(entityId))
                {
                    var box = _boxMapper.Get(entityId);
                    RenderBox(transform2D, box, guiStyle);
                }

                // Label
                if (_labelMapper.Has(entityId))
                {
                    var label = _labelMapper.Get(entityId);
                   
                    RenderLabel(transform2D, label, guiStyle);
                   
                }

            }    

            _sb.End();
        }

        private void RenderBox(Transform2D transform2D, Box box, GUIStyle style)
        {
            var rect = new Rectangle(transform2D.AbsolutePosition.ToPoint(), transform2D.ScaleSize.ToPoint());

            // Background
            _sb.FillRectangle(rect, style.BackgroundColor);

            // Borders
            _sb.DrawRectangle(rect, style.BorderColor, style.BorderWidth);
        }

        private void RenderLabel(Transform2D transform2D, Label label, GUIStyle style)
        {
            var measureString = style.SpriteFont.MeasureString(label.Text);

            var textPosition = new Vector2(
                    x: transform2D.AbsolutePosition.X + (transform2D.ScaleSize.X / 2 - measureString.X / 2),
                    y: transform2D.AbsolutePosition.Y + (transform2D.ScaleSize.Y / 2 - measureString.Y / 2)
                    );

            _sb.DrawString(style.SpriteFont, label.Text, textPosition, style.TextColor);
        }

        
    }
}
