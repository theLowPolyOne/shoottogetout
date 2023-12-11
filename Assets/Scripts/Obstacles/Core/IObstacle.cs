using System;
using UnityEngine;

namespace STGO.Gameplay
{
    public interface IObstacle
    {
        Vector3 Position { get; }
        public event Action OnDestroy;
        public abstract void Destroy();
        public abstract void Destroy(Vector3 explosionPoint);
    }
}