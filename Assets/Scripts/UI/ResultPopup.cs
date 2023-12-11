using DG.Tweening;
using STGO.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace STGO.UI
{
    public class ResultPopup : MonoBehaviour
    {
        [Header("COMPONENTS:")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _titleLabel;
        [SerializeField] private CanvasGroup _background;

        [Header("OPTIONS:")]
        [SerializeField] private float _showDuration = 0.5f;
        [SerializeField] private Color _victoryColor;
        [SerializeField] private Color _loseColor;

        private const string kVictoryText = "You Win";
        private const string kLoseText = "You Lose";

        [Inject] private IGameStateManager _gameStateManager;
        [Inject] private IUIManager _uiManager;

        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            _uiManager.OnResultShow += Show;
            _restartButton.onClick.AddListener(OnRestartButtonPressed);
        }

        private void OnDisable()
        {
            _uiManager.OnResultShow -= Show;
            _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
        }

        private void Init()
        {
            _restartButton.interactable = false;
            _restartButton.transform.localScale = Vector3.zero;
            _titleLabel.transform.localScale = Vector3.zero;
            _background.alpha = 0f;
        }

        public void Show(bool isVictory)
        {
            var text = isVictory ? kVictoryText : kLoseText;
            var color = isVictory ? _victoryColor : _loseColor;
            _titleLabel.text = text;
            _titleLabel.color = color;

            var sequance = DOTween.Sequence();
            sequance.Join(_background.DOFade(1, _showDuration));
            sequance.Join(_titleLabel.transform.DOScale(Vector3.one, _showDuration).SetEase(Ease.OutBack));
            sequance.Join(_restartButton.transform.DOScale(Vector3.one, _showDuration * 0.75f).SetEase(Ease.OutBack));
            sequance.OnComplete(() => _restartButton.interactable = true);
        }

        private void OnRestartButtonPressed()
        {
            _gameStateManager.UpdateState(GameState.Gameplay);
        }
    }
}