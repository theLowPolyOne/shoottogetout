using STGO.Input;
using System;
using UnityEngine;
using Zenject;

namespace STGO.Gameplay
{
    public class Player : MonoBehaviour
    {
        [Header("COMPONENTS:")]
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Projectile _projectile;

        [Header("CHARGING:")]
        [SerializeField] private float _chargingSpeed = 0.1f;
        [SerializeField] private float _chargeMax = 0.8f;

        [Header("SHOOT:")]
        [SerializeField] private float _shootDuration = 1f;
        [SerializeField] private LayerMask _obstaclesLayers;

        [Header("WINNING:")]
        [SerializeField] private Transform _winningDoor;
        [SerializeField] private Animator _winningDoorAnimator;
        [SerializeField] private float _winningTweenDuration = 2f;

        [Inject] private IGameStateManager _gameStateManager;

        private Vector3 _winningEndPosition => _winningDoor.position;

        private PlayerInputHandler _inputHandler;
        private InputData _input;
        private bool _isInputEnabled = true;
        private bool _isShootRequested;
        private bool _isChargingAvailable => _chargePower <= _chargeMax;
        private bool _isChargingEnabled =>
            _isChargingAvailable && _projectile.IsReadyToLaunch;
        private float _chargePower = 0f;
        private Vector3 _startChargingScale;

        public event Action OnShoot;
        public event Action OnCharging;
        public event Action OnMaxChargingReached;

        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            _inputHandler?.Enable();
        }

        private void OnDisable()
        {
            _inputHandler?.Disable();
        }

        private void Init()
        {
            _inputHandler = new PlayerInputHandler();

            OnShoot += Shoot;
            OnCharging += Charge;
            OnMaxChargingReached += HandleDefeat;
            _projectile.OnHit += CheckPath;
        }

        private void CheckPath()
        {
            if (!IsObstacleOnPath(false, out IObstacle obstacle)) HandleWin();
        }

        protected virtual void Update()
        {
            if (_isChargingEnabled)
            {
                GetInput();
                HandleShoot();
            }
        }

        private void GetInput()
        {
            if (_isInputEnabled) _input = _inputHandler.InputData;

            if (_input.IsShootDown) HandleShootRequest();
            if (_input.IsShootHeld) OnCharging?.Invoke();
        }

        private void HandleShootRequest()
        {
            _isShootRequested = true;
            _startChargingScale = _playerView.transform.localScale;
            _projectile.PrepareToLaunch();
        }

        private void HandleShoot()
        {
            if (_isShootRequested)
            {
                if (!_input.IsShootHeld)
                {
                    OnShoot?.Invoke();
                }
            }
        }

        private void HandleWin()
        {
            _playerView.MoveTo(_winningEndPosition, _winningTweenDuration);
            _winningDoorAnimator.Play("Open");
            _gameStateManager.UpdateState(GameState.Victory);
        }

        private void HandleDefeat()
        {
            _isInputEnabled = false;
            _playerView.Lose();
            _gameStateManager.UpdateState(GameState.Loss);
        }

        private void Shoot()
        {
            _isShootRequested = false;

            if(IsObstacleOnPath(true, out IObstacle obstacle))
            {
                _projectile.LaunchTo(_winningEndPosition, _shootDuration, obstacle);
            }
            else
            {
                _projectile.LaunchTo(_winningEndPosition, _shootDuration);
            }
        }

        private void ResetLastCharge()
        {
            throw new NotImplementedException();
        }

        private void Charge()
        {
            float scaleSpeed = _chargingSpeed * Time.deltaTime;
            _chargePower += scaleSpeed;
            if (_isChargingAvailable) HandleCharging();
            else OnMaxChargingReached?.Invoke();
        }

        private void HandleCharging()
        {
            _playerView.SetScale(_chargePower);
            _projectile.SetScale(_startChargingScale - _playerView.ChargingScale);
        }

        private bool IsObstacleOnPath(bool isShootCheck, out IObstacle obstacle)
        {
            Vector3 size = isShootCheck ? _projectile.Size : Vector3.one * _playerView.PathSize;
            Vector3 origin = isShootCheck ? _projectile.Position : _playerView.transform.position;
            float distance = Vector3.Distance(origin, _winningEndPosition);

            if (Physics.BoxCast(origin, size, -transform.forward, out RaycastHit hit, Quaternion.identity, distance, _obstaclesLayers))
            {
                obstacle = hit.collider.GetComponent<IObstacle>();
                return true;
            }
            else
            {
                obstacle = null;
                return false;
            }
        }
    }
}