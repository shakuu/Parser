namespace Parser.Common.Contracts
{
    public interface IFinalizedCombatStatistics
    {
        string CharacterName { get; set; }

        double DamageDone { get; set; }

        double DamagePerSecond { get; set; }

        double DamageTaken { get; set; }

        double DamageTakenPerSecond { get; set; }

        double CombatDuration { get; set; }
    }
}
