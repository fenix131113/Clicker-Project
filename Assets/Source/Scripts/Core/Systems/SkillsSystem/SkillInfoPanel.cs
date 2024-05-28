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
    [SerializeField] private Image Filler;
    [SerializeField] private GameObject FillerObject;

    private RectTransform _rect;

    private void Awake() => _rect = GetComponent<RectTransform>();
    void Update()
    {
        if (Input.mousePosition.y > Screen.height / 2)
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - _rect.sizeDelta.y / 2 - 50);
        else
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + _rect.sizeDelta.y / 2 + 50);
    }

    public void UpdateInfoCommonSkill(SkillItem skill)
    {
        skillMoneyCostText.gameObject.SetActive(true);
        skillPointsCostText.gameObject.SetActive(true);
        FillerObject.SetActive(false);

        #region Set Pos Before Show
        if (_rect == null)
            _rect = GetComponent<RectTransform>();
        if (Input.mousePosition.y > Screen.height / 2)
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - _rect.sizeDelta.y / 2);
        else
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + _rect.sizeDelta.y / 2);
        #endregion

        //Set data in texts logic
        skillNameText.text = skill.Skill.SkillName;
        skillDescriptionText.text = skill.Skill.Description;
        skillMoneyCostText.text = $"Деньги: {skill.MoneyCost}$";
        skillPointsCostText.text = $"Очки навыков: {skill.SkillPointsCost}";
    }
    public void UpdateInfoPassiveSkill(PassiveSkillItem skill)
    {
        skillMoneyCostText.gameObject.SetActive(false);
        skillPointsCostText.gameObject.SetActive(false);
        FillerObject.SetActive(true);

        fillerAmountText.text = $"{Mathf.Clamp(skill.Skill.GetCurrentCounter(), 0, skill.Skill.CounterGoal)} / {skill.Skill.CounterGoal}";
        Filler.fillAmount = (float)skill.Skill.GetCurrentCounter() / skill.Skill.CounterGoal;

        #region Set Pos Before Show
        if (_rect == null)
            _rect = GetComponent<RectTransform>();
        if (Input.mousePosition.y > Screen.height / 2)
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - _rect.sizeDelta.y / 2);
        else
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + _rect.sizeDelta.y / 2);
        #endregion

        //Set data in texts logic
        skillNameText.text = skill.Skill.SkillName;
        skillDescriptionText.text = skill.Skill.Description;
    }
}
