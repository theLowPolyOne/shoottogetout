using UnityEngine;
using Zenject;

namespace STGO.Gameplay
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform _winningDoor;
        [SerializeField] private Animator _winningDoorAnimator;
        [SerializeField] private ParticleSystem[] _bigExplosions;
        [SerializeField] private ParticleSystem[] _smallExplosions;

        private Player _player;
        public Player Player => _player;

        [Inject] private IPlayerFactory _playerFactory;
        [Inject] private IGameStateManager _gameStateManager;

        private void Start()
        {
            SpawnPlayer();
            _player.OnWin += HandleWin;
            _player.OnLose += HandleLose;
        }

        public void SpawnPlayer()
        {
            _player = _playerFactory.GetPlayer();
            _player.SetWinningDoor(_winningDoor, _winningDoorAnimator);
            _player.SetProjectile(_bigExplosions, _smallExplosions);
        }

        private void HandleWin()
        {
            _gameStateManager.UpdateState(GameState.Victory);
        }

        private void HandleLose()
        {
            _gameStateManager.UpdateState(GameState.Loss);
        }
    }
}