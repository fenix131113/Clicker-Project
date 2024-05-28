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


        [JsonIgnore] private int _salaryPerWorker = 30;
        [JsonIgnore] public int SalayPerWorker => _salaryPerWorker;
        public void SetSalaryPerWorker(int count) => _salaryPerWorker = count;


        [JsonIgnore] private int _workerFoodPerDay = 5;
        [JsonIgnore] public int WorkerFoodPerDay => _workerFoodPerDay;
        public void SetWorkerFoodPerDay(int count) => _workerFoodPerDay = count;


        [JsonIgnore] private PlayerData data;
        [JsonIgnore] private readonly EarningsManager earningsManager;

        public void SetData(PlayerData data) => this.data = data;
        public WorkersManager(EarningsManager earningsManager,CalendarManager calendarManager)
        {
            calendarManager.onNewDay += OnNewDay;
            this.earningsManager = earningsManager;
        }

        private void OnNewDay(int dayIndex, DayType dayType)
        {
            if (Workers > 0)
            {
                int earning = WorkerFoodPerDay * Workers * data.MoneyPerFood;
                int salary = SalayPerWorker * Workers;
                data.Money += earning;
                earningsManager.AddOrUpdateHistoryEntry(dayIndex - 1, "Доход с работников", earning);

                if (dayIndex % 7 == 0)
                {
                    data.Money -= salary;
                    earningsManager.AddOrUpdateHistoryEntry(dayIndex - 1, "Зарплата", salary);
                }
            }
        }
    }
}
