using STGO.Gameplay;
using UnityEngine;
using Zenject;

namespace STGO.Core
{
    public class EnterPoint : MonoBehaviour
    {
        [SerializeField] GameState _startGameState = GameState.MainMenu;
        [Inject] IGameStateManager _gameStateManager;

        private void Start()
        {
            _gameStateManager.UpdateState(_startGameState);
        }
    }
}