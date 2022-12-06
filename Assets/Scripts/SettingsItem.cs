namespace FifteenPuzzle
{
    public class SettingsItem
    {
        public SettingsItem(bool canPlayMusic, bool canPlaySound)
        {
            CanPlayMusic = canPlayMusic;
            CanPlaySound = canPlaySound;
        }

        public bool CanPlayMusic;
        public bool CanPlaySound;
    }
}