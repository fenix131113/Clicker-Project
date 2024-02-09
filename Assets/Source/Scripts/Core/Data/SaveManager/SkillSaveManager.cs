using Clicker.Core.SkillSystem;
using System.Collections.Generic;
using UnityEngine;

public class SkillSaveManager : MonoBehaviour
{
    [SerializeField] private SkillItem[] allSkills;


    private PlayerData data;

    public void SetData(PlayerData data) => this.data = data;
    public void SaveSkillsData()
    {
        List<bool> buyedSkills = new();
        foreach (SkillItem item in allSkills)
            buyedSkills.Add(item.IsBuyed);

        data.SetBuyedSkillsArray(buyedSkills.ToArray());
    }
    public void LoadSkillsData(bool[] skillsData)
    {
        for (int i = 0; i < skillsData.Length; i++)
            if (skillsData[i])
            {
                allSkills[i].GetComponent<SkillBase>().SetData(data);
                allSkills[i].RestoreSkillData();
            }
    }
}