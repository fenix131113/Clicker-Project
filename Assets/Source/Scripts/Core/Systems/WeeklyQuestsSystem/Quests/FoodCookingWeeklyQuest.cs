public class FoodCookingWeeklyQuest : WeeklyQuestBase
{
    private MainClicker _mainClicker;
    public FoodCookingWeeklyQuest(string desctiption, WeeklyQuestsController controller, WeeklyQuestDifficultItem[] difficultItems, MainClicker mainClicker) : base(desctiption, controller, difficultItems)
    {
        _mainClicker = mainClicker;
        mainClicker.onFoodCooked += QuestEvent;
    }

    public override void UnsubscribeEvents()
    {
        _mainClicker.onFoodCooked -= QuestEvent;
    }
}