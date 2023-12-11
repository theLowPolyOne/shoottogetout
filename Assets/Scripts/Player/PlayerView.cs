using DG.Tweening;
using UnityEngine;

namespace STGO.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [Header("COMPONENTS:")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _pathTransform;

        private Vector3 _startPlayerScale;
        private float _startPathScaleX;
        private Vector3 _chargingScale;

        private float _jumpPower = 0.5f;
        private int _jumpNumber = 10;

        public Vector3 ChargingScale => _chargingScale;

        public float PathSize => _pathTransform.localScale.x;

        private void Start()
        {
            _startPlayerScale = _playerTransform.localScale;
            _startPathScaleX = _pathTransform.localScale.x;
        }

        public void SetScale(float scaleModifier)
        {
            _chargingScale = _startPlayerScale - (_startPlayerScale * scaleModifier);
            _playerTransform.DOScale(_chargingScale, Time.deltaTime);
            _pathTransform.DOScaleX(_startPathScaleX - (_startPathScaleX * scaleModifier), Time.deltaTime);
        }

        public void MoveTo(Vector3 endPosition, float duration)
        {
            _chargingScale = Vector3.zero;
            var sequence = DOTween.Sequence();
            sequence.Join(_playerTransform.DOJump(endPosition, _jumpPower, _jumpNumber, duration));
        }

        public void Lose()
        {
            var sequence = DOTween.Sequence();
            sequence.Join(_playerTransform.DOScale(_playerTransform.localScale * 1.1f, 0.3f));
            sequence.Append(_playerTransform.DOScale(Vector3.zero, 0.2f));
        }
    }
}