using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FifteenPuzzle
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton = null;
        [SerializeField] private Button _settingsGameButton = null;
        [SerializeField] private Button _exitGameButton = null;
        [SerializeField] private SettingsPanel _settingsPanel = null;

        private void Start()
        {
            _startGameButton.onClick.AddListener(StartGameButtonClicked);
            _settingsGameButton.onClick.AddListener(SettingsGameButtonClicked);
            _exitGameButton.onClick.AddListener(ExitGameButtonClicked);
        }

        private void StartGameButtonClicked()
        {
            SceneManager.LoadScene("Game");
        }

        private void SettingsGameButtonClicked()
        {
            _settingsPanel.gameObject.SetActive(true)    ;
        }

        private void ExitGameButtonClicked()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
