using Zenject;

public class DependensyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainInput>().FromNew().AsSingle().NonLazy();
        Container.Bind<TimePicker>().FromInstance(TimePicker.GetInstance()).AsSingle().NonLazy();
    }
}
