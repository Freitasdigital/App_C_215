using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Managers;
// using Spine;
// using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        // [SerializeField, Header("Links")] private SkeletonAnimation skeletonAnimation;
        //
        // [SerializeField, Header("Animations List")] private List<Animations> animationsList;
        //
        // private List<Skin> _skinsList;
        // private Animations _currentAnimation;
        //
        // private void OnValidate()
        // {
        //     if (skeletonAnimation == null)
        //     {
        //         Debug.LogWarning($"{nameof(skeletonAnimation)} is null");
        //     }
        //
        //     if (animationsList.Count <= 0)
        //     {
        //         Debug.LogWarning($"{nameof(animationsList)} is empty");
        //     }
        //
        // }
        //
        // private void Start()
        // {
        //     Init();
        // }
        //
        public void SetAnimation(Enums.State state)
        {
            // _currentAnimation = GetAnimation(state);
            //
            // if (_currentAnimation == null)
            //     throw new ArgumentNullException(nameof(_currentAnimation));
            //
            // skeletonAnimation.loop = _currentAnimation.Loop;
            // skeletonAnimation.AnimationName = _currentAnimation.AnimationName;
            // skeletonAnimation.timeScale = _currentAnimation.TimeScale;
        }
        
        public float GetCurrentAnimationDuration()
        {
            return 0f;
            //return skeletonAnimation.Skeleton.Data.FindAnimation(_currentAnimation.AnimationName).Duration;
        }
        //
        // private void Init()
        // {
        //     _skinsList = skeletonAnimation.Skeleton.Data.Skins.ToList();
        //     
            // if (GameManager.Instance != null)
            // {
            //     SetSkin(GameManager.Instance.PlayerId);
            // }
            // else
            // {
            //     Debug.LogWarning($"{nameof(GameManager.Instance)} is null");
            // }
        // }
        //
        // private void SetSkin(int index)
        // {
        //     skeletonAnimation.initialSkinName = $"{_skinsList[index].Name}";
        //     skeletonAnimation.Initialize(true);
        // }
        //
        // private Animations GetAnimation(Enums.State state)
        // {
        //     return animationsList.FirstOrDefault(x => x.State == state);
        // }
    }
}