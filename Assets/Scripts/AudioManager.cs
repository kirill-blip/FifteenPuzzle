using UnityEngine;

namespace FifteenPuzzle
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _clickingClip = null;
        [SerializeField] private AudioClip _backgroundClip = null;
        [SerializeField] private AudioSource _firstAudioSource = null;
        [SerializeField] private AudioSource _secondAudioSource = null;

        private void Start()
        {
            Settings.CanPlayMusicChanged += CanPlayMusicChanged;
            Settings.CanPlaySoundChanged += CanPlaySoundChanged;

            CanPlayMusicChanged();

            if (_secondAudioSource != null)
            {
                _secondAudioSource.clip = _clickingClip;

                if (Settings.CanPlaySound)
                {
                    _secondAudioSource.volume = 1;
                }
                else
                {
                    _secondAudioSource.volume = 0;
                }
            }
        }

        private void CanPlayMusicChanged()
        {
            if (_firstAudioSource != null)
            {
                _firstAudioSource.clip = _backgroundClip;

                if (Settings.CanPlayMusic)
                {
                    _firstAudioSource.Play();
                }
                else
                {
                    _firstAudioSource.Stop();
                }
            }
        }

        private void CanPlaySoundChanged()
        {
            if (_secondAudioSource != null)
            {
                _secondAudioSource.clip = _clickingClip;

                if (Settings.CanPlaySound)
                {
                    _secondAudioSource.volume = 1;
                }
                else
                {
                    _secondAudioSource.volume = 0;
                }
            }
        }

        public void PlayClickingClip()
        {
            _secondAudioSource.Play();
        }
    }
}
