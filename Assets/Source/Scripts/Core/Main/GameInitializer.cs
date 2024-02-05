using Clicker.Core.Earnings;
using Clicker.Core.Time;
using Clicker.Core.Workers;
using Zenject;
using UnityEngine;

public class GameInitializer : MonoInstaller
{
    [SerializeField] private GameObject generalContainer;
    public override void InstallBindings()
    {
        Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
        Container.Bind<WorkersManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<EarningsManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<GeneralPassiveMoneyController>().FromNew().AsSingle().NonLazy();
        Container.Bind<TimeManager>().FromComponentOn(generalContainer).AsSingle();
    }
}
