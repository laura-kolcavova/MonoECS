using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoECS.Engine.Physics;
using MonoECS.Ecs.Systems;
using MonoECS.Ecs;
using System.Linq;

namespace MonoECS.Engine.Interactive
{
    public class InteractionSystem : EntitySystem, IUpdateSystem
    {
        private ComponentMapper<Interaction2D> _interaction2DMapper;
        private ComponentMapper<Transform2D> _transform2DMapper;

        private MouseState current_mouseState;
        private MouseState prev_mouseState;

        // Rozdelit na mouse interaction a keyboard interaction system
        public InteractionSystem()
            : base (AspectBuilder
                  .All(typeof(Interaction2D), typeof(Transform2D)))
        {
        }

        public override void Initialize(IComponentMapperService componentService)
        {
            _interaction2DMapper = componentService.GetMapper<Interaction2D>();
            _transform2DMapper = componentService.GetMapper<Transform2D>();
        }


        public void Begin()
        {
            current_mouseState = Mouse.GetState();
        }

        public void End()
        {
            prev_mouseState = current_mouseState;
        }

        public void Update(GameTime gameTime)
        {
            Begin();

            foreach(int entityId in ActiveEntities)
            {
                Process(entityId);
            }

            End();
        }

        public void Process(int entityId)
        {
            var transform = _transform2DMapper.Get(entityId);
            var interaction = _interaction2DMapper.Get(entityId);

            // Check for Events

            bool onHover = CheckHover(transform, interaction);

            bool onOver = CheckOver(transform, interaction);

            bool onPress = CheckPress(transform, interaction);

            bool onRelease = CheckRelease(transform, interaction);

            bool onClick = CheckClick(transform, interaction);

            bool onDragStart = CheckDragStart(transform, interaction);

            bool onDragOver = CheckDragOver(transform, interaction);

            bool onDrop = CheckDrop(transform, interaction);

            // Trigger Events

            if (onHover) interaction.OnHover(BuildMouseEvent(entityId));

            if (onOver) interaction.OnOver(BuildMouseEvent(entityId));

            if (onPress) interaction.OnPress(BuildMouseEvent(entityId));

            if (onRelease) interaction.OnRelease(BuildMouseEvent(entityId));

            if (onClick) interaction.OnClick(BuildMouseEvent(entityId));

            if (onDragStart) interaction.OnDragStart(BuildDragEvent(entityId));

            if (onDragOver) interaction.OnDragOver(BuildDragEvent(entityId));

            if (onDrop) interaction.OnDrop(BuildDragEvent(entityId));
        }

        // Mouse handlers

        private bool CheckHover(Transform2D transform, Interaction2D interaction)
        {
            if(!interaction.IsHovered && IsEntityHovered(transform, current_mouseState))
            {
                return true;
            }

            return false;
        }

        private bool CheckOver(Transform2D transform, Interaction2D interaction)
        {
            if (interaction.IsHovered && !IsEntityHovered(transform, current_mouseState))
            {
                return true;
            }

            return false;
        }

        private bool CheckPress(Transform2D transform, Interaction2D interaction)
        {
            if (!interaction.IsPressed && IsEntityPressed(transform, current_mouseState))
            {
                return true;
            }
            return false;
        }

        private bool CheckRelease(Transform2D transform, Interaction2D interaction)
        {
            if (interaction.IsPressed && !IsEntityPressed(transform, current_mouseState))
            {
                return true;
            }
            return false;
        }

        private bool CheckClick(Transform2D transform, Interaction2D interaction)
        {
            if (interaction.IsPressed && IsEntityHovered(transform, current_mouseState) && IsMouseReleased(current_mouseState))
            {
                return true;
            }
            return false;
        }

        // Drag handlers

        private bool CheckDragStart(Transform2D transform, Interaction2D interaction)
        {
            if (!interaction.IsDraged && IsEntityPressed(transform, current_mouseState)
                && IsMouseReleased(prev_mouseState))
            {
                return true;
            }

            return false;
        }


        public bool CheckDragOver(Transform2D transform, Interaction2D interacton)
        {
            if (interacton.IsDraged && !IsEntityPressed(transform, current_mouseState) && 
                IsMousePressed(current_mouseState)) 
            {
                return true;
            }

            return false;
        }

       

        private bool CheckDrop(Transform2D transform, Interaction2D interaction)
        {
            if (interaction.IsDraged && IsMouseReleased(current_mouseState))
            {
                return true;
            }

            return false;
        }

        private MouseEventArgs BuildMouseEvent(int entityId)
        {
            return new MouseEventArgs()
            {
                EntityId = entityId,
                MouseState = current_mouseState
            };
        }

        private DragEventArgs BuildDragEvent(int entityId)
        {
            return new DragEventArgs()
            {
                EntityId = entityId,
                MouseState = current_mouseState
            };
        }

        // Mouse Helpers

        public static bool IsMousePressed(MouseState mouseState)
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsMouseReleased(MouseState mouseState)
        {
            return mouseState.LeftButton == ButtonState.Released;
        }

        public static bool IsEntityHovered(Transform2D transform, MouseState mouseState)
        {
            var bounds = new Rectangle(transform.AbsolutePosition.ToPoint(), transform.ScaleSize.ToPoint());


            return mouseState.Position.X >= bounds.X &&
                   mouseState.Position.X <= bounds.X + bounds.Width &&
                   mouseState.Position.Y >= bounds.Y &&
                   mouseState.Position.Y <= bounds.Y + bounds.Height;
        }

        public static bool IsEntityPressed(Transform2D transform, MouseState mouseState)
        {
            return IsEntityHovered(transform, mouseState) && IsMousePressed(mouseState);
        }
    }
}
