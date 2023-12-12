using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace STGO.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        [Header("COMPONENTS:")]
        [SerializeField] private Transform _transform;
        [SerializeField] private Collider _collider;

        [Header("VFX:")]
        [SerializeField] private Vector3 _vfxOffset = Vector3.zero;
        [SerializeField] private ParticleSystem[] _bigExplosions;
        [SerializeField] private ParticleSystem[] _smallExplosions;

        private Vector3 _defaultPosition;
        private bool _isReadyToLaunch = true;
        [SerializeField] private LayerMask _obstaclesLayers;

        private float _explosionRadius => Size.x * 10;

        public bool IsReadyToLaunch => _isReadyToLaunch;
        public Vector3 Position => _transform.position;
        public Vector3 Size => _collider.bounds.size;
        public event Action OnHit;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _defaultPosition = transform.position;
            _transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void SetVFX(ParticleSystem[] bigExplosions, ParticleSystem[] smallExplosions)
        {
            _bigExplosions = bigExplosions;
            _smallExplosions = smallExplosions;
        }

        public void PrepareToLaunch()
        {
            gameObject.SetActive(true);
        }

        public void SetScale(Vector3 scale)
        {
            _transform.DOScale(scale, Time.deltaTime);
        }

        public void LaunchTo(Vector3 endPosition, float duration, IObstacle obstacle)
        {
            _isReadyToLaunch = false;
            _transform.DOMove(endPosition, duration)
                .OnUpdate(() => CheckDistanceToObstacle(obstacle))
                .OnComplete(ResetToDefault);
        }

        public void LaunchTo(Vector3 endPosition, float duration)
        {
            _isReadyToLaunch = false;
            _transform.DOMove(endPosition, duration)
                .OnComplete(ResetToDefault);
            OnHit?.Invoke();
        }

        private void CheckDistanceToObstacle(IObstacle obstacle)
        {
            if (_transform.position.z <= obstacle.Position.z)
            {
                HandleHit(obstacle);
            }
        }

        private void HandleHit(IObstacle obstacle)
        {
            DOTween.Kill(_transform);
            HandleExplosion(obstacle.Position);
            ResetToDefault();
            OnHit?.Invoke();
        }

        private void HandleExplosion(Vector3 position)
        {
            Collider[] hits = Physics.OverlapSphere(position, _explosionRadius, _obstaclesLayers);
            var vfxPosition = position + _vfxOffset;
            PlayExplosionEffect(vfxPosition, _explosionRadius, hits.Length);

            if (hits.Length > 1)
            {
                foreach (Collider hit in hits)
                {
                    IObstacle obstacle = hit.GetComponent<IObstacle>();
                    obstacle?.Destroy(position);
                }
            }
            else
            {
                IObstacle obstacle = hits[0].GetComponent<IObstacle>();
                obstacle?.Destroy();
            }
        }

        private void PlayExplosionEffect(Vector3 position, float size, int targetAmount)
        {
            ParticleSystem vfx = null;
            Vector3 vfxScale;

            if (targetAmount > 1)
            {
                vfx = _bigExplosions[Random.Range(0, _bigExplosions.Length)];
                vfxScale = Vector3.one * size * 0.25f;
            }
            else
            {
                vfx = _smallExplosions[Random.Range(0, _smallExplosions.Length)];
                vfxScale = Vector3.one;
            }

            if (vfx != null)
            {
                vfx.gameObject.SetActive(true);
                vfx.transform.position = position;
                vfx.transform.localScale = vfxScale;
                vfx.Play();
            }
        }

        private void ResetToDefault()
        {
            gameObject.SetActive(false);
            _transform.localScale = Vector3.zero;
            _transform.position = _defaultPosition;
            _isReadyToLaunch = true;
        }
    }
}