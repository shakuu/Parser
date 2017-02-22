using System;

namespace Parser.FileReader.Contracts
{
    public interface ICommand
    {
        DateTime TimeStamp { get; set; }

        string AbilityActivatorName { get; set; }

        string AbilityTargetName { get; set; }

        string EffectName { get; set; }

        decimal EffectAmount { get; set; }

        decimal EffectEffectiveAmount { get; set; }

        bool IsCritical { get; set; }
    }
}
