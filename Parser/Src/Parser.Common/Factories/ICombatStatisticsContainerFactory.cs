using Parser.Common.Contracts;

namespace Parser.Common.Factories
{
    public interface ICombatStatisticsContainerFactory
    {
        ICombatStatisticsContainer CreateCombatStatisticsContainer(ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory);
    }
}
