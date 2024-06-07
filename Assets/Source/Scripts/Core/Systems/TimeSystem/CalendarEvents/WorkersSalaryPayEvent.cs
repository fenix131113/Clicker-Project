using Clicker.Core.Time;
using Clicker.Core.Workers;

public class WorkersSalaryPayEvent : CalendarEvent
{
    protected override string _eventName { get; set; } = "Workers salary";
    protected override string _description { get; set; } = "Salary_Pay_Event_Description";
    protected override int _eventPeriod { get; set; }
    protected override int _hour { get; set; }
    protected override bool _eventComplete { get; set; }

    private PlayerData _data;
    private WorkersManager _workersManager;

    public WorkersSalaryPayEvent(int eventPeriod, int hour, PlayerData data, WorkersManager workersManager)
    {
        _eventPeriod = eventPeriod;
        _hour = hour;
        _data = data;
        _workersManager = workersManager;
        _description = $"Payment of worker's salaries\n\n<color=\"red\"><size=40>{workersManager.SalayPerWorker * workersManager.Workers}$";
    }
    public override void EventAction()
    {
        _eventComplete = true;
        _workersManager.PaySalary();
    }
}