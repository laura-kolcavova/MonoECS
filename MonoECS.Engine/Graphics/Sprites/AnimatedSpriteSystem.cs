using Microsoft.Xna.Framework;
using MonoECS.Ecs;
using MonoECS.Ecs.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Graphics.Sprites
{
    public class AnimatedSpriteSystem : EntitySystem, IUpdateSystem
    {
        private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;

        public AnimatedSpriteSystem() :
            base (AspectBuilder.All(typeof(AnimatedSprite)))
        {

        }

        public override void Initialize(IComponentMapperService componentService)
        {
            _animatedSpriteMapper = componentService.GetMapper<AnimatedSprite>();
        }

        public void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                var animatedSprite = _animatedSpriteMapper.Get(entityId);

                var currentAnimation = animatedSprite.CurrentAnimation;

                if (currentAnimation != null && !currentAnimation.IsComplete)
                {
                    currentAnimation.Update(gameTime);
                    animatedSprite.SetTextureRegion(currentAnimation.CurrentFrame);
                }
            }
            
        }
    }
}
