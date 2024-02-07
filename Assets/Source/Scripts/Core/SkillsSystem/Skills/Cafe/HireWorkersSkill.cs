using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public class HireWorkersSkill : SkillBase
    {
        private PlayerData data;
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.WorkersManager.AddWorkers(1);
                    break;
                case 2:
                    data.WorkersManager.AddWorkers(1);
                    break;
                case 3:
                    data.WorkersManager.AddWorkers(1);
                    break;
                case 4:
                    data.WorkersManager.AddWorkers(1);
                    break;
                case 5:
                    data.WorkersManager.AddWorkers(1);
                    break;
            }
        }
    }
}