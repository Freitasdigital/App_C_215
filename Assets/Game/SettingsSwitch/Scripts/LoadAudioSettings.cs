using UnityEngine;

namespace Game.SettingsSwitch.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class LoadAudioSettings : MonoBehaviour
    {
        protected AudioSource AudioSource;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        
        private void OnDestroy()
        {
            UnsubscribeFromEvent();
        }

        private void Start()
        {
            Load();
            SubscribeToEvent();
        }
        
        protected abstract void Load();

        protected abstract void SubscribeToEvent();

        protected abstract void UnsubscribeFromEvent();

        protected abstract void SwitchState(bool state);
    }
}