using UnityEngine;
using UnityEngine.UI;

namespace Game.SettingsSwitch.Scripts
{
    public abstract class AudioStateSwitcher : MonoBehaviour
    {
        [SerializeField, Header("State Sprites")] private Sprite enableSprite;
        [SerializeField] private Sprite disableSprite;

        private Image _image;
        private Button _button;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(SwitchState);
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
        
        protected abstract void SwitchState();

        protected void SwitchSprite(bool state)
        {
            if (enableSprite != null && disableSprite != null)
            {
                _image.sprite = state ? enableSprite : disableSprite;
            }
            else
            {
                Debug.LogWarning("Sprites is null");
            }
        }
    }
}