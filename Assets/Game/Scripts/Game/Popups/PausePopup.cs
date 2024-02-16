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
            if (LevelsManager.Scripts.LevelsManager.Instance != null)
            {
                var currentLevel = LevelsManager.Scripts.LevelsManager.Instance.GetCurrentLevel();

                LoadScene(currentLevel.Level.ToString());
            }
            else
            {
                Debug.LogWarning($"{nameof(LevelsManager)} is null");
            }
        }

        public void Exit()
        {
            if (isExitConfirmExist)
            {
                onExitClick?.Invoke();
            }
            else
            {
                LoadScene(Scenes.MenuScene.ToString());
            }
        }

        private void LoadScene(string scene)
        {
            if (ScenesManager.Instance != null)
            {
                ScenesManager.Instance.LoadScene(scene);
            }
            else
            {
                Debug.LogWarning($"{nameof(ScenesManager)} is null");
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
                Debug.LogWarning($"{nameof(GameLogic)} is null");
            }
        }
    }
}