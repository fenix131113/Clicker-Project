using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public class UtilityDownSkill : SkillBase
    {
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
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
}