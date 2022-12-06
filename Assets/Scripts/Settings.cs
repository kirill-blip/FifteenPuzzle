using UnityEngine;
using System.IO;

namespace FifteenPuzzle
{

    public class Settings : MonoBehaviour
    {
        [SerializeField] private SettingsPanel _settingsPanel = null;

        public static bool CanPlaySound = true;
        public static bool CanPlayMusic = true;

        private const string _fileName = "Settings.json";

        public static System.Action CanPlaySoundChanged;
        public static System.Action CanPlayMusicChanged;

        private void Start()
        {
            Load();

            if (_settingsPanel is not null)
            {
                _settingsPanel.MusicButtonClickedAction += () =>
                {
                    CanPlayMusic = !CanPlayMusic;
                    CanPlayMusicChanged?.Invoke();
                    Save();
                };

                _settingsPanel.SoundButtonClickedAction += () =>
                {
                    CanPlaySound = !CanPlaySound;
                    CanPlaySoundChanged?.Invoke();
                    Save();
                };
            }
        }

        public void Save()
        {
            SettingsItem item = new SettingsItem(CanPlayMusic, CanPlaySound);

            string json = JsonUtility.ToJson(item);
            string path = Application.persistentDataPath + _fileName;

            File.WriteAllText(path, json);
        }

        public void Load()
        {
            string path = Application.persistentDataPath + _fileName;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                SettingsItem settingsItem = JsonUtility.FromJson<SettingsItem>(json);

                CanPlayMusic = settingsItem.CanPlayMusic;
                CanPlaySound = settingsItem.CanPlaySound;
            }
        }
    }
}