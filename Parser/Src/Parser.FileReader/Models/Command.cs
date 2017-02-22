using System;

using Parser.FileReader.Contracts;

namespace Parser.FileReader.Models
{
    public class Command : ICommand
    {
        public string AbilityActivatorName { get; set; }

        public string AbilityTargetName { get; set; }

        public decimal EffectAmount { get; set; }

        public decimal EffectEffectiveAmount { get; set; }

        public string EffectName { get; set; }

        public bool IsCritical { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
