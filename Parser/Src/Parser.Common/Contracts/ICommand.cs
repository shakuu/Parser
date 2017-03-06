using System;

namespace Parser.Common.Contracts
{
    public interface ICommand
    {
        DateTime TimeStamp { get; set; }

        string AbilityActivatorName { get; set; }

        string AbilityTargetName { get; set; }

        string AbilityName { get; set; }

        string AbilityGameId { get; set; }

        double AbilityCost { get; set; }

        string EventType { get; set; }

        string EventTypeGameId { get; set; }

        string EventName { get; set; }

        string EventNameGameId { get; set; }

        double EffectAmount { get; set; }

        string EffectType { get; set; }

        string EffectTypeGameId { get; set; }

        double EffectEffectiveAmount { get; set; }

        bool IsCritical { get; set; }

        string OriginalString { get; set; }
    }
}
