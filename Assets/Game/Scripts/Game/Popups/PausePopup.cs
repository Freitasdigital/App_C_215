using Game.Scripts.Enums;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Game.Popups
{
    public class PausePopup : MonoBehaviour
    {
        [SerializeField, Header("Events")] private UnityEvent onExitClick;
        
        [SerializeField, Header("Pause Settings")] private bool isExitConfirmExist;

        public void SetPauseActive()
        {
            ChangePauseState(true);
        }
        
        public void Continue()
        {
            ChangePauseState(false);
        }
        
        public void Again()
        {
            LoadScene(Scenes.GameScene);
        }
        
        public void Exit()
        {
            if (isExitConfirmExist)
            {
                onExitClick?.Invoke();
            }
            else
            {
                LoadScene(Scenes.MenuScene);
            }
        }

        private void LoadScene(Scenes scene)
        {
            if (ScenesManager.Instance != null)
            {
                ScenesManager.Instance.LoadScene(scene);
            }
            else
            {
                Debug.LogWarning($"{nameof(ScenesManager)}.Instance is null");
            }
        }

        private void ChangePauseState(bool state)
        {
            gameObject.SetActive(state);

            if (GameLogic.GameLogic.Instance != null)
            {
                GameLogic.GameLogic.Instance.IsPauseActive(state);
            }
            else
            {
                Debug.LogWarning("GameLogic.Instance is null");
            }
        }
    }
}