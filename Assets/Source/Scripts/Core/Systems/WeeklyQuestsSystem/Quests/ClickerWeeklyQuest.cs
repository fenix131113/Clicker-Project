public class ClickerWeeklyQuest : WeeklyQuestBase
{
    private MainClicker _mainClicker;
    public ClickerWeeklyQuest(string desctiption, WeeklyQuestsController controller, WeeklyQuestDifficultItem[] difficultItems, MainClicker clickerScript) : base(desctiption, controller, difficultItems) 
    {
        _mainClicker = clickerScript;
        clickerScript.onClick += QuestEvent;
    }

    public override void UnsubscribeEvents()
    {
        _mainClicker.onClick -= QuestEvent;
    }
}