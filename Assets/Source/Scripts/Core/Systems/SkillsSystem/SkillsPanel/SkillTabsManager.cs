using UnityEngine;

namespace Clicker.Core.SkillSystem
{
    public class SkillTabsManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] tabs;
        [SerializeField] private SkillTab[] tabsScripts;
        private GameObject activeTab;

        private void Start()
        {
            foreach (SkillTab tab in tabsScripts)
                tab.Init(this);
            SwitchTab(0);
        }
        public void SwitchTab(int tabIndex)
        {
            if (activeTab == tabs[tabIndex])
                return;

            activeTab?.SetActive(false);
            activeTab = tabs[tabIndex];
            activeTab.SetActive(true);
        }
    }
}