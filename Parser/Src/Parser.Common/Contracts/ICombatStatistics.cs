namespace Parser.Common.Contracts
{
    public interface ICombatStatistics
    {
        decimal DamageDone { get; set; }

        decimal HealingDone { get; set; }

        bool IsCompleted { get; set; }
    }
}
