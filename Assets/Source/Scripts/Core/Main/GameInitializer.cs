using Clicker.Core.Earnings;
using Clicker.Core.Time;
using Clicker.Core.Workers;
using Clicker.Core.Tournament;
using Zenject;
using UnityEngine;

public class GameInitializer : MonoInstaller
{
    [SerializeField] private GameObject generalContainer;

    public override void InstallBindings()
    {
        Container.Bind<GlobalObjectsContainer>().FromComponentOn(generalContainer).AsSingle();
        Container.Bind<AudioController>().FromComponentOn(generalContainer).AsSingle();
        Container.Bind<SkillSaveManager>().FromComponentOn(generalContainer).AsSingle();
        Container.Bind<MafiaManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<RobberyManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<TournamentManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<WorkersManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<NoticeSystem>().FromNew().AsSingle().NonLazy();
        Container.Bind<GeneralPassiveMoneyController>().FromNew().AsSingle().NonLazy();
        Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
        Container.Bind<CalendarManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<TimeManager>().FromComponentOn(generalContainer).AsSingle();
        Container.Bind<WeeklyQuestsController>().FromComponentOn(generalContainer).AsSingle();
        Container.Bind<EarningsManager>().FromNew().AsSingle().NonLazy();
    }
}
