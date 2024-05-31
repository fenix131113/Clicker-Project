
namespace Clicker.Core.SkillSystem
{
    public class PassiveClickerSkill : PassiveSkillBase
    {
        public override void StartControllerCall()
        {
            _mainClicker.onClick += CheckConditions;
        }

        public override int GetCurrentCounter()
        {
            return data.Clicks;
        }

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.SetClickPower(2);
                    _weeklyQuests.SetQuestsDifficultModifier(1.2f);
                    _weeklyQuests.SetRewardModifier(1.2f);
                    _tournamentManager.SetFoodCountModifier(1.5f);
                    break;
                case 2:
                    data.SetClickPower(5);
                    _weeklyQuests.SetQuestsDifficultModifier(2f);
                    _weeklyQuests.SetRewardModifier(1.5f);
                    _tournamentManager.SetFoodCountModifier(2f);
                    break;
                case 3:
                    data.SetClickPower(10);
                    _weeklyQuests.SetQuestsDifficultModifier(4f);
                    _weeklyQuests.SetRewardModifier(1.8f);
                    _tournamentManager.SetFoodCountModifier(7f);
                    break;
                case 4:
                    data.SetClickPower(20);
                    _weeklyQuests.SetQuestsDifficultModifier(6f);
                    _weeklyQuests.SetRewardModifier(2f);
                    _tournamentManager.SetFoodCountModifier(10f);
                    break;
                case 5:
                    data.SetClickPower(40);
                    _weeklyQuests.SetQuestsDifficultModifier(8f);
                    _weeklyQuests.SetRewardModifier(2.5f);
                    _tournamentManager.SetFoodCountModifier(18f);
                    break;
            }
        }
    }
}