using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

		[SerializeField] private LevelChoice _levelChoice = null;

		private AudioManager _audioManager = null;

		private void Start()
		{
			_audioManager = FindObjectOfType<AudioManager>();

			_startGameButton.onClick.AddListener(() => StartCoroutine(StartGameButtonClicked()));
			_settingsGameButton.onClick.AddListener(() => StartCoroutine(SettingsGameButtonClicked()));
			_exitGameButton.onClick.AddListener(() => StartCoroutine(ExitGameButtonClicked()));
		}

		private IEnumerator StartGameButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();

			_levelChoice.gameObject.SetActive(true);

			//SceneManager.LoadScene("4x4");
		}

		private IEnumerator SettingsGameButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();

			_settingsPanel.gameObject.SetActive(true);
		}

		private IEnumerator ExitGameButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();

#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
		}
	}
}
