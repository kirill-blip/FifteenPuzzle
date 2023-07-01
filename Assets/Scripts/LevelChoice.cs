using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace FifteenPuzzle
{
	public class LevelChoice : MonoBehaviour
	{
		[SerializeField] private Button _closeButton;

		[SerializeField] private Button _3x3LevelButton;
		[SerializeField] private Button _4x4LevelButton;
		[SerializeField] private Button _5x5LevelButton;

		private AudioManager _audioManager;

		private void Start()
		{
			_audioManager = FindObjectOfType<AudioManager>();

			_closeButton.onClick.AddListener(delegate { StartCoroutine(ClosePanel()); });

			_3x3LevelButton.onClick.AddListener(delegate { StartCoroutine(LoadLevel(3)); });
			_4x4LevelButton.onClick.AddListener(delegate { StartCoroutine(LoadLevel(4)); });
			_5x5LevelButton.onClick.AddListener(delegate { StartCoroutine(LoadLevel(5)); });
		}

		private IEnumerator ClosePanel()
		{
			yield return _audioManager.PlayButtonSoundClip();

			gameObject.SetActive(false);
		}

		private IEnumerator LoadLevel(int tileCount)
		{
			yield return _audioManager.PlayButtonSoundClip();

			SceneManager.LoadScene($"{tileCount}x{tileCount}");
		}
	}
}