using System;
using UnityEngine.SceneManagement;
using Zenject;
using STGO.UI;

namespace STGO.Gameplay
{
    public class GameStateManager : IGameStateManager
    {
        private const string kMainMenuScene = "MainMenu";
        private const string kGameplayScene = "Gameplay";
        private const string kGUIScene = "GUI";

        private GameState _currentState;
        public GameState CurrentState => _currentState;

        public event Action<GameState> OnGameStateChanged;

        [Inject] private IUIManager _uiManager;

        public void UpdateState(GameState newState)
        {
            _currentState = newState;

            switch (newState)
            {
                case GameState.MainMenu:
                    HandleMainMenuState();
                    break;
                case GameState.Gameplay:
                    HandleGameplayState();
                    break;
                case GameState.Victory:
                    HandleVictoryState();
                    break;
                case GameState.Loss:
                    HandleLossState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnGameStateChanged?.Invoke(_currentState);
        }

        private void HandleMainMenuState()
        {
            SceneManager.LoadScene(kMainMenuScene, LoadSceneMode.Single);
        }

        private void HandleGameplayState()
        {
            SceneManager.LoadScene(kGameplayScene, LoadSceneMode.Single);
            SceneManager.LoadScene(kGUIScene, LoadSceneMode.Additive);
        }

        private void HandleVictoryState()
        {
            _uiManager.ShowResult(true);
        }

        private void HandleLossState()
        {
            _uiManager.ShowResult(false);
        }
    }
    public enum GameState
    {
        MainMenu,
        Gameplay,
        Victory,
        Loss,
    }
}