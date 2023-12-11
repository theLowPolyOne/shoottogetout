using STGO.Gameplay;
using UnityEngine;
using Zenject;

public class ObstacleInstaller : MonoInstaller
{
    [SerializeField] private ObstacleProvider _obstacleProvider;
    [SerializeField] private ObstacleManager _obstacleManager;

    public override void InstallBindings()
    {
        Container.Bind<IObstacleProvider>().FromInstance(_obstacleProvider).AsSingle();
        Container.Bind<IObstacleManager>().FromInstance(_obstacleManager).AsSingle();
        Container.Bind<IObstacleFactory>().To<ObstacleFactory>().AsSingle();
        Container.Bind<IObstacleManager>().To<ObstacleManager>().AsSingle();
    }
}