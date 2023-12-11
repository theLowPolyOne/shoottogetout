using UnityEngine;
using Zenject;

namespace STGO.Gameplay
{
    public class PlayerFactory : IPlayerFactory
    {
        [Inject] private IPlayerSettings _playerSettings;

        public Player GetPlayer()
        {
            var player = GameObject.Instantiate(_playerSettings.PlayerPrefab, _playerSettings.StartPosition, Quaternion.identity);
            player.Setup(_playerSettings.ChargingSpeed, 
                _playerSettings.ChargeMax,
                _playerSettings.ShootDuration,
                _playerSettings.WinningAnimationDuration,
                _playerSettings.ObstaclesLayers);
            return player;
        }
    }
}