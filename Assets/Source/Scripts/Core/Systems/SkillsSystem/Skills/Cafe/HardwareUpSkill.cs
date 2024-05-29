using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public class HardwareUpSkill : SkillBase
    {
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost + 20);
                    data.PassiveMoneyController.SetConsumablesPayPeriod(data.PassiveMoneyController.ConsumablesPayPeriod + 1);
                    break;
                case 2:
                    data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost + 30);
                    data.PassiveMoneyController.SetConsumablesPayPeriod(data.PassiveMoneyController.ConsumablesPayPeriod + 2);
                    break;
                case 3:
                    data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost + 50);
                    data.PassiveMoneyController.SetConsumablesPayPeriod(data.PassiveMoneyController.ConsumablesPayPeriod + 2);
                    break;
                case 4:
                    data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost + 80);
                    data.PassiveMoneyController.SetConsumablesPayPeriod(data.PassiveMoneyController.ConsumablesPayPeriod + 2);
                    break;
                case 5:
                    data.PassiveMoneyController.SetUtilityServiceCost(data.PassiveMoneyController.UtilityServiceCost + 120);
                    data.PassiveMoneyController.SetConsumablesPayPeriod(data.PassiveMoneyController.ConsumablesPayPeriod + 3);
                    break;
            }
        }
    }
}