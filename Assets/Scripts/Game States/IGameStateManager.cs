using System;

namespace STGO.Gameplay
{
    public interface IGameStateManager
    {
        public GameState CurrentState { get; }
        public event Action<GameState> OnGameStateChanged;
        public void UpdateState(GameState newState);
    }
}