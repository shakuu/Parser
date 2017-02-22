using System;

namespace Parser.FileReader.Contracts
{
    public interface ICommand
    {
        DateTime TimeStamp { get; }

        string AbilityActivatorName { get; }

        string AbilityTargetName { get; }

        string AbilityName { get; }

        string EffectName { get; }

        decimal EffectAmount { get; }

        decimal EffectEffectiveAmount { get; }

        bool IsCritical { get; }
    }
}
