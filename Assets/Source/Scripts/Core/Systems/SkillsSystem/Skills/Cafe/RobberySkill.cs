using UnityEngine;
using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
    public sealed class RobberySkill : SkillBase
    {
        public override int MaxLevels => 5;

        [Inject]
        private void Init(PlayerData data) => this.data = data;

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.RobberyManager.SetRobberyChance(0.05f);
                    break;
                case 2:
                    data.RobberyManager.SetRobberyMoneyPercent(20);
                    break;
                case 3:
                    data.RobberyManager.SetRobberyChance(0.025f);
                    break;
                case 4:
                    data.RobberyManager.SetRobberyMoneyPercent(10);
                    break;
                case 5:
                    data.RobberyManager.SetAllowRobbery(false);
                    break;
                default:
                    Debug.LogError("This level action doesn't exist!");
                    break;
            }
        }
    }
}
