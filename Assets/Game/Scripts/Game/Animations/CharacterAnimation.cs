using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Spine.Unity;

namespace Game.Scripts.Game.Animations
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField, Header("Links")] private SkeletonAnimation skeletonAnimation;
        
        [SerializeField, Header("Animations List")] private List<Animations> animationsList;
        
        private Animations _currentAnimation;
        
        private void OnValidate()
        {
            if (skeletonAnimation == null)
            {
                Debug.LogWarning($"{nameof(skeletonAnimation)} is null");
            }
        
            if (animationsList.Count <= 0)
            {
                Debug.LogWarning($"{nameof(animationsList)} is empty");
            }
        
        }
        
        public void SetAnimation(Enums.State state)
        {
            _currentAnimation = GetAnimation(state);
            
            if (_currentAnimation == null)
                throw new ArgumentNullException(nameof(_currentAnimation));
            
            skeletonAnimation.loop = _currentAnimation.Loop;
            skeletonAnimation.AnimationName = _currentAnimation.AnimationName;
            skeletonAnimation.timeScale = _currentAnimation.TimeScale;
        }
        
        public float GetCurrentAnimationDuration()
        {
            return skeletonAnimation.Skeleton.Data.FindAnimation(_currentAnimation.AnimationName).Duration;
        }
        
        private Animations GetAnimation(Enums.State state)
        {
            return animationsList.FirstOrDefault(x => x.State == state);
        }
    }
}