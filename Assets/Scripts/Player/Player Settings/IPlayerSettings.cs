using UnityEngine;

namespace STGO.Gameplay
{
    public interface IPlayerSettings
    {
        public float ChargingSpeed { get; }
        public float ChargeMax { get; }
        public float ShootDuration { get; }
        public LayerMask ObstaclesLayers { get; }
        public float WinningAnimationDuration { get; }
    }
}