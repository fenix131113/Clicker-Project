
public class EarnMoneyWeeklyQuest : WeeklyQuestBase
{
    public EarnMoneyWeeklyQuest(string desctiption, WeeklyQuestsController controller, WeeklyQuestDifficultItem[] difficultItems) : base(desctiption, controller, difficultItems)
    {
        _controller.Data.onGetMoney += QuestEvent;
    }

    public override void UnsubscribeEvents()
    {
        _controller.Data.onGetMoney -= QuestEvent;
    }
}