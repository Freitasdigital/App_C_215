using UnityEngine;

namespace Game.SettingsSwitch.Scripts
{
    public class LoadMusic : LoadAudioSettings
    {
        protected override void Load()
        {
            if (VolumeManager.Instance != null)
            {
                SwitchState(VolumeManager.Instance.IsMusicOn);
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
                VolumeManager.Instance.OnMusicChanged += SwitchState;
            }
            else
            {
                Debug.LogWarning("VolumeManager.Instance is null");
            }
        }

        protected override void UnsubscribeFromEvent()
        {
            VolumeManager.Instance.OnMusicChanged -= SwitchState;
        }

        protected override void SwitchState(bool state)
        {
            AudioSource.mute = !state;
        }
    }
}