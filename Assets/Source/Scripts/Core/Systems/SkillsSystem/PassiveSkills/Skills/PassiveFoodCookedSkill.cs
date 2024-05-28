namespace Clicker.Core.SkillSystem
{
    public class PassiveFoodCookedSkill : PassiveSkillBase
    {
        public override void StartControllerCall()
        {
            _mainClicker.onFoodCooked += CheckConditions;
        }

        public override int GetCurrentCounter()
        {
            return data.CookedFood;
        }

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.SetMoneyPerFood(data.MoneyPerFood + 1);
                    break;
                case 2:
                    data.SetMoneyPerFood(data.MoneyPerFood + 2);
                    break;
                case 3:
                    data.SetMoneyPerFood(data.MoneyPerFood + 4);
                    break;
                case 4:
                    data.SetMoneyPerFood(data.MoneyPerFood + 8);
                    break;
                case 5:
                    data.SetMoneyPerFood(data.MoneyPerFood + 16);
                    break;
            }   
        }
    }
}