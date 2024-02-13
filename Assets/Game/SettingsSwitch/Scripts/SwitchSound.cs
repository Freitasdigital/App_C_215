using UnityEngine;

namespace Game.SettingsSwitch.Scripts
{
    public class SwitchSound : AudioStateSwitcher
    {
        protected override void Load()
        {
            if (VolumeManager.Instance != null)
            {
                SwitchSprite(VolumeManager.Instance.IsSoundOn);
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
                VolumeManager.Instance.OnSoundChanged += SwitchSprite;
            }
            else
            {
                Debug.LogWarning("VolumeManager.Instance is null");
            }
        }

        protected override void UnsubscribeFromEvent()
        {
            VolumeManager.Instance.OnSoundChanged -= SwitchSprite;
        }

        protected override void SwitchState()
        {
            VolumeManager.Instance.SwitchSound();
        }
    }
}