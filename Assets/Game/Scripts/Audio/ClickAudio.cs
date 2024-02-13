using Game.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Audio
{
    [RequireComponent(typeof(Button))]
    public class ClickAudio : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        
            _button.onClick.AddListener(PlayClick);
        }

        private void PlayClick()
        {
            AudioEffectsManager.PlaySound(AudioClips.Click.ToString());
        }
    }
}