public class FoodCookingWeeklyQuest : WeeklyQuestBase
{
    private MainClicker _mainClicker;
    public FoodCookingWeeklyQuest(string desctiption, WeeklyQuestsController controller, int minNeedProgress, int maxNeedProgress, MainClicker mainClicker) : base(desctiption, controller, minNeedProgress, maxNeedProgress)
    {
        _mainClicker = mainClicker;
        mainClicker.onFoodCooked += QuestEvent;
    }

    public override void UnsubscribeEvents()
    {
        _mainClicker.onFoodCooked -= QuestEvent;
    }
}