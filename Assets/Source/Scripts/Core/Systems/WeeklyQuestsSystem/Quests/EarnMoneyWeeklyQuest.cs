
public class EarnMoneyWeeklyQuest : WeeklyQuestBase
{
    public EarnMoneyWeeklyQuest(string desctiption, WeeklyQuestsController controller, int minNeedProgress, int maxNeedProgress) : base(desctiption, controller, minNeedProgress, maxNeedProgress)
    {
        _controller.Data.onGetMoney += QuestEvent;
    }

    public override void UnsubscribeEvents()
    {
        _controller.Data.onGetMoney -= QuestEvent;
    }
}