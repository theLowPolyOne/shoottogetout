using STGO.Gameplay;
using STGO.UI;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [SerializeField] private PlayerSettings _playerSettings;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerSettings>().FromInstance(_playerSettings).AsSingle();
        Container.Bind<IGameStateManager>().To<GameStateManager>().AsSingle();
        Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        Container.Bind<IUIManager>().To<UIManager>().AsSingle();
    }
}
