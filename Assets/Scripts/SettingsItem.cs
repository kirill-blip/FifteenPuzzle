namespace FifteenPuzzle
{
    public class SettingsItem
    {
        public SettingsItem(bool canPlayMusic, bool canPlaySound, Language language)
        {
            CanPlayMusic = canPlayMusic;
            CanPlaySound = canPlaySound;
            Language = language;
        }

        public bool CanPlayMusic;
        public bool CanPlaySound;
        public Language Language;
    }
}