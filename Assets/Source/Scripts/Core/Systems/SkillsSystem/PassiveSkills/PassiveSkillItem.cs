using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Clicker.Core.SkillSystem
{
    public class PassiveSkillItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _currentImage;
        [SerializeField] private Sprite _openedIcon;
        [SerializeField] private Sprite _selectedIcon;
        [SerializeField] private Sprite _defaultIcon;
        [SerializeField] private PassiveSkillBase _passiveSkill;

        private bool _isOpened;
        private GlobalObjectsContainer _objectsContainer;
        private NoticeSystem _notices;

        public bool IsOpened => _isOpened;
        public PassiveSkillBase Skill => _passiveSkill;

        [Inject]
        private void Init(GlobalObjectsContainer objectsContainer, NoticeSystem notices)
        {
            _objectsContainer = objectsContainer;
            _notices = notices;
        }
        public void OpenSkill(bool withNotification = true)
        {
            _isOpened = true;
            _currentImage.sprite = _openedIcon;
            if (withNotification)
                _notices.CreateNewNotification($"Вы открыли новый пассивный навык \"{Skill.SkillName}\"");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _objectsContainer.SkillInfoPanelRectTransform.GetComponent<SkillInfoPanel>().UpdateInfoPassiveSkill(this);
            _objectsContainer.SkillInfoPanelRectTransform.gameObject.SetActive(true);

            if (!_isOpened)
                _currentImage.sprite = _selectedIcon;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isOpened)
                _currentImage.sprite = _defaultIcon;
            _objectsContainer.SkillInfoPanelRectTransform.gameObject.SetActive(false);
        }
    }
}