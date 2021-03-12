using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Engine.Graphics.TextureAtlases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoECS.Engine.Graphics.Sprites
{
    public class SpriteSheet
    {
        private readonly Dictionary<string, TextureRegion2D> _regionsByName;
        private readonly List<TextureRegion2D> _regionsByIndex;

        public string Name { get; }
        public Texture2D Texture { get; }
        
        public Dictionary<string, SpriteSheetAnimationCycle> Cycles { get; }

        public SpriteSheet(string name, Texture2D texture)
        {
            Name = name;
            Texture = texture;

            _regionsByName = new Dictionary<string, TextureRegion2D>();
            _regionsByIndex = new List<TextureRegion2D>();

            Cycles = new Dictionary<string, SpriteSheetAnimationCycle>();
        }

        public SpriteSheet(string name, Texture2D texture, Dictionary<string, Rectangle> regions)
           : this(name, texture)
        {
            foreach (var region in regions)
            {
                CreateRegion(region.Key, region.Value.X, region.Value.Y, region.Value.Width, region.Value.Height);
            }
        }

        public TextureRegion2D CreateRegion(string name, int x, int y, int width, int height)
        {
            if (_regionsByName.ContainsKey(name))
                throw new InvalidOperationException($"Region {name} already exists in the region sheet");

            var region = new TextureRegion2D(name, Texture, x, y, width, height);
            AddRegion(region);
            return region;
        }

        private void AddRegion(TextureRegion2D region)
        {
            _regionsByName.Add(region.Name, region);
            _regionsByIndex.Add(region);
        }

        public TextureRegion2D GetRegion(int index)
        {
            if (index < 0 || index > _regionsByIndex.Count)
                throw new IndexOutOfRangeException();

            return _regionsByIndex[index];
        }

        public TextureRegion2D GetRegion(string name)
        {
            if (_regionsByName.TryGetValue(name, out TextureRegion2D region))
                return region;

            throw new KeyNotFoundException(name);
        }

        public TextureRegion2D this[string name] => GetRegion(name);
        public TextureRegion2D this[int index] => GetRegion(index);
        
        public SpriteSheetAnimation CreateAnimation(string name)
        {
            var cycle = Cycles[name];
            var frames = cycle.Frames.Select(f => _regionsByIndex[f]).ToArray();

            return new SpriteSheetAnimation(name, frames, cycle.FrameDuration,
                cycle.SpriteEffect, cycle.IsLooping, cycle.IsReversed);
        }
    }
}
