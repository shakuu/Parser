using System;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class ParameterCtorHealingDonePerSecondViewModel : HealingDonePerSecondViewModel
    {
        public ParameterCtorHealingDonePerSecondViewModel(Guid id, string characterName, double healingDonePerSecond)
        {
            base.Id = id;
            base.CharacterName = characterName;
            base.HealingDonePerSecond = healingDonePerSecond;
        }
    }
}
