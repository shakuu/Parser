using System;

using Parser.FileReader.Contracts;

namespace Parser.FileReader.Models
{
    public class Command : ICommand
    {
        public Command(DateTime timeStamp, string abilityActivatorName, string abilityTargetName, string abilityName, string effectName, decimal effectAmount, decimal effectEffectiveAmount)
        {
            this.TimeStamp = timeStamp;
            this.AbilityActivatorName = abilityActivatorName;
            this.AbilityTargetName = abilityTargetName;
            this.AbilityName = abilityName;
            this.EffectName = effectName;
            this.EffectAmount = effectAmount;
            this.EffectEffectiveAmount = effectEffectiveAmount;
        }

        public string AbilityActivatorName { get; private set; }

        public string AbilityTargetName { get; private set; }

        public string AbilityName { get; private set; }

        public decimal EffectAmount { get; private set; }

        public decimal EffectEffectiveAmount { get; private set; }

        public string EffectName { get; private set; }

        public bool IsCritical { get; private set; }

        public DateTime TimeStamp { get; private set; }
    }
}
