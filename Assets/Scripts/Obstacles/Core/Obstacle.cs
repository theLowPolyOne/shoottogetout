using System;
using UnityEngine;

namespace STGO.Gameplay
{
    public abstract class Obstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] protected Collider _collider;

        public Vector3 Position => transform.position;

        public event Action OnDestroy;

        public virtual void Destroy()
        {
            _collider.enabled = false;
            OnDestroy?.Invoke();
        }

        public virtual void Destroy(Vector3 explosionPoint)
        {
            Destroy();
        }
    }
}