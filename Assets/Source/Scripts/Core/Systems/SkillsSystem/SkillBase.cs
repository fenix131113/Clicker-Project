using UnityEngine;

namespace Clicker.Core.SkillSystem
{
	public class SkillBase : MonoBehaviour
	{
		[SerializeField] protected string skillName = "";
		public string SkillName => skillName;


        [SerializeField][Multiline(5)] protected string description = "";
		public string Description => description;


        [SerializeField] protected int level = 1;
		public int Level => level;


        protected PlayerData data;
        public void SetData(PlayerData data) => this.data = data;

        public virtual int MaxLevels { get { return 1; } }

		public virtual void BuyAction()
		{
			switch (level)
			{
				default:
					Debug.LogError("This level action doesn't exist!");
					break;
			}
		}
	}
}
