using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using TMPro;

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
        [SerializeField] private Button _resultButton = null;
        [SerializeField] private Button _shareButton = null;
        [SerializeField] private SettingsPanel _settingsPanel = null;
        [SerializeField] private ResultPanel _resultPanel = null;

        [SerializeField] private LevelChoice _levelChoice = null;

        private AudioManager _audioManager = null;

        [DllImport("__Internal")]
        private static extern void ShareGame();

        [DllImport("__Internal")]
        private static extern void CheckNativeAdsInerstitial();

        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();

            _startGameButton.onClick.AddListener(() => StartCoroutine(StartGameButtonClicked()));
            _settingsGameButton.onClick.AddListener(() => StartCoroutine(SettingsGameButtonClicked()));
            _shareButton.onClick.AddListener(() => StartCoroutine(ShareGameCoroutine()));
            _resultButton.onClick.AddListener(() => StartCoroutine(ShowResultPanel()));
            _exitGameButton.onClick.AddListener(() => StartCoroutine(ExitGameButtonClicked()));

            InvokeRepeating(nameof(CheckNativeAdsInerstitial), 0.5f, 0.25f);
        }

        private IEnumerator ShowResultPanel()
        {
            yield return _audioManager.PlayButtonSoundClip();

            _resultPanel.gameObject.SetActive(true);
        }

        private IEnumerator ShareGameCoroutine()
        {
            yield return _audioManager.PlayButtonSoundClip();

            ShareGame();
        }

        private IEnumerator StartGameButtonClicked()
        {
            yield return _audioManager.PlayButtonSoundClip();

            _levelChoice.gameObject.SetActive(true);
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
