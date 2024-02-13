using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.LastChance.Scripts.Animation;
using Game.LastChance.Scripts.Enums;
using Game.Scripts.Audio;
using Game.Scripts.Enums;
using Game.Scripts.Utils;
using Game.Scripts.Wallet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.LastChance.Scripts
{
    public class LastChanceLogic : MonoBehaviour
    {
        [SerializeField, Header("Events")] private UnityEvent<int> onHeartWin;
        [SerializeField] private UnityEvent onSpinEnded;
        
        [SerializeField, Header("Links")] private List<LastChanceElement> lastChanceElementsList;
        [SerializeField] private SpriteAnimation[] elementsSprite;
        [SerializeField] private Button spinButton;
        
        [SerializeField, Header("Spin Settings")] private float spinDuration = 3f;

        private readonly List<int> _winIndexList = new List<int>();
        
        private void OnDisable()
        {
            this.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            spinButton.interactable = true;
        }

        public void Spin()
        {
            _winIndexList.Clear();
            
            spinButton.interactable = false;
            AudioEffectsManager.PlaySound(AudioClips.LastChanceSpin.ToString());

            GetIndex();
            StartCoroutine(CheckResult());
        }

        private void GetIndex()
        {
            var index = 0;
            
            var spriteIndex = lastChanceElementsList.
                Select(x => x.Probability).ToArray().Probability();

            CheckIndex(spriteIndex, index);
        }

        private void CheckIndex(int spriteIndex, int index)
        {
            if (spriteIndex >= 0)
            {
                for (int i = 0; i < elementsSprite.Length; i++)
                {
                    index = SetIndexToElement(index, spriteIndex);
                }
            }
            else
            {
                for (int i = 0; i < elementsSprite.Length; i++)
                {
                    var randomIndex = Random.Range(0, lastChanceElementsList.Count);

                    index = SetIndexToElement(index, randomIndex);
                }
            }
        }

        private int SetIndexToElement(int index, int spriteIndex)
        {
            _winIndexList.Add(spriteIndex);

            SetSpritesToElement(index, spriteIndex);
            index++;
            
            return index;
        }

        private void SetSpritesToElement(int elementIndex, int winIndex)
        {
            var spritesArray = lastChanceElementsList.Select(x => x.Sprite).ToArray();
            
            elementsSprite[elementIndex].SetSpritesSettings(spritesArray, spinDuration, winIndex);
        }

        private IEnumerator CheckResult()
        {
            const float offset = 0.5f;
            yield return new WaitForSecondsRealtime(spinDuration + offset);
            
            AudioEffectsManager.PlaySound(AudioClips.LastChanceResult.ToString());

            if (_winIndexList.All(x => x == _winIndexList.First()))
            {
                GetReward(_winIndexList.First());
            }
            else
            {
                onSpinEnded?.Invoke();
            }
            
            onSpinEnded?.Invoke();
            StartCoroutine(ClosePopup());
        }

        private void GetReward(int winIndex)
        {
            var winElement = lastChanceElementsList.ToArray()[winIndex];
            var rewardType = winElement.RewardType;
            
            if (rewardType == RewardType.Heart)
            {
                onHeartWin?.Invoke(winElement.Reward);
            }
            else if (rewardType == RewardType.Money)
            {
                Wallet.AddMoney(winElement.Reward);
                
                onSpinEnded?.Invoke();
            }
            else if (rewardType == RewardType.Empty)
            {
                onSpinEnded?.Invoke();
            }
            
            Debug.Log($"Win type: {rewardType}, Reward: {winElement.Reward}");
        }

        private IEnumerator ClosePopup()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            
            this.gameObject.SetActive(false);
        }
    }
}