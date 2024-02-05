using Clicker.Core.Earnings;
using Clicker.Core.Time;

namespace Clicker.Core.Workers
{
    public class WorkersManager
    {
        private readonly PlayerData data;
        private readonly EarningsManager earningsManager;

        public WorkersManager(PlayerData data, EarningsManager earningsManager)
        {
            this.data = data;

            CalendarManager.onNewDay += OnNewDay;
            this.earningsManager = earningsManager;
        }

        private void OnNewDay(int dayIndex, DayType dayType)
        {
            int earning = data.MoneyPerWorker * data.Workers;
            int salary = data.SalayPerWorker * data.Workers;
            data.Money += earning;
            earningsManager.AddOrUpdateHistoryEntry(dayIndex, "Доход с работников", earning);

            if(dayIndex % 7 == 0)
            {
                data.Money -= salary;
                earningsManager.AddOrUpdateHistoryEntry(dayIndex, "Зарплата", salary);
            }
        }
    }
}
