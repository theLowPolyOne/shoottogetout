using UnityEngine;

namespace STGO.Gameplay
{
    public interface IObstacleProvider
    {
        public Obstacle[] Obstacles { get; }
        public AnimatedObstacle[] AnimatedObstacles { get; }
        public AnimationClip[] StandingIdleAnimations { get; }
        public AnimationClip[] StandingDestroyAnimations { get; }
        public AnimationClip[] SittingIdleAnimations { get; }
        public AnimationClip[] SittingDestroyAnimations { get; }
    }
}