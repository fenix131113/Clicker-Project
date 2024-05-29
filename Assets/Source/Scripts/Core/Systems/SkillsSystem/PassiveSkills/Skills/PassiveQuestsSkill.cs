using Clicker.Core.SkillSystem;

public class PassiveQuestsSkill : PassiveSkillBase
{
    public override void StartControllerCall()
    {
        _weeklyQuests.onQuestComplete += CheckConditions;
    }

    public override int GetCurrentCounter()
    {
        return data.WeeklyQuestsComplete;
    }

    public override void BuyAction()
    {
        switch (level)
        {
            // Default = 50
            case 1:
                data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost - 5);
                break;
            case 2:
                data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost - 5);
                break;
            case 3:
                data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost - 5);
                break;
            case 4:
                data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost - 5);
                break;
            case 5:
                data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost - 10);
                break;
        }
    }
}