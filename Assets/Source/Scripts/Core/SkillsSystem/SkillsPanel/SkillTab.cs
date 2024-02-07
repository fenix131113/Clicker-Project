using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Clicker.Core.SkillSystem
{
	public class SkillTab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private int tabIndex;
		[SerializeField] private Color selectedColor;
		[SerializeField] private Color defaultColor;

		private SkillTabsManager _tabManager;

		public void Init(SkillTabsManager tabManager) => _tabManager = tabManager;
		public void OnPointerClick(PointerEventData eventData) => _tabManager.SwitchTab(tabIndex);
		public void OnPointerEnter(PointerEventData eventData) => GetComponent<Image>().color = selectedColor;
		public void OnPointerExit(PointerEventData eventData) => GetComponent<Image>().color = defaultColor;
	}
}