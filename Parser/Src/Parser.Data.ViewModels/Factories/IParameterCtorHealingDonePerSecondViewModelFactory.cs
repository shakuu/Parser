using System;

using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.ViewModels.Factories
{
    public interface IParameterCtorHealingDonePerSecondViewModelFactory
    {
        ParameterCtorHealingDonePerSecondViewModel CreateParameterCtorHealingDonePerSecondViewModel(Guid id, string characterName, double healingDonePerSecond);
    }
}
