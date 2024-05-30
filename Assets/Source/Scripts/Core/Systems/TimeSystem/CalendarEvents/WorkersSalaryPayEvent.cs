using Clicker.Core.Time;
using Clicker.Core.Workers;

public class WorkersSalaryPayEvent : CalendarEvent
{
    protected override string _eventName { get; set; } = "Зарплата рабочим";
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
        _description = $"Выплата зарплаты рабочим\n\n<color=\"red\"><size=34>{workersManager.SalayPerWorker * workersManager.Workers}$";
    }
    public override void EventAction()
    {
        _eventComplete = true;
        _workersManager.PaySalary();
    }
}