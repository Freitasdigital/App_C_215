using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Game.Movements
{
    public class LoopVertical : BaseMove
    {
        [SerializeField, Header("Loop Type")] private bool isYoyoLoop;
        
        [SerializeField, Header("Animation Settings"), Min(0)] private float duration = 1f;
        [SerializeField] private float movePosition;

        private void Start()
        {
            Move(movePosition, duration);
        }
        
        public override void Move(float endDestination, float durationTime)
        {
            if (transform == null) return;

            var loopType = isYoyoLoop ? LoopType.Yoyo : LoopType.Restart;
            
            _tween = transform.DOMoveY(endDestination, durationTime)
                .SetEase(Ease.Linear).SetLoops(-1, loopType);
        }
    }
}