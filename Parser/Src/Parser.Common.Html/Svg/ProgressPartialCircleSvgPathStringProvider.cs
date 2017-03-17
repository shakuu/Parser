using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Parser.Common.Html.Svg
{
    public class ProgressPartialCircleSvgPathStringProvider : IProgressPartialCircleSvgPathStringProvider
    {
        private readonly IDictionary<int, string> memorizedSvgStringsByRoundedPercentage;

        public ProgressPartialCircleSvgPathStringProvider()
        {
            this.memorizedSvgStringsByRoundedPercentage = new ConcurrentDictionary<int, string>();
        }

        public string GetPathString(double value, double maximum, double radius, double svgSize)
        {
            var roundedPercentageValue = (int)(value / maximum * 100);
            if (!this.memorizedSvgStringsByRoundedPercentage.ContainsKey(roundedPercentageValue))
            {
                var path = this.GenerateSvgPathString(value, maximum, radius, svgSize);
                this.memorizedSvgStringsByRoundedPercentage.Add(roundedPercentageValue, path);
            }

            return this.memorizedSvgStringsByRoundedPercentage[roundedPercentageValue];
        }

        private string GenerateSvgPathString(double value, double maximum, double radius, double svgSize)
        {
            var centerPoint = svgSize / 2;

            var alpha = 360 / maximum * value;
            var a = (90 - alpha) * Math.PI / 180;
            var y = centerPoint - radius * Math.Sin(a);
            var x = centerPoint + radius * Math.Cos(a);

            var path = string.Empty;
            if (maximum == value)
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
