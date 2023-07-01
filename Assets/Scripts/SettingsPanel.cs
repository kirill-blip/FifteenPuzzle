using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FifteenPuzzle
{
	public class SettingsPanel : MonoBehaviour
	{
		[SerializeField] private Button _soundButton = null;
		[SerializeField] private Button _musicButton = null;
		[SerializeField] private Button _languageButton = null;
		[SerializeField] private Button _closeButton = null;

		[SerializeField] private List<Sprite> _soundSprites = null;
		[SerializeField] private List<Sprite> _musicSprites = null;

		private AudioManager _audioManager = null;

		public System.Action SoundButtonClickedAction;
		public System.Action MusicButtonClickedAction;
		public System.Action LanguageButtonClickedAction;

		private void Start()
		{
			_audioManager = FindObjectOfType<AudioManager>();

			if (!Settings.CanPlayMusic)
			{
				ChangeSprites(_musicButton.GetComponent<Image>(), _musicSprites);
			}

			if (!Settings.CanPlaySound)
			{
				ChangeSprites(_soundButton.GetComponent<Image>(), _soundSprites);
			}

			_soundButton.onClick.AddListener(() => StartCoroutine(SoundButtonClicked()));
			_musicButton.onClick.AddListener(() => StartCoroutine(MusicButtonClicked()));
			_closeButton.onClick.AddListener(() => StartCoroutine(CloseButtonClicked()));
			_languageButton.onClick.AddListener(() => StartCoroutine(LanguageButtonClicked()));
		}

		private IEnumerator LanguageButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();
			LanguageButtonClickedAction?.Invoke();
		}

		private IEnumerator SoundButtonClicked()
		{
			ChangeSprites(_soundButton.GetComponent<Image>(), _soundSprites);
			SoundButtonClickedAction?.Invoke();

			yield return _audioManager.PlayButtonSoundClip();
		}

		private IEnumerator MusicButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();

			ChangeSprites(_musicButton.GetComponent<Image>(), _musicSprites);
			MusicButtonClickedAction?.Invoke();
		}

		private void ChangeSprites(Image image, List<Sprite> sprites)
		{
			image.sprite = image.sprite == sprites[0] ? sprites[1] : sprites[0];
		}

		private IEnumerator CloseButtonClicked()
		{
			yield return _audioManager.PlayButtonSoundClip();
			gameObject.SetActive(false);
		}
	}
}
