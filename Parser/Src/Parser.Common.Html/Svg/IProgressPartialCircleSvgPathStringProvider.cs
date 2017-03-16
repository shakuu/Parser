namespace Parser.Common.Html.Svg
{
    public interface IProgressPartialCircleSvgPathStringProvider
    {
        string GetPathString(double value, double maximum, double radius);
    }
}
