using System;
using UnityEngine;

namespace Game.SettingsSwitch.Scripts
{
    public class VolumeManager : MonoBehaviour
    {
        public static VolumeManager Instance { get; private set; }

        public event Action<bool> OnMusicChanged;
        public event Action<bool> OnSoundChanged;
        
        public bool IsMusicOn { get; private set; }
        public bool IsSoundOn { get; private set; }

        private const string MusicPlayerPrefsKey = "MusicPlayerPrefsKey";
        private const string SoundPlayerPrefsKey = "SoundPlayerPrefsKey";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            
            LoadVolume();
        }

        public void SwitchMusic()
        {
            IsMusicOn = !IsMusicOn;
            OnMusicChanged?.Invoke(IsMusicOn);
            
            SaveVolume();
        }
        
        public void SwitchSound()
        {
            IsSoundOn = !IsSoundOn;
            OnSoundChanged?.Invoke(IsSoundOn);
            
            SaveVolume();
        }

        private void SaveVolume()
        {
            PlayerPrefs.SetInt(MusicPlayerPrefsKey, IsMusicOn ? 1 : 0);
            PlayerPrefs.SetInt(SoundPlayerPrefsKey, IsSoundOn ? 1 : 0);
            
            PlayerPrefs.Save();
        }

        private void LoadVolume()
        {
            IsMusicOn = PlayerPrefs.GetInt(MusicPlayerPrefsKey, 1) == 1;
            IsSoundOn = PlayerPrefs.GetInt(SoundPlayerPrefsKey, 1) == 1;
        }
    }
}