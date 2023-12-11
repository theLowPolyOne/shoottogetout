using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace STGO.Gameplay
{
    public class ObstacleManager : MonoBehaviour, IObstacleManager
    {
        [SerializeField] private Transform _obstaclesContainer;
        [SerializeField] private List<ObstaclePositionPreset> _positionPresets;

        [Inject] private IObstacleFactory _obstacleFactory;

        private void Start()
        {
            SetupObstacles();
        }

        public void SetupObstacles()
        {
            var preset = _positionPresets[Random.Range(0, _positionPresets.Count)];
            var positions = preset.Positions;

            for (int i = 0, j = positions.Count; i < j; i++)
            {
                var obstacle = _obstacleFactory.GetAnimatedObstacle();
                obstacle.transform.parent = _obstaclesContainer;
                obstacle.transform.position = positions[i];
            }
        }
    }
}