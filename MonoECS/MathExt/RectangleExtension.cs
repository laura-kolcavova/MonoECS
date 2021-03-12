using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.MathExt
{
    public static class RectangleExtension
    {
        public static bool Intersects(this Rectangle bounds, Point point)
        {
            return
                bounds.Right > point.X &&
                bounds.Left < point.X &&
                bounds.Bottom > point.Y &&
                bounds.Top < point.Y;
        }

        //public static bool Intersects(Rectangle bounds1, Rectangle bounds2)
        //{

        //    return
        //        bounds1.Right > bounds2.Left &&
        //        bounds1.Left < bounds2.Right &&
        //        bounds1.Bottom > bounds2.Top &&
        //        bounds1.Top < bounds2.Bottom
        //}
    }
}
