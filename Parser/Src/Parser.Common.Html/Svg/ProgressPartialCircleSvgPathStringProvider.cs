using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Parser.Common.Html.Svg
{
    public class ProgressPartialCircleSvgPathStringProvider : IProgressPartialCircleSvgPathStringProvider
    {
        private const string PathFormat = "M{0},{1} A{2},{2},0,{3},1,{4},{5}";

        private const double MaximumValue = 100;
        private const double AlphaModifier = 360d / ProgressPartialCircleSvgPathStringProvider.MaximumValue;

        private readonly IDictionary<int, string> memorizedSvgPathsByPercentage;

        public ProgressPartialCircleSvgPathStringProvider()
        {
            this.memorizedSvgPathsByPercentage = new ConcurrentDictionary<int, string>();
        }

        public string GetPathString(int percentage, int radius, int svgSize)
        {
            var percentageSvgPathIsMemorized = this.memorizedSvgPathsByPercentage.ContainsKey(percentage);
            if (percentageSvgPathIsMemorized == false)
            {
                var path = this.GenerateSvgPathString(percentage, radius, svgSize);
                this.memorizedSvgPathsByPercentage.Add(percentage, path);
            }

            return this.memorizedSvgPathsByPercentage[percentage];
        }

        private string GenerateSvgPathString(int percentage, int radius, int svgSize)
        {
            var centerPoint = svgSize / 2;

            var alpha = (int)(ProgressPartialCircleSvgPathStringProvider.AlphaModifier * percentage);
            var a = (90 - alpha) * Math.PI / 180;
            var y = centerPoint - radius * Math.Sin(a);
            var x = centerPoint + radius * Math.Cos(a);

            var generatedSvgPath = string.Empty;
            if (percentage != ProgressPartialCircleSvgPathStringProvider.MaximumValue)
            {
                var center = alpha > 180 ? 1 : 0;

                generatedSvgPath = string.Format(ProgressPartialCircleSvgPathStringProvider.PathFormat, centerPoint, centerPoint - radius, radius, center, x, y);
            }

            return generatedSvgPath;
        }
    }
}
