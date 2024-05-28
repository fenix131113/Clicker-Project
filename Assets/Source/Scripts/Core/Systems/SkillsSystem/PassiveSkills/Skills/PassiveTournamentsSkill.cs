namespace Clicker.Core.SkillSystem
{
    public class PassiveTournamentsSkill : PassiveSkillBase
    {
        public override void StartControllerCall()
        {
            _tournamentManager.onTournamentWin += CheckConditions;
        }

        public override int GetCurrentCounter()
        {
            return data.TournamentWins;
        }

        public override void BuyAction()
        {
            switch (level)
            {
                case 1:
                    data.UnlockFood(1);
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    break;
                case 2:
                    data.UnlockFood(2);
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    break;
                case 3:
                    data.UnlockFood(3);
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    break;
                case 4:
                    data.UnlockFood(4);
                    data.WorkersManager.SetWorkerFoodPerDay(data.WorkersManager.WorkerFoodPerDay + 5);
                    break;
            }
        }
    }
}