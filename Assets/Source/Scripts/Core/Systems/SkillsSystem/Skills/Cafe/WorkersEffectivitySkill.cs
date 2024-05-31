using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public class WorkersEffectivitySkill : SkillBase
    {
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker + 5);
                    break;
                case 2:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker + 25);
                    break;
                case 3:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker + 150);
                    break;
                case 4:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker + 250);
                    break;
                case 5:
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker + 300);
                    break;
            }
        }
    }
}