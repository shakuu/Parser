using System;

using Parser.FileReader.Contracts;

namespace Parser.FileReader.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(DateTime timeStamp, string abilityActivatorName, string abilityTargetName, string abilityName, string effectName, decimal effectAmount, decimal effectEffectiveAmount);
    }
}
