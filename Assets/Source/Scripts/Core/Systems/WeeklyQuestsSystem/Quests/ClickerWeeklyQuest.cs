public class ClickerWeeklyQuest : WeeklyQuestBase
{
    public ClickerWeeklyQuest(string desctiption, WeeklyQuestsController controller, int minNeedProgress, int maxNeedProgress, MainClicker clickerScript) : base(desctiption, controller, minNeedProgress, maxNeedProgress) 
    {
        clickerScript.onClick += QuestEvent;
    }
}