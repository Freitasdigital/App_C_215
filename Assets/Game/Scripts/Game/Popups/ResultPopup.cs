using Game.Scripts.Audio;
using Game.Scripts.Enums;
using Game.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Game.Popups
{
    public class ResultPopup : MonoBehaviour
    {
		[SerializeField, Header("Links")] private TextMeshProUGUI scoreText;

        private void OnValidate()
        {
            if (scoreText == null)
            {
                Debug.LogWarning($"{nameof(scoreText)} is null");
            }
        }
        
        public void SetPopupActive(int score)
        {
            gameObject.SetActive(true);
            
            SetText(score);
            AudioEffectsManager.PlaySound(AudioClips.Result.ToString());
        }

        public void PlayAgain()
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
            LoadScene(Scenes.MenuScene.ToString());
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
        
        private void SetText(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}