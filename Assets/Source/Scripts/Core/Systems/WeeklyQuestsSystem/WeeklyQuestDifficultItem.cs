using System;

[Serializable]
public class WeeklyQuestDifficultItem
{
    private int _daysLeft;
    private int _moneyReward;
    private int _skillPointsReward;
    private int _needProgress;

    public int DaysLeft => _daysLeft;
    public int MoneyReward => _moneyReward;
    public int SkillPointsReward => _skillPointsReward;
    public int NeedProgress => _needProgress;

    public WeeklyQuestDifficultItem(int daysLeft, int moneyReward, int skillPointsReward, int needProgress)
    {
        _daysLeft = daysLeft;
        _moneyReward = moneyReward;
        _skillPointsReward = skillPointsReward;
        _needProgress = needProgress;
    }
}