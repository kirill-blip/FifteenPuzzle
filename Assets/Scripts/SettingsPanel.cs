using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FifteenPuzzle
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Button _soundButton = null;
        [SerializeField] private Button _musicButton = null;
        [SerializeField] private Button _closeButton = null;


        [SerializeField] private List<Sprite> _soundSprites = null;
        [SerializeField] private List<Sprite> _musicSprites = null;

        public System.Action SoundButtonClickedAction;
        public System.Action MusicButtonClickedAction;

        private void Start()
        {
            if (!Settings.CanPlayMusic)
            {
                ChangeSprites(_musicButton.GetComponent<Image>(), _musicSprites);
            }

            if (!Settings.CanPlaySound)
            {
                ChangeSprites(_soundButton.GetComponent<Image>(), _soundSprites);
            }

            _soundButton.onClick.AddListener(SoundButtonClicked);
            _musicButton.onClick.AddListener(MusicButtonClicked);
            _closeButton.onClick.AddListener(CloseButtonClicked);
        }

        private void SoundButtonClicked()
        {
            ChangeSprites(_soundButton.GetComponent<Image>(), _soundSprites);
            SoundButtonClickedAction?.Invoke();
        }

        private void MusicButtonClicked()
        {
            ChangeSprites(_musicButton.GetComponent<Image>(), _musicSprites);
            MusicButtonClickedAction?.Invoke();
        }

        private void ChangeSprites(Image image, List<Sprite> sprites)
        {
            image.sprite = image.sprite == sprites[0] ? sprites[1] : sprites[0];
        }

        private void CloseButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}
