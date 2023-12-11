using UnityEngine;

namespace STGO.Gameplay
{
    [CreateAssetMenu(menuName = "STGO/Providers/Obstacle Provider")]
    public class ObstacleProvider : ScriptableObject, IObstacleProvider
    {
        [SerializeField] private Obstacle[] _obstacles;
        [SerializeField] private AnimatedObstacle[] _animatedObstacles;
        [SerializeField] private AnimationClip[] _standingIdleAnimations;
        [SerializeField] private AnimationClip[] _standingDestroyAnimations;
        [SerializeField] private AnimationClip[] _sittingIdleAnimations;
        [SerializeField] private AnimationClip[] _sittingDestroyAnimations;

        public Obstacle[] Obstacles => _obstacles;
        public AnimatedObstacle[] AnimatedObstacles => _animatedObstacles;
        public AnimationClip[] StandingIdleAnimations => _standingIdleAnimations;
        public AnimationClip[] StandingDestroyAnimations => _standingDestroyAnimations;
        public AnimationClip[] SittingIdleAnimations => _sittingIdleAnimations;
        public AnimationClip[] SittingDestroyAnimations => _sittingDestroyAnimations;
    }
}