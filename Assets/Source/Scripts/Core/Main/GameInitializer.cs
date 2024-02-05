using Zenject;

public class GameInitializer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
    }
}
