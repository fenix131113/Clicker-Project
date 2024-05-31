using Zenject;
namespace Clicker.Core.SkillSystem.Skills
{
    public class ConsumableQualitySkill : SkillBase
    {
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                // Default = 50
                case 1:
                    data.PassiveMoneyController.SetConsumablesCost(data.PassiveMoneyController.ConsumablesCost - 5);
                    break;
                case 2:
                    data.PassiveMoneyController.SetConsumablesCost(data.PassiveMoneyController.ConsumablesCost - 10);
                    break;
                case 3:
                    data.PassiveMoneyController.SetConsumablesCost(data.PassiveMoneyController.ConsumablesCost - 10);
                    break;
                case 4:
                    data.PassiveMoneyController.SetConsumablesCost(data.PassiveMoneyController.ConsumablesCost - 10);
                    break;
                case 5:
                    data.PassiveMoneyController.SetConsumablesCost(data.PassiveMoneyController.ConsumablesCost - 15);
                    break;
            }
        }
    }
}