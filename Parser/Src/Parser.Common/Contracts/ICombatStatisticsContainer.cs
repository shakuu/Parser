namespace Parser.Common.Contracts
{
    public interface ICombatStatisticsContainer
    {
        decimal DamageDone { get; set; }

        decimal HealingDone { get; set; }
    }
}
