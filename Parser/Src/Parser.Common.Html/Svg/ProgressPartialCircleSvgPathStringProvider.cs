using System;

namespace Parser.Common.Html.Svg
{
    public class ProgressPartialCircleSvgPathStringProvider : IProgressPartialCircleSvgPathStringProvider
    {
        public string GetPathString(double value, double maximum, double radius)
        {
            double center = 0;
            var alpha = 360 / maximum * value;
            var a = (90 - alpha) * Math.PI / 180;
            var y = 300 - radius * Math.Sin(a);
            var x = 300 + radius * Math.Cos(a);
            var path = string.Empty;

            if (maximum == value)
            {
                path = "M" + 300 + "," + (300 - radius) + " A" + radius + "," + radius + "," + 0 + "," + 1 + "," + 1 + "," + 299.99 + "," + (300 - radius);
            }
            else
            {
                if (alpha > 180)
                {
                    center = 1;
                }
                else
                {
                    center = 0;
                }

                path = "M" + 300 + "," + (300 - radius) + " A" + radius + "," + radius + "," + 0 + "," + center + "," + 1 + "," + x + "," + y;
            }

            return path;
        }
    }
}
