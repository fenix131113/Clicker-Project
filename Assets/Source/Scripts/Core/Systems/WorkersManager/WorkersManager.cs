using Clicker.Core.Earnings;
using Clicker.Core.Time;
using Newtonsoft.Json;

namespace Clicker.Core.Workers
{
    public class WorkersManager
    {
        [JsonIgnore] private int _workers = 0;
        [JsonIgnore] public int Workers => _workers;
        public void AddWorkers(int count) => _workers += count;


        [JsonIgnore] private int _salaryPerWorker = 70;
        [JsonIgnore] public int SalayPerWorker => _salaryPerWorker;
        public void SetSalaryPerWorker(int count) => _salaryPerWorker = count;


        [JsonIgnore] private int _workerFoodPerDay = 5;
        [JsonIgnore] public int WorkerFoodPerDay => _workerFoodPerDay;
        public void SetWorkerFoodPerDay(int count) => _workerFoodPerDay = count;


        [JsonIgnore] private PlayerData data;
        [JsonIgnore] private NoticeSystem notices;
        [JsonIgnore] private CalendarManager calendarManager;
        [JsonIgnore] private readonly EarningsManager earningsManager;

        public void SetData(PlayerData data) => this.data = data;
        public WorkersManager(EarningsManager earningsManager, CalendarManager calendarManager, NoticeSystem notices)
        {
            calendarManager.onNewDay += OnNewDay;
            this.earningsManager = earningsManager;
            this.notices = notices;
            this.calendarManager = calendarManager;
        }

        private void OnNewDay(int day, DayType dayType)
        {
            if (Workers > 0)
            {
                int earning = WorkerFoodPerDay * Workers * data.MoneyPerFood;
                data.Money += earning;
                earningsManager.AddOrUpdateHistoryEntry(day, "Доход с работников", earning);
                notices.CreateNewNotification($"Ваши рабочие принесли доход: {earning}");
            }
        }

        public void PaySalary()
        {
            int salary = SalayPerWorker * Workers;
            data.Money -= salary;
            earningsManager.AddOrUpdateHistoryEntry(calendarManager.Day, "Зарплата", 0, salary);
        }
    }
}
