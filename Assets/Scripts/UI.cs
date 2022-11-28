using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FifteenPuzzle
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Button _returnToMenuButton = null;
        [SerializeField] private Button _restartGameButton = null;
        [SerializeField] private Button _shuffleButton = null;
        [SerializeField] private Button _returnButton = null;

        [SerializeField] private GameObject _panel = null;

        public System.Action RestartGameClickedAction;
        public System.Action ShuffleButtonClickedAction;

        private Game _game = null;

        private void Start()
        {
            _game = FindObjectOfType<Game>();
            _game.GameWon += GameWon;
            _game.GameRestarted += ActivateObjects;

            _returnToMenuButton.onClick.AddListener(ReturnToMenuButtonClicked);
            _restartGameButton.onClick.AddListener(RestartGameButtonClicked);
            _returnButton.onClick.AddListener(ReturnToMenuButtonClicked);
            _shuffleButton.onClick.AddListener(ShuffleButtonClicked);

            ActivateObjects();
        }

        private void ShuffleButtonClicked()
        {
            ShuffleButtonClickedAction?.Invoke();
        }

        private void GameWon()
        {
            ActivateObjects();
        }

        private void ActivateObjects()
        {
            _panel.gameObject.SetActive(!_panel.gameObject.activeInHierarchy);
            _shuffleButton.gameObject.SetActive(!_shuffleButton.gameObject.activeInHierarchy);
            _returnButton.gameObject.SetActive(!_returnButton.gameObject.activeInHierarchy);
        }

        private void ReturnToMenuButtonClicked()
        {
            SceneManager.LoadScene("Menu");
        }

        private void RestartGameButtonClicked()
        {
            RestartGameClickedAction?.Invoke();
        }
    }
}