namespace Parser.Common.Html.Svg
{
    public interface IProgressPartialCircleSvgPathStringProvider
    {
        string GetPathString(int percentage, int radius, int svgSize);
    }
}
