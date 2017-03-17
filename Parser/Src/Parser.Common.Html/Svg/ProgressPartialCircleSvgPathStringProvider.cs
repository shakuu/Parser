using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Parser.Common.Html.Svg
{
    public class ProgressPartialCircleSvgPathStringProvider : IProgressPartialCircleSvgPathStringProvider
    {
        private const double AlphaModifier = 360d / 100d;

        private readonly IDictionary<int, string> memorizedSvgStringsByRoundedPercentage;

        public ProgressPartialCircleSvgPathStringProvider()
        {
            this.memorizedSvgStringsByRoundedPercentage = new ConcurrentDictionary<int, string>();
        }

        public string GetPathString(int percentage, int radius, int svgSize)
        {
            var roundedPercentageValueIsMemorized = this.memorizedSvgStringsByRoundedPercentage.ContainsKey(percentage);
            if (roundedPercentageValueIsMemorized == false)
            {
                var path = this.GenerateSvgPathString(percentage, radius, svgSize);
                this.memorizedSvgStringsByRoundedPercentage.Add(percentage, path);
            }

            return this.memorizedSvgStringsByRoundedPercentage[percentage];
        }

        private string GenerateSvgPathString(int percentage, int radius, int svgSize)
        {
            var centerPoint = svgSize / 2;

            var alpha = (int)(ProgressPartialCircleSvgPathStringProvider.AlphaModifier * percentage);
            var a = (90 - alpha) * Math.PI / 180;
            var y = centerPoint - radius * Math.Sin(a);
            var x = centerPoint + radius * Math.Cos(a);

            var path = string.Empty;
            if (100 == percentage)
            {
                path = "M" + centerPoint + "," + (centerPoint - radius) + " A" + radius + "," + radius + "," + 0 + "," + 1 + "," + 1 + "," + 299.99 + "," + (centerPoint - radius);
            }
            else
            {
                var center = alpha > 180 ? 1 : 0;

                path = "M" + centerPoint + "," + (centerPoint - radius) + " A" + radius + "," + radius + "," + 0 + "," + center + "," + 1 + "," + x + "," + y;
            }

            return path;
        }
    }
}
