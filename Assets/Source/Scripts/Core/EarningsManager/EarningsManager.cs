using Clicker.Core.Time;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Core.Earnings
{
    public class EarningsManager
    {
        public readonly List<EarningDayHistory> _earningsList = new();
        public IReadOnlyCollection<EarningDayHistory> EarningsList => _earningsList;

        public void AddOrUpdateHistoryEntry(int dayNum, string categoryName, int earnMoney, int expensesMoney = 0)
        {
            if (CalendarManager.Day - 1 > dayNum)
            {
                Debug.LogWarning("You trying to add earning into the past!");
                return;
            }
            EarningDayHistory earningDay = GetEarnDayHistoryByDayIndex(dayNum - 1);

            //If earning in this day is clear - add new day history
            if (earningDay != null)
                earningDay.AddOrUpdateCategory(categoryName, earnMoney, expensesMoney);
            else // if not clear - add new category or update the old one
            {
                _earningsList.Add(new EarningDayHistory(dayNum - 1, new(categoryName, earnMoney, expensesMoney)));
                if (_earningsList.Count > 14)
                    _earningsList.RemoveAt(0);
            }
        }

        public EarningDayHistory GetEarnDayHistoryByDayIndex(int dayIndex)
        {
            if (EarningsList.Count == 0)
                return null;

            foreach (EarningDayHistory item in _earningsList)
                if (item.Day == dayIndex)
                    return item;

            return null;
        }
    }
}
