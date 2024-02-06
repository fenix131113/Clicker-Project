using Clicker.Core.Earnings;
using Clicker.Core.Time;
using UnityEngine;

namespace Clicker.Core.Workers
{
    public class WorkersManager
    {
        #region Saving Data
        [SerializeField] private int _workers = 2;
        public int Workers => _workers;
        public void AddWorkers(int count) => _workers += count;


        [SerializeField] private int _moneyPerWorker = 1;
        public int MoneyPerWorker => _moneyPerWorker;
        public void AddMoneyPerWorker(int count) => _moneyPerWorker += count;


        [SerializeField] private int _salaryPerWorker = 30;
        public int SalayPerWorker => _salaryPerWorker;
        public void ReduceSalaryPerWorker(int count) => _salaryPerWorker -= count;
        #endregion

        private PlayerData data;
        private readonly EarningsManager earningsManager;

        public void SetData(PlayerData data) => this.data = data;
        public WorkersManager(EarningsManager earningsManager)
        {
            CalendarManager.onNewDay += OnNewDay;
            this.earningsManager = earningsManager;
        }

        private void OnNewDay(int dayIndex, DayType dayType)
        {
            int earning = MoneyPerWorker * Workers;
            int salary = SalayPerWorker * Workers;
            data.Money += earning;
            earningsManager.AddOrUpdateHistoryEntry(dayIndex - 1, "Доход с работников", earning);

            if(dayIndex % 7 == 0)
            {
                data.Money -= salary;
                earningsManager.AddOrUpdateHistoryEntry(dayIndex - 1, "Зарплата", salary);
            }
        }
    }
}
