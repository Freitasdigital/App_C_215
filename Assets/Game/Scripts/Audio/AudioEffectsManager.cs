using System.Linq;
using UnityEngine;

namespace Game.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEffectsManager : MonoBehaviour
    {
        private static AudioEffectsManager _instance;
        
        [SerializeField] private AudioClip[] audioClips;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            
            _audioSource = GetComponent<AudioSource>();
        }
        
        public static void PlaySound(string soundName)
        {
            if (_instance)
            {
                _instance.PlaySoundByName(soundName);
            }
            else
            {
                Debug.LogWarning("AudioEffectsManager.Instance == null");
            }
        }

        private void PlaySoundByName(string soundName)
        {
            var clip = FindAudioClipByName(soundName);
            
            if (clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"Sound clip not found {soundName}");
            }
        }

        private AudioClip FindAudioClipByName(string soundName)
        {
            return audioClips.FirstOrDefault(x => x.name == soundName);
        }
    }
}
