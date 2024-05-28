using Clicker.Core.SkillSystem;

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
                data.SetClickPower(2);
                break;
            case 2:
                data.SetClickPower(5);
                break;
            case 3:
                data.SetClickPower(10);
                break;
            case 4:
                data.SetClickPower(20);
                break;
            case 5:
                data.SetClickPower(40);
                break;
        }
    }
}