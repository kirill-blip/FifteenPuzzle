using System.Collections;
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
		private AudioManager _audioManager = null;

		private void Start()
		{
			_game = FindObjectOfType<Game>();
			_game.GameWon += GameWon;
			_game.GameRestarted += ActivateObjects;

			_audioManager = FindObjectOfType<AudioManager>();

			_returnToMenuButton.onClick.AddListener(() => StartCoroutine(ReturnToMenuButtonClicked()));
			_restartGameButton.onClick.AddListener(() => StartCoroutine(RestartGameButtonClicked()));
			_returnButton.onClick.AddListener(() => StartCoroutine(ReturnToMenuButtonClicked()));
			_shuffleButton.onClick.AddListener(() => StartCoroutine(ShuffleButtonClicked()));

			ActivateObjects();
		}

		private void GameWon()
		{
			_shuffleButton.gameObject.SetActive(false);
			_returnButton.gameObject.SetActive(false);

			ActivateObjects();
		}

		private void ActivateButtons()
		{
			_shuffleButton.gameObject.SetActive(true);
			_returnButton.gameObject.SetActive(true);
		}

		private void ActivateObjects()
		{
			_panel.gameObject.SetActive(!_panel.gameObject.activeInHierarchy);
		}

		private IEnumerator ShuffleButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();
			ShuffleButtonClickedAction?.Invoke();
		}

		private IEnumerator ReturnToMenuButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();
			SceneManager.LoadScene(0);
		}

		private IEnumerator RestartGameButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();
			ActivateButtons();
			RestartGameClickedAction?.Invoke();
		}
	}
}