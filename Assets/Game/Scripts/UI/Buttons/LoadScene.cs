using Game.Scripts.Enums;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private Scenes scene;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(Load);
        }

        private void Load()
        {
            if (ScenesManager.Instance != null)
            {
                ScenesManager.Instance.LoadScene(scene);
            }
            else
            {
                Debug.LogWarning("ScenesManager.Instance is null");
            }
        }
    }
}