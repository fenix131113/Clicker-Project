using Zenject;
using UnityEngine;
using Clicker.Core.Tournament;

namespace Clicker.Core.SkillSystem
{
    public class PassiveSkillBase : SkillBase
    {
        [SerializeField] protected int _counterGoal;
        [SerializeField] protected PassiveSkillItem _skillItem;

        protected GlobalObjectsContainer _objectsContainer;
        protected TournamentManager _tournamentManager;
        protected MainClicker _mainClicker;

        public int CounterGoal => _counterGoal;

        [Inject]
        private void Init(PlayerData data, GlobalObjectsContainer objectsContainer, TournamentManager tournamentManager)
        {
            this.data = data;
            _tournamentManager = tournamentManager;
            _objectsContainer = objectsContainer;
            _mainClicker = _objectsContainer.ClickerScript;

            LoadDataIfExsist();
        }
        public void SetSkillItem(PassiveSkillItem skillItem)
        {
            _skillItem = skillItem;
        }
        private void LoadDataIfExsist()
        {
            if(GetCurrentCounter() >= _counterGoal)
            {
                BuyAction();
                _skillItem.OpenSkill(false);
            }
        }
        public void CheckConditions()
        {
            if (!_skillItem.IsOpened && GetCurrentCounter() >= _counterGoal)
            {
                BuyAction();
                _skillItem.OpenSkill();
            }
        }
        public virtual int GetCurrentCounter()
        {
            throw new System.Exception("Current counter value is null!");
        }
        public virtual void StartControllerCall() => throw new System.Exception();
    }
}