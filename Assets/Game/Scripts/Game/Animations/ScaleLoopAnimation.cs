using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Game.Animations
{
    public class ScaleLoopAnimation : MonoBehaviour
    {
        [SerializeField, Header("Animation Settings")] private float scaleValue = 0.8f;
        [SerializeField] private float animationTime = 0.5f;
        
        private Tween _tween;

        private void OnDestroy()
        {
            StopTween();
        }
        
        private void Start()
        {
            ScaleLoop();
        }

        private void ScaleLoop()
        {
            if (transform == null) return;

            _tween = transform.DOScale(scaleValue, animationTime).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        }

        private void StopTween()
        {
            if (_tween != null && _tween.IsActive())
            {
                _tween.Kill();
            }
        }
    }
}