using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace STGO.Gameplay
{
    public class ObstacleFactory : IObstacleFactory
    {
        [Inject] private IObstacleProvider _obstacleProvider;

        public AnimatedObstacle GetAnimatedObstacle()
        {
            var index = Random.Range(0, _obstacleProvider.AnimatedObstacles.Length);
            var obstacle = GameObject.Instantiate(_obstacleProvider.AnimatedObstacles[index]);

            var animations = new List<AnimationClip>
            {
                _obstacleProvider.StandingIdleAnimations[Random.Range(0, _obstacleProvider.StandingIdleAnimations.Length)],
                _obstacleProvider.StandingDestroyAnimations[Random.Range(0, _obstacleProvider.StandingDestroyAnimations.Length)]
            };
            RuntimeAnimatorProvider.SetRuntimeAnimatorController(obstacle.Animator, animations);

            return obstacle;
        }
    }
}