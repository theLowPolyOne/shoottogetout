using System.Collections.Generic;
using UnityEngine;

namespace STGO.Gameplay
{
    public class ObstaclePositionPreset : MonoBehaviour
    {
        [SerializeField] private List<Transform> _obstaclePoints;
        public List<Vector3> Positions => GetAllPositions();

        private List<Vector3> GetAllPositions()
        {
            var positions = new List<Vector3>();
            foreach (Transform point in _obstaclePoints)
            {
                var position = point.transform.position;
                positions.Add(position);
            }
            return positions;
        }
    }
}