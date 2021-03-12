using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Graphics.Sprites
{
    public class SpriteSheetAnimationCycle
    {
        public List<int> Frames { get; set; }
        public bool IsLooping { get; set; }
        public bool IsReversed { get; set; }
        public float FrameDuration { get; set; }
        public SpriteEffect SpriteEffect { get; set; }

        public SpriteSheetAnimationCycle()
        {
            Frames = new List<int>();
            FrameDuration = 0.2f;
        }

        public SpriteSheetAnimationCycle(params int[] frames)
            : this()
        {
            foreach (int frame in frames)
                Frames.Add(frame);
        }

        public SpriteSheetAnimationCycle(int from, int to)
            : this()
        {
            for(int i = from; i <= to; i ++)
            {
                Frames.Add(i);
            }
        }
    }
}
