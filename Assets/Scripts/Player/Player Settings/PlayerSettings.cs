using UnityEngine;

namespace STGO.Gameplay
{
    [CreateAssetMenu(menuName = "STGO/Game Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject, IPlayerSettings
    {
        [Header("CHARGING:")]

        [Tooltip("The speed at which the projectile increases and the player decreases while charging a shot")]
        [SerializeField, Range(0, 10)] private float _chargingSpeed = 0.1f;
        public float ChargingSpeed => _chargingSpeed;

        [Tooltip("The maximum percentage of the player's scale that can be used to charge a projectile.")]
        [SerializeField, Range(0, 1)] private float _chargeMax = 0.8f;
        public float ChargeMax => _chargeMax;

        [Header("SHOOT:")]

        [Tooltip("The time in seconds it takes for a projectile to fly across the entire game area.")]
        [SerializeField] private float _shootDuration = 2f;
        public float ShootDuration => _shootDuration;

        [Tooltip("Layers containing game objects that can be destroyed by a projectile.")]
        [SerializeField] private LayerMask _obstaclesLayers;
        public LayerMask ObstaclesLayers => _obstaclesLayers;

        [Header("WINNING:")]

        [Tooltip("Time in seconds that the victory animation takes when the player moves towards the door.")]
        [SerializeField] private float _winningAnimationDuration = 2f;
        public float WinningAnimationDuration => _winningAnimationDuration;
    }
}