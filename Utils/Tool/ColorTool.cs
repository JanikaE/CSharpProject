using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Utils.Tool
{
    public static class ColorTool
    {
        public static Color FindClosest(this Color self, IEnumerable<Color> colors, int redWeigth = 1, int greenWeight = 1, int blueWeight = 1)
        {
            if (colors?.Any() != true) throw new ArgumentException("颜色集合不能为空", nameof(colors));

            Color closest = default;
            double minDistance = double.MaxValue;

            foreach (var color in colors)
            {
                int dr = self.R - color.R;
                int dg = self.G - color.G;
                int db = self.B - color.B;
                double distance = Math.Sqrt(redWeigth * dr * dr + greenWeight * dg * dg + blueWeight * db * db);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = color;
                }
            }
            return closest;
        }

        public static Color FindClosestLab(this Color self, IEnumerable<Color> colors)
        {
            if (colors?.Any() != true) throw new ArgumentException("颜色集合不能为空", nameof(colors));

            var labSelf = new Rgb { R = self.R, G = self.G, B = self.B }.To<Lab>();
            var comparison = new Cie1976Comparison();

            return colors
                .Select(c => new { Color = c, Lab = new Rgb { R = c.R, G = c.G, B = c.B }.To<Lab>() })
                .OrderBy(x => x.Lab.Compare(labSelf, comparison))
                .First().Color;
        }
    }
}
