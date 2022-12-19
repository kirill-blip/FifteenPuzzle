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
        [SerializeField] private Button _informationButton = null;
        
        [SerializeField] private GameObject _panel = null;
        [SerializeField] private GameObject _informationPanel = null;

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
            _informationButton.onClick.AddListener(InfromationButtonClicked);

            ActivateObjects();
        }

        private void InfromationButtonClicked()
        {
            print("It's works");
            _informationPanel.gameObject.SetActive(true);
        }

        private void ShuffleButtonClicked()
        {
            ShuffleButtonClickedAction?.Invoke();
        }

        private void GameWon()
        {
            _shuffleButton.gameObject.SetActive(false);
            _returnButton.gameObject.SetActive(false);
            _informationButton.gameObject.SetActive(false);

            ActivateObjects();
        }

        private void ActivateButtons()
        {
            _shuffleButton.gameObject.SetActive(true);
            _returnButton.gameObject.SetActive(true);
            _informationButton.gameObject.SetActive(true);
        }

        private void ActivateObjects()
        {
            _panel.gameObject.SetActive(!_panel.gameObject.activeInHierarchy);
        }

        private void ReturnToMenuButtonClicked()
        {
            SceneManager.LoadScene("Menu");
        }

        private void RestartGameButtonClicked()
        {
            ActivateButtons();
            RestartGameClickedAction?.Invoke();
        }
    }
}