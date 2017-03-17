namespace Parser.Common.Contracts
{
    public interface IFinalizedCombatStatistics
    {
        string CharacterName { get; set; }

        double DamageDone { get; set; }

        double DamageDonePerSecond { get; set; }

        double DamageTaken { get; set; }

        double DamageTakenPerSecond { get; set; }

        double HealingDone { get; set; }

        double HealingDonePerSecond { get; set; }

        double CombatDurationInSeconds { get; set; }
    }
}
