using UnityEngine;
using Zenject;

public class ClockInstaller : MonoInstaller
{
    [SerializeField] private Clock _clock;

    public override void InstallBindings()
    {
        Container.Bind<Clock>().FromInstance(_clock);
    }
}
