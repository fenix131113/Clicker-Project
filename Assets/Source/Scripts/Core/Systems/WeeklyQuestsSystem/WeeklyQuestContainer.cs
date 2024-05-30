using UnityEngine;

[System.Serializable]
public class WeeklyQuestContainer : System.IComparable<WeeklyQuestContainer>
{
    private int _questIndex;
    private int _needProgress;
    private int _daysLeft;
    private int _progress;
    private bool _complete;
    private bool _isLoose;
    private int _moneyReward;
    private int _skillPointsReward;
    private int _visualBlockIndex;

    public int QuestIndex => _questIndex;
    public int NeedProgress => _needProgress;
    public int DaysLeft => _daysLeft;
    public int Progress => _progress;
    public bool Complete => _complete;
    public bool IsLoose => _isLoose;
    public int MoneyReward => _moneyReward;
    public int SkillPointsReward => _skillPointsReward;

    public WeeklyQuestContainer(int questIndex, WeeklyQuestDifficultItem difficultItem, float difficultModifier, float rewardModifier)
    {
        _questIndex = questIndex;

        if(questIndex != 0)
        _needProgress = (int)(difficultItem.NeedProgress * difficultModifier);
        else
            _needProgress = difficultItem.NeedProgress;

        _daysLeft = difficultItem.DaysLeft;
        _moneyReward = (int)(difficultItem.MoneyReward * rewardModifier);
        _skillPointsReward = difficultItem.SkillPointsReward;
    }

    public void IncreaseProgress(int count)
    {
        if(!IsLoose && !Complete)
        _progress = Mathf.Clamp(_progress + count, 0, _needProgress);
    }

    public void DecreaseDaysLeft()
    {
        _daysLeft = Mathf.Clamp(_daysLeft - 1, 0, int.MaxValue);
    }

    public void SetCompleted()
    {
        _complete = true;
    }
    public void SetLoose()
    {
        _isLoose = true;
    }

    public int CompareTo(WeeklyQuestContainer other)
    {
        if (other.QuestIndex > _questIndex)
            return -1;
        else if (other.QuestIndex == _questIndex)
            return 0;
        else
            return 1;
    }
}