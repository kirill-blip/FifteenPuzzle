using UnityEngine;

namespace FifteenPuzzle
{
    public class Music : MonoBehaviour
    {
        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }

        public void StopAllMusic()
        {
            _audioManager.StopMusic();
        }

        public void StartAllMusic()
        {
            _audioManager.StartMusic();
        }
    }
}