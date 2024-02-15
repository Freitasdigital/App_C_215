using Game.LevelsManager.Scripts.Enums;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private Levels level;
        
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
                ScenesManager.Instance.LoadScene(level.ToString());
            }
            else
            {
                Debug.LogWarning($"{nameof(ScenesManager)} is null");
            }
        }
    }
}