using Clicker.Core.SkillSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillDescriptionText;
    [SerializeField] private TMP_Text skillMoneyCostText;
    [SerializeField] private TMP_Text skillPointsCostText;
    [SerializeField] private TMP_Text fillerAmountText;
    [SerializeField] private GameObject buyButtonObject;
    [SerializeField] private Image filler;
    [SerializeField] private GameObject fillerObject;
    [SerializeField] private PassiveSkillItem startPassiveSelectedSkill;

    private SkillItem selectedSkillItem;
    private PassiveSkillItem selectedPassiveItem;

    public void SelectFirstSkill()
    {
        UpdateInfoPassiveSkill(startPassiveSelectedSkill);
        SelectSkill(startPassiveSelectedSkill);
    }
    public void BuySelectedSkill()
    {
        if (!selectedSkillItem)
            return;

        selectedSkillItem.Buy();
    }
    public void SelectSkill(SkillItem skillItem)
    {
        if (selectedSkillItem)
            selectedSkillItem.SetUnselectedVisual();
        if (selectedPassiveItem)
        {
            selectedPassiveItem.SetUnselectedVisual();
            selectedPassiveItem = null;
        }
        selectedSkillItem = skillItem;
        selectedSkillItem.SetSelectedVisual();
    }
    public void SelectSkill(PassiveSkillItem passiveSkillItem)
    {
        if (selectedSkillItem)
        {
            selectedSkillItem.SetUnselectedVisual();
            selectedSkillItem = null;
        }
        if (selectedPassiveItem)
            selectedPassiveItem.SetUnselectedVisual();

        selectedPassiveItem = passiveSkillItem;
        selectedPassiveItem.SetSelectedVisual();
    }

    public void UpdateInfoCommonSkill(SkillItem skill)
    {
        skillMoneyCostText.gameObject.SetActive(true);
        skillPointsCostText.gameObject.SetActive(true);
        fillerObject.SetActive(false);


        bool allNeededSkills = true;

        foreach (SkillItem skillItem in skill.NeedToBuyItems)
            if (!skillItem.IsBuyed)
            {
                allNeededSkills = false;
                break;
            }
        if(allNeededSkills)
            foreach (PassiveSkillItem passiveItem in skill.NeedPassiveItems)
                if (!passiveItem.IsOpened)
                {
                    allNeededSkills = false;
                    break;
                }


        if (skill.IsBuyed || !allNeededSkills)
            buyButtonObject.SetActive(false);
        else
            buyButtonObject.SetActive(true);

        //Set data in texts logic
        skillNameText.text = skill.Skill.SkillName;
        skillDescriptionText.text = skill.Skill.Description;
        skillMoneyCostText.text = $"Money: {skill.MoneyCost}$";
        skillPointsCostText.text = $"Skill points: {skill.SkillPointsCost}";
    }
    public void UpdateInfoPassiveSkill(PassiveSkillItem skill)
    {
        skillMoneyCostText.gameObject.SetActive(false);
        skillPointsCostText.gameObject.SetActive(false);
        fillerObject.SetActive(true);

        buyButtonObject.SetActive(false);

        fillerAmountText.text = $"{Mathf.Clamp(skill.Skill.GetCurrentCounter(), 0, skill.Skill.CounterGoal)} / {skill.Skill.CounterGoal}";
        filler.fillAmount = (float)skill.Skill.GetCurrentCounter() / skill.Skill.CounterGoal;

        //Set data in texts logic
        skillNameText.text = skill.Skill.SkillName;
        skillDescriptionText.text = skill.Skill.Description;
    }
}