using UnityEngine;

namespace Clicker.Core.SkillSystem
{
    public class PassiveSkillsController : MonoBehaviour
    {
        [SerializeField] private PassiveSkillBase[] _passiveSkills;

        private void Start()
        {
            foreach(var skill in _passiveSkills)
                skill.StartControllerCall();
        }
    }
}