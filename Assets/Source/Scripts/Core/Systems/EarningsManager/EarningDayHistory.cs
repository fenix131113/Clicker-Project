using System;
using System.Collections.Generic;

namespace Clicker.Core.Earnings
{
    [Serializable]
    public class EarningDayHistory
    {
        private readonly int _day;
        public int Day => _day;

        private List<EarningsHistoryCategory> _categories = new();
        public IReadOnlyCollection<EarningsHistoryCategory> Catigories => _categories;

        public EarningDayHistory(int dayIndex, EarningsHistoryCategory earn)
        {
            _day = dayIndex;
            _categories.Add(earn);
        }

        public void AddOrUpdateCategory(string earnName, int earnMoney, int expensesMoney)
        {
            EarningsHistoryCategory category = GetCategoryByName(earnName);
            if (category == null)
            {
                _categories.Add(new(earnName, earnMoney, expensesMoney));
            }
            else
            {
                category.AddToEarnings(earnMoney);
                category.AddToExpenses(expensesMoney);
            }
        }

        public EarningsHistoryCategory GetCategoryByName(string name)
        {
            foreach (EarningsHistoryCategory category in _categories)
                if (category.CategoryName == name)
                    return category;
            return null;
        }
    }
}