using DG.Tweening;
using STGO.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace STGO.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("COMPONENTS:")]
        [SerializeField] private Button _playButton;

        [Inject] private IGameStateManager _gameStateManager;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _playButton.onClick.AddListener(OnPlayButtonPressed);
        }

        private void OnPlayButtonPressed()
        {
            _gameStateManager.UpdateState(GameState.Gameplay);
        }
    }
}