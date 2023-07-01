using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace FifteenPuzzle
{
	public class Settings : MonoBehaviour
	{
		[SerializeField] private SettingsPanel _settingsPanel = null;

		public static bool CanPlaySound = true;
		public static bool CanPlayMusic = true;
		public static Language Language = Language.RU;

		public List<Locale> Locales;

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

				_settingsPanel.LanguageButtonClickedAction += () =>
				{
					Language = Language == Language.EN ? Language.RU : Language.EN;

					LocalizationSettings.SelectedLocale = Locales.Find(x => x.name == Language.ToString());
					Save();
				};
			}
		}

		public void Save()
		{
			SettingsItem item = new SettingsItem(CanPlayMusic, CanPlaySound, Language);

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

				Language = settingsItem.Language;
			}

			LocalizationSettings.SelectedLocale = Locales.Find(x => x.name == Language.ToString());
		}

#if UNITY_EDITOR
		[MenuItem("Data/Delete data")]
		public static void Delete()
		{
			File.Delete(Application.persistentDataPath + _fileName);
		}
#endif
	}
}