using Microsoft.Xna.Framework;
using MonoECS.Ecs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Physics
{
    public class RigidBody2D : IEntityComponent
    {
        public Vector2 Velocity { get; set; }
    }
}
