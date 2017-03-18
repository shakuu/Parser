using Parser.Common.Contracts;

namespace Parser.Common.Factories
{
    public interface ILiveStatisticsContainerFactory
    {
        ILiveStatisticsContainer CreateLiveStatisticsContainer();
    }
}
