using Clicker.Core.SkillSystem;
using TMPro;
using UnityEngine;

public class SkillInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillDescriptionText;
    [SerializeField] private TMP_Text skillMoneyCostText;
    [SerializeField] private TMP_Text skillPointsCostText;

    private RectTransform _rect;

    private void Awake() => _rect = GetComponent<RectTransform>();
    void Update()
    {
        if (Input.mousePosition.y > Screen.height / 2)
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - _rect.sizeDelta.y / 2);
        else
            _rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + _rect.sizeDelta.y / 2);
    }

    public void UpdateInfo(SkillItem skill)
    {
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
}
