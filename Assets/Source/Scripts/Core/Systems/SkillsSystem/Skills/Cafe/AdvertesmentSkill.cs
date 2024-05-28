using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public class AdvertesmentSkill : SkillBase
    {
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 1);
                    break;
                case 2:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 2);
                    break;
                case 3:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 3);
                    break;
                case 4:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 4);
                    break;
                case 5:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    break;
            }
        }
    }
}