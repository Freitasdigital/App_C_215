using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Text
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DisplayText : MonoBehaviour
    {
        protected TextMeshProUGUI Text => GetComponent<TextMeshProUGUI>();

        private void OnDestroy()
        {
            UnsubscribeFromEvent();
        }

        private void Start()
        {
            Load();
            SubscribeToEvent();
        }

        public virtual void SetText(int value)
        {
            Text.text = value.ToString();
        }
        
        public virtual void SetText(string text)
        {
            Text.text = text;
        }
        
        protected virtual void SubscribeToEvent()
        {
            
        }

        protected virtual void UnsubscribeFromEvent()
        {
            
        }

        protected virtual void Load()
        {
            
        }
    }
}