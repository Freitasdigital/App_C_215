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
            if (ScenesManager.Instance != null)
            {
                ScenesManager.Instance.LoadScene(Scenes.GameScene);
            }
            else
            {
                Debug.LogWarning("ScenesManager.Instance is null");
            }
        }

        public void Exit()
        {
            if (ScenesManager.Instance != null)
            {
                ScenesManager.Instance.LoadScene(Scenes.MenuScene);
            }
            else
            {
                Debug.LogWarning("ScenesManager.Instance is null");
            }
        }
        
        private void SetText(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}