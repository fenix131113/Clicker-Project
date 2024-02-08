using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public class EndGameSkill : SkillBase
    {
        public override int MaxLevels => 1;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    
                    break;
            }
        }
    }
}