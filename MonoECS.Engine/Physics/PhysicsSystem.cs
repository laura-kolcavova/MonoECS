using Microsoft.Xna.Framework;
using MonoECS.Ecs;
using MonoECS.Ecs.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoECS.Engine.Physics
{
    public class PhysicsSystem : EntitySystem, IUpdateSystem
    {
        private ComponentMapper<Transform2D> _transform2DMapper;
        private ComponentMapper<RigidBody2D> _rigidBody2DMapper;

        public PhysicsSystem() : base(AspectBuilder
            .All(typeof(Transform2D), typeof(RigidBody2D)))
        {

        }

        public override void Initialize(IComponentMapperService componentService)
        {
            _transform2DMapper = componentService.GetMapper<Transform2D>();
            _rigidBody2DMapper = componentService.GetMapper<RigidBody2D>();
        }

        public void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                var transform2D = _transform2DMapper.Get(entityId);
                var rigidBody2D = _rigidBody2DMapper.Get(entityId);


                if (rigidBody2D.Velocity != Vector2.Zero)
                {
                    transform2D.Position += rigidBody2D.Velocity;
                }
            }
        }


        //public void MoveToPosition(Vector2 target, float speed)
        //{
        //    Transform.SetPosition(Vector2Ext.MoveTowards(Transform.Position, target, speed));
        //}
    }
}
