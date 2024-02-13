using UnityEngine;

namespace Game.SettingsSwitch.Scripts
{
    public class LoadSound : LoadAudioSettings
    {
        protected override void Load()
        {
            if (VolumeManager.Instance != null)
            {
                SwitchState(VolumeManager.Instance.IsSoundOn);
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
                VolumeManager.Instance.OnSoundChanged += SwitchState;
            }
            else
            {
                Debug.LogWarning("VolumeManager.Instance is null");
            }
        }

        protected override void UnsubscribeFromEvent()
        {
            VolumeManager.Instance.OnSoundChanged -= SwitchState;
        }

        protected override void SwitchState(bool state)
        {
            AudioSource.mute = !state;
        }
    }
}