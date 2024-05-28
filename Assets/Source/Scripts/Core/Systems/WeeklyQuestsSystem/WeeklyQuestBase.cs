public class WeeklyQuestBase
{
    protected WeeklyQuestsController _controller;
    protected string _description;
    protected int _minNeedProgress;
    protected int _maxNeedProgress;
    public string Description => _description;
    public int MinNeedProgress => _minNeedProgress;
    public int MaxNeedProgress => _maxNeedProgress;

    public delegate void OnProgressIncreased();
    public OnProgressIncreased onProgressIncreased;

    public WeeklyQuestBase(string desctiption, WeeklyQuestsController controller, int minNeedProgress, int maxNeedProgress)
    {
        _description = desctiption;
        _controller = controller;
        _minNeedProgress = minNeedProgress;
        _maxNeedProgress = maxNeedProgress;
    }
    protected void QuestEvent()
    {
        onProgressIncreased?.Invoke();
    }
}