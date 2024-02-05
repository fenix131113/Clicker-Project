namespace Clicker.Core.Earnings
{
    [System.Serializable]
    public class EarningsHistoryCategory
    {

        private readonly string _categoryName;
        public string CategoryName => _categoryName;


        private int _earningMoney;
        public int EarningMoney => _earningMoney;


        private int _expensesMoney;
        public int ExpensesMoney => _expensesMoney;


        public EarningsHistoryCategory(string earnName, int earnMoney, int expensesMoney)
        {
            _categoryName = earnName;
            _earningMoney = earnMoney;
            _expensesMoney = expensesMoney;
        }

        public void AddToEarnings(int count) { _earningMoney += count; }

        public void AddToExpenses(int count) { _expensesMoney += count; }
    }
}