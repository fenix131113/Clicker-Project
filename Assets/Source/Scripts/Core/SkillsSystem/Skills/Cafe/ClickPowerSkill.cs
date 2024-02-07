using UnityEngine;
using Zenject;

namespace Clicker.Core.SkillSystem.Skills
{
	public class ClickPowerSkill : SkillBase
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
                    data.SetClickPower(2);
                    break;
                case 2:
                    data.SetClickPower(5);
                    break;
                case 3:
                    data.SetClickPower(10);
                    break;
                case 4:
                    data.SetClickPower(20);
                    break;
                case 5:
                    data.SetClickPower(40);
                    break;
            }
        }
    }
}