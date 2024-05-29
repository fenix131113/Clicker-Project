
public class EarnMoneyWeeklyQuest : WeeklyQuestBase
{
    WeeklyQuestsController controller;
    public EarnMoneyWeeklyQuest(string desctiption, WeeklyQuestsController controller, int minNeedProgress, int maxNeedProgress) : base(desctiption, controller, minNeedProgress, maxNeedProgress)
    {
        controller.Data.onGetMoney += QuestEvent;
        controller.Data.onGetMoney += (int sc) => UnityEngine.Debug.Log(sc.ToString()  + " ---");
    }

    public override void UnsubscribeEvents()
    {
        controller.Data.onGetMoney -= QuestEvent;
    }
}
