
namespace Clicker.Core.SkillSystem
{
    public class PassiveMafiaPaymentsSkill : PassiveSkillBase
    {
        public override void StartControllerCall()
        {
            _mafiaManager.onMafiaPayComplete += CheckConditions;
        }

        public override int GetCurrentCounter()
        {
            return data.MafiaPayments;
        }

        public override void BuyAction()
        {
            switch (level)
            {
                // Default = 100
                case 1:
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker - 5);
                    break;
                case 2:
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker - 5);
                    break;
                case 3:
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker - 5);
                    break;
                case 4:
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker - 5);
                    break;
                case 5:
                    data.WorkersManager.SetSalaryPerWorker(data.WorkersManager.SalayPerWorker - 5);
                    break;
            }
        }
    }
}