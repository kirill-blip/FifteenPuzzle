using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FifteenPuzzle
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource _musicAudioSource = null;
		[SerializeField] private AudioSource _soundAudioSource = null;

		[SerializeField] private List<AudioClip> _backgroundClips = null;
		[SerializeField] private AudioClip _movingClip;
		[SerializeField] private AudioClip _buttonSound;

		[SerializeField] private float _musicVolume = 1f;
		[SerializeField] private bool _canPlayBackgroundMusic = true;

		private bool _isWorking = false;

		private void Start()
		{
			Settings.CanPlayMusicChanged += CanPlayMusicChanged;
			Settings.CanPlaySoundChanged += CanPlaySoundChanged;

			CanPlayMusicChanged();
			CanPlaySoundChanged();
		}

		private void Update()
		{
			if (_canPlayBackgroundMusic && !_musicAudioSource.isPlaying && !_isWorking)
			{
				StartCoroutine(PlayBackgroundMusic());
			}
		}

		private IEnumerator PlayBackgroundMusic()
		{
			_isWorking = true;

			yield return new WaitForSeconds(Random.Range(1.5f, 5f));

			_musicAudioSource.clip = _backgroundClips[Random.Range(0, _backgroundClips.Count)];
			_musicAudioSource.Play();

			_isWorking = false;
		}

		private void CanPlayMusicChanged()
		{
			if (_musicAudioSource != null)
			{
				if (!Settings.CanPlayMusic)
				{
					_musicAudioSource.volume = 0;
				}
				else
				{
					_musicAudioSource.volume = _musicVolume;
				}
			}
		}

		private void CanPlaySoundChanged()
		{
			if (_soundAudioSource != null)
			{
				if (Settings.CanPlaySound)
				{
					_soundAudioSource.volume = 1;
				}
				else
				{
					_soundAudioSource.volume = 0;
				}
			}
		}

		public void PlayClickingClip()
		{
			_soundAudioSource.PlayOneShot(_movingClip);
		}

		public IEnumerator PlayButtonSoundClip()
		{
			_soundAudioSource.PlayOneShot(_buttonSound);
			yield return new WaitForSeconds(_buttonSound.length);
		}
	}
}
