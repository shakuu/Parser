namespace Parser.Common.Html.Svg
{
    public interface IPartialCircleSvgPathProvider
    {
        string GetSvgPath(int percentage, int radius, int svgSize);
    }
}
