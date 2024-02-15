using Game.Scripts.Enums;
using Game.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.LevelsManager.Scripts
{
    public class ResultPopup : MonoBehaviour
    {
        [SerializeField, Header("Text")] private TextMeshProUGUI levelText;
		[SerializeField] private TextMeshProUGUI scoreText;
        
        [SerializeField, Header("Result Colors")] private Color winScoreValueColor;
        [SerializeField] private Color loseScoreValueColor;
        
        [SerializeField, Header("Reward")] private GameObject rewardPanel;
        [SerializeField] private TextMeshProUGUI rewardText;
        
        [SerializeField, Header("Action Button State")] private Image actionButtonImage;
        [SerializeField] private Sprite againSprite;
        [SerializeField] private Sprite nextSprite;

        private bool _isPlayerWin;

        private void OnValidate()
        {
            if (levelText == null || scoreText == null)
            {
                Debug.LogWarning($"{nameof(levelText)} or {nameof(scoreText)} is null");
            }
            
            if (rewardPanel == null || rewardText == null)
            {
                Debug.LogWarning($"{nameof(rewardPanel)} or {nameof(rewardText)} is null");
            }
            
            if (actionButtonImage == null)
            {
                Debug.LogWarning($"{nameof(actionButtonImage)} is null");
            }
            
            if (againSprite == null || nextSprite == null)
            {
                Debug.LogWarning($"{nameof(againSprite)} or {nameof(nextSprite)} is null");
            }
        }

        public void SetPopupActive(int score, int reward)
        {
            gameObject.SetActive(true);

            if (LevelsManager.Instance != null)
            {
                CheckScore(score, reward);
                SetLevelText(LevelsManager.Instance.GetCurrentLevel().Level.ToString());
            }
            else
            {
                Debug.LogWarning($"{nameof(LevelsManager.Instance)} is null");
            }
        }

        public void ActionButton()
        {
            if (_isPlayerWin)
            {
                LevelsManager.Instance.LoadNextLevel();
            }
            
            if (ScenesManager.Instance != null)
            {
                //ScenesManager.Instance.LoadScene(Scenes.GameScene);
            }
            else
            {
                Debug.LogWarning($"{nameof(ScenesManager.Instance)} is null");
            }
        }
        
        private void CheckScore(int score, int reward)
        {
            if (LevelsManager.Instance.IsScoreEnough(score))
            {
                WinState(score, reward);
            }
            else
            {
                LoseState(score);
            }
        }
        
        private void WinState(int score, int reward)
        {
            _isPlayerWin = true;
            rewardPanel.SetActive(true);
            
            SetRewardValueText(reward);
            SetActionButtonSprite(nextSprite);
            SetCurrentScoreText(score, LevelsManager.Instance.GetRequiredScore());
            
            //AudioEffectsManager.Playing(AudioClips.ResultWin.ToString());
        }

        private void LoseState(int score)
        {
            _isPlayerWin = false;
            rewardPanel.SetActive(false);
            
            SetActionButtonSprite(againSprite);
            SetCurrentScoreText(score, LevelsManager.Instance.GetRequiredScore());
            
            //AudioEffectsManager.Playing(AudioClips.ResultLose.ToString());
        }

        public void Exit()
        {
            if (ScenesManager.Instance != null)
            {
                ScenesManager.Instance.LoadScene(Scenes.MenuScene.ToString());
            }
            else
            {
                Debug.LogWarning($"{nameof(ScenesManager.Instance)} is null");
            }
        }

        private void SetActionButtonSprite(Sprite sprite)
        {
            actionButtonImage.sprite = sprite;
        }
        
        private void SetLevelText(string text)
        {
            levelText.text = text;
        }
        
        private void SetCurrentScoreText(int currentScore, int neededScore)
        {
            var colorString = ColorUtility.ToHtmlStringRGBA(_isPlayerWin ? winScoreValueColor : loseScoreValueColor);
            var coloredText = $"<color=#{colorString}>{currentScore}/</color><color=white>{neededScore}</color>";
            
            scoreText.text = coloredText;
        }
        
        private void SetRewardValueText(int value)
        {
            rewardText.text = value.ToString();
        }
    }
}