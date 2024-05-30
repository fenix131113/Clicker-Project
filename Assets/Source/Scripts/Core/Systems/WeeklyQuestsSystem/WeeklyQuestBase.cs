public class WeeklyQuestBase
{
    protected WeeklyQuestsController _controller;
    protected string _description;
    protected WeeklyQuestDifficultItem[] _weeklyQuestDifficultItems;


    public string Description => _description;
    public WeeklyQuestDifficultItem[] WeeklyQuestDifficultItems => _weeklyQuestDifficultItems;

    public delegate void OnProgressIncreased(int count = 1);
    public OnProgressIncreased onProgressIncreased;

    public WeeklyQuestBase(string desctiption, WeeklyQuestsController controller, WeeklyQuestDifficultItem[] difficultItems)
    {
        _description = desctiption;
        _controller = controller;
        _weeklyQuestDifficultItems = difficultItems;
    }
    protected void QuestEvent()
    {
        onProgressIncreased?.Invoke();
    }

    protected void QuestEvent(int count)
    {
        onProgressIncreased?.Invoke(count);
    }
    public virtual void UnsubscribeEvents()
    {
        throw new System.NotImplementedException();
    }
}