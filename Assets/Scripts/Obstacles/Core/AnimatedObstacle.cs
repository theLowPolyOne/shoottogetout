using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace STGO.Gameplay
{
    public abstract class AnimatedObstacle : Obstacle
    {
        [Header("COMPONENTS:")]
        [SerializeField] protected Animator _animator;
        [SerializeField] protected bool _isRandomStartFrame = true;

        public Animator Animator => _animator;

        protected const string kDestroyState = "Destroy";
        protected const string kExplosionState = "ExplosionDestroy";

        private void Start()
        {
            if (_isRandomStartFrame) RandomizeStartFrame();
        }

        private void RandomizeStartFrame()
        {
            var state = _animator.GetCurrentAnimatorStateInfo(0);
            _animator.Play(state.fullPathHash, 0, Random.Range(0f, 1f));
        }

        public override void Destroy()
        {
            _animator.Play(kDestroyState);
            base.Destroy();
        }

        public override void Destroy(Vector3 explosionPoint)
        {
            RotateOnHit(explosionPoint);
            _animator.Play(kExplosionState);
            base.Destroy();
        }

        protected virtual void RotateOnHit(Vector3 explosionPoint)
        {
            var sequance = DOTween.Sequence();
            Vector3 direction = (explosionPoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(-direction, Vector3.up);
            sequance.Join(transform.DORotateQuaternion(targetRotation, 0.1f));
        }
    }
}