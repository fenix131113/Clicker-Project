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


        [SerializeField] private int _skillPointsCost;
        public int SkillPointsCost => _skillPointsCost;


        [SerializeField] private SkillCostType _skillCostType;
        public SkillCostType SkillCostType => _skillCostType;


        [SerializeField] private SkillBase _skill;
        public SkillBase Skill => _skill;


        private bool _isBuyed;
        public bool IsBuyed => _isBuyed;


        private bool _effectsApplyed;
        public bool IsEffectsApplyed => _effectsApplyed;


        private GlobalObjectsContainer _objectsContainer;
        private AudioController _audioController;
        private PlayerData _data;
        private Image _img;

        [Inject]
        public void Init(PlayerData data, GlobalObjectsContainer objectsContainer, AudioController audioController)
        {
            _data = data;
            _audioController = audioController;
            _objectsContainer = objectsContainer;
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
                    if (_data.Money >= _moneyCost && _data.SkillPoints >= SkillPointsCost)
                    {
                        _data.Money -= _moneyCost;
                        _data.RemoveSkillPoints(SkillPointsCost);
                        _isBuyed = true;
                        _audioController.PlaySound(_objectsContainer.AnnouncmentSound);
                    }
                    break;
                case SkillCostType.MONEY:
                    if (_data.Money >= _moneyCost)
                    {
                        _data.Money -= _moneyCost;
                        _isBuyed = true;
                        _audioController.PlaySound(_objectsContainer.AnnouncmentSound);
                    }
                    break;
                case SkillCostType.SKILL_POINTS:
                    if (_data.SkillPoints >= _skillPointsCost)
                    {
                        _data.RemoveSkillPoints(SkillPointsCost);
                        _isBuyed = true;
                        _audioController.PlaySound(_objectsContainer.AnnouncmentSound);
                    }
                    break;
            }
            CheckBuyedStatus();
        }

        private void Start() => _img = GetComponent<Image>();

        /// <summary>
        /// Update visual and data if skill is buyed
        /// </summary>
        public void CheckBuyedStatus()
        {
            if (!_isBuyed || _effectsApplyed)
                return;

            //Set buyed sprite if buyed
            if (_img == null)
                _img = GetComponent<Image>();
            _img.sprite = buyedIcon;

            //Add skill effects
            Skill.BuyAction();
            _effectsApplyed = true;
        }

        /// <summary>
        /// Set skill as buyed and apply skill effects
        /// </summary>
        public void RestoreSkillData()
        {
            _isBuyed = true;
            CheckBuyedStatus();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Include this code to hide skill info panel when skill !isBuyed

            //if (needToBuyItems.Length > 0)
            //    foreach (SkillItem item in needToBuyItems)
            //        if (!item.IsBuyed)
            //            return;
            if (!_isBuyed && needToBuyItems.Length > 0)
            {
                foreach (SkillItem item in needToBuyItems)
                    if (!item.IsBuyed)
                        break;
                    else if (item == needToBuyItems[^1] && item.IsBuyed)
                        _img.sprite = selectedIcon;
            }
            else if (needToBuyItems.Length == 0)
                _img.sprite = selectedIcon;

            _objectsContainer.SkillInfoPanelRectTransform.GetComponent<SkillInfoPanel>().UpdateInfo(this);
            _objectsContainer.SkillInfoPanelRectTransform.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isBuyed)
                _img.sprite = defaultIcon;
            _objectsContainer.SkillInfoPanelRectTransform.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData) => Buy();
    }
}