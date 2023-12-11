using DG.Tweening;
using UnityEngine;

namespace STGO.Gameplay
{
    public class SkeletonObstacle : AnimatedObstacle
    {
        [Header("DESTROY OPTIONS:")]
        [SerializeField] protected float _removingDuration = 4f;
        [SerializeField] protected float _removingDepth = -1f;

        public void HandleOnDestroyed()
        {
            var sequance = DOTween.Sequence();
            sequance.Join(transform.DOScaleY(0, _removingDuration / 2));
            sequance.Join(transform.DOLocalMoveY(_removingDepth, _removingDuration));
            sequance.OnComplete(() => gameObject.SetActive(false));
        }
    }
}