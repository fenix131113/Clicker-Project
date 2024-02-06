using UnityEngine;

namespace Clicker.Core.SkillSystem
{
	public class SkillBase : MonoBehaviour
	{
		[SerializeField][HideInInspector] protected string skillName;
		public string SkillName => skillName;


        [SerializeField][HideInInspector] protected string description;
		public string Description => description;


        [SerializeField][HideInInspector] protected int level = 1;
		public int Level => level;

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
