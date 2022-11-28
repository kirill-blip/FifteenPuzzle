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
            if (_firstAudioSource != null)
                _firstAudioSource.clip = _backgroundClip;

            if (_secondAudioSource != null)
                _secondAudioSource.clip = _clickingClip;
        }

        public void PlayClickingClip()
        {
            _secondAudioSource.Play();
        }
    }
}
