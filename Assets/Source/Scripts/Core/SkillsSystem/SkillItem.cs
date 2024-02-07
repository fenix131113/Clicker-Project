using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Clicker.Core.SkillSystem
{
    public class SkillItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        [SerializeField] private Sprite buyedIcon;
        [SerializeField] private Sprite selectedIcon;
        [SerializeField] private Sprite defaultIcon;
        [SerializeField] private SkillItem[] needToBuyItems;

        [SerializeField] private int _moneyCost;
        public int MoneyCost => _moneyCost;
#if UNITY_EDITOR
        public int MoneyCostEditor
        {
            get { return _moneyCost; }
            set { _moneyCost = value; }
        }
#endif


        [SerializeField] private int _skillPointsCost;
        public int SkillPointsCost => _skillPointsCost;

#if UNITY_EDITOR
        public int SkillPointsCostEditor
        {
            get { return _skillPointsCost; }
            set { _skillPointsCost = value; }
        }
#endif


        private SkillCostType _skillCostType;
        public SkillCostType SkillCostType => _skillCostType;
#if UNITY_EDITOR
        public SkillCostType SkillCostTypeEditor
        {
            get { return _skillCostType; }
            set { _skillCostType = value; }
        }
#endif


        [SerializeField] private SkillBase _skill;
        public SkillBase Skill => _skill;


        private bool _isBuyed;
        public bool IsBuyed => _isBuyed;


        private bool _effectsApplyed;
        public bool IsEffectsApplyed => _effectsApplyed;


        private GlobalObjectsContainer objectsContainer;
        private PlayerData data;
        private Image img;

        [Inject]
        public void Init(PlayerData data, GlobalObjectsContainer objectsContainer)
        {
            this.data = data;
            this.objectsContainer = objectsContainer;
        }

        public void Buy()
        {
            if (_isBuyed)
                return;
            if (needToBuyItems.Length > 0)
                foreach (SkillItem item in needToBuyItems)
                    if (!item.IsBuyed)
                        return;

            switch (_skillCostType)
            {
                case SkillCostType.BOTH:
                    if (data.Money >= _moneyCost && data.SkillPoints >= SkillPointsCost)
                    {
                        data.Money -= _moneyCost;
                        data.RemoveSkillPoints(SkillPointsCost);
                        _isBuyed = true;
                    }
                    break;
                case SkillCostType.MONEY:
                    if (data.Money >= _moneyCost)
                    {
                        data.Money -= _moneyCost;
                        _isBuyed = true;
                    }
                    break;
                case SkillCostType.SKILL_POINTS:
                    if (data.SkillPoints >= _skillPointsCost)
                    {
                        data.RemoveSkillPoints(SkillPointsCost);
                        _isBuyed = true;
                    }
                    break;
            }
            CheckBuyedStatus();
        }

        private void Start() => img = GetComponent<Image>();

        /// <summary>
        /// Update visual and data if skill is buyed
        /// </summary>
        public void CheckBuyedStatus()
        {
            if (!_isBuyed || _effectsApplyed)
                return;

            //Set buyed sprite if buyed
            img.sprite = buyedIcon;

            //Add skill effects
            Skill.BuyAction();
            _effectsApplyed = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (needToBuyItems.Length > 0)
                foreach (SkillItem item in needToBuyItems)
                    if (!item.IsBuyed)
                        return;
            if (!_isBuyed)
                img.sprite = selectedIcon;
            objectsContainer.SkillInfoPanelRectTransform.GetComponent<SkillInfoPanel>().UpdateInfo(this);
            objectsContainer.SkillInfoPanelRectTransform.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isBuyed)
                img.sprite = defaultIcon;
            objectsContainer.SkillInfoPanelRectTransform.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData) => Buy();
    }
}