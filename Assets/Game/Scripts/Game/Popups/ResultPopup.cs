using Game.Scripts.Audio;
using Game.Scripts.Enums;
using Game.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.Game.Popups
{
    public class ResultPopup : MonoBehaviour
    {
        [SerializeField, Header("Events")] private UnityEvent<bool> onResultActive;
        
		[SerializeField, Header("Links")] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject winContainer;
        [SerializeField] private GameObject loseContainer;

        [SerializeField, Header("Images")] private Image bodyImage;
        [SerializeField] private Image titleImage;
        
        [SerializeField, Header("Result Sprites")] private ResultSpritesContainer spritesContainer;

        private void OnValidate()
        {
            if (scoreText == null)
            {
                Debug.LogWarning($"{nameof(scoreText)} is null");
            }
            
            if (winContainer == null || loseContainer == null)
            {
                Debug.LogWarning($"{nameof(winContainer)} or {nameof(loseContainer)} is null");
            }
            
            if (bodyImage == null || titleImage == null)
            {
                Debug.LogWarning($"{nameof(bodyImage)} or {nameof(titleImage)} is null");
            }
        }
        
        public void SetPopupActive(bool isLevelDone, int reward)
        {
            gameObject.SetActive(true);
            winContainer.SetActive(isLevelDone);
            loseContainer.SetActive(!isLevelDone);

            if (isLevelDone)
            {
                WinResult(reward);
            }
            else
            {
                LoseResult();
            }
            
            onResultActive?.Invoke(isLevelDone);
        }

        public void SetInactive()
        {
            gameObject.SetActive(false);
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

        private void WinResult(int reward)
        {
            SetText(reward);
            SetBodySprite(spritesContainer.WinBodySprite);
            SetTitleSprite(spritesContainer.WinTitleSprite);
            AudioEffectsManager.PlaySound(AudioClips.ResultWin.ToString());
        }
        
        private void LoseResult()
        {
            SetBodySprite(spritesContainer.LoseBodySprite);
            SetTitleSprite(spritesContainer.LoseTitleSprite);
            AudioEffectsManager.PlaySound(AudioClips.ResultLose.ToString());
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

        private void SetBodySprite(Sprite spite)
        {
            bodyImage.sprite = spite;
        }

        private void SetTitleSprite(Sprite spite)
        {
            titleImage.sprite = spite;
        }

        private void SetText(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}