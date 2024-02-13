using UnityEngine;

namespace Game.SettingsSwitch.Scripts
{
    public class SwitchMusic : AudioStateSwitcher
    {
        protected override void Load()
        {
            if (VolumeManager.Instance != null)
            {
                SwitchSprite(VolumeManager.Instance.IsMusicOn);
            }
            else
            {
                Debug.LogWarning("VolumeManager.Instance is null");
            }
        }

        protected override void SubscribeToEvent()
        {
            if (VolumeManager.Instance != null)
            {
                VolumeManager.Instance.OnMusicChanged += SwitchSprite;
            }
            else
            {
                Debug.LogWarning("VolumeManager.Instance is null");
            }
        }

        protected override void UnsubscribeFromEvent()
        {
            VolumeManager.Instance.OnMusicChanged -= SwitchSprite;
        }

        protected override void SwitchState()
        {
            VolumeManager.Instance.SwitchMusic();
        }
    }
}