using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FifteenPuzzle
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = FindObjectOfType<AudioManager>();

            _closeButton.onClick.AddListener(() => StartCoroutine(ClosePanel()));

        }

        private IEnumerator ClosePanel()
        {
            yield return _audioManager.PlayButtonSoundClip();
            gameObject.SetActive(false);
        }
    }
}