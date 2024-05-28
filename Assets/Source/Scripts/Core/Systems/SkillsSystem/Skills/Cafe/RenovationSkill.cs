

using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public class RenovationSkill : SkillBase
    {
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.SetMoneyPerFood(data.MoneyPerFood + 1);
                    break;
                case 2:
                    data.SetMoneyPerFood(data.MoneyPerFood + 1);
                    break;
                case 3:
                    data.SetMoneyPerFood(data.MoneyPerFood + 1);
                    break;
                case 4:
                    data.SetMoneyPerFood(data.MoneyPerFood + 1);
                    break;
                case 5:
                    data.SetMoneyPerFood(data.MoneyPerFood + 1);
                    break;
            }
        }
    }
}