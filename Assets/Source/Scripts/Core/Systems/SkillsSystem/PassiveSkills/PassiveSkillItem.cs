using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Clicker.Core.SkillSystem
{
    public class PassiveSkillItem : MonoBehaviour, IPointerClickHandler
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
                _notices.CreateNewNotification($"You have unlocked a new passive skill \"{Skill.SkillName}\"");
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _objectsContainer.SkillInfoPanel.UpdateInfoPassiveSkill(this);
            _objectsContainer.SkillInfoPanel.SelectSkill(this);
        }
        public void SetSelectedVisual()
        {
            if (!_isOpened)
                _currentImage.sprite = _selectedIcon;
        }
        public void SetUnselectedVisual()
        {
            if (!_isOpened)
                _currentImage.sprite = _defaultIcon;
        }
    }
}