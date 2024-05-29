public class ClickerWeeklyQuest : WeeklyQuestBase
{
    private MainClicker _mainClicker;
    public ClickerWeeklyQuest(string desctiption, WeeklyQuestsController controller, int minNeedProgress, int maxNeedProgress, MainClicker clickerScript) : base(desctiption, controller, minNeedProgress, maxNeedProgress) 
    {
        _mainClicker = clickerScript;
        clickerScript.onClick += QuestEvent;
    }

    public override void UnsubscribeEvents()
    {
        _mainClicker.onClick -= QuestEvent;
    }
}