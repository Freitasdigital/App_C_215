using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Game.Player
{
    public class BallsCount : MonoBehaviour
    {
        [SerializeField, Header("Events")] private UnityEvent<int> onLivesChanged;

        public int CurrentBalls { get; private set; }

        public void Init(int ballsAmount)
        {
            if (ballsAmount <= 1)
                throw new ArgumentOutOfRangeException(nameof(ballsAmount));
            
            CurrentBalls = ballsAmount;
            UpdateBallsCount();
        }

        public void AddBall()
        {
            ChangeBallsAmount(+1);
        }
        
        public void GetBall()
        {
            ChangeBallsAmount(-1);
        }

        private void ChangeBallsAmount(int amount)
        {
            CurrentBalls += amount;
            CurrentBalls = Mathf.Max(CurrentBalls, 0);

            UpdateBallsCount();
        }

        private void UpdateBallsCount()
        {
            onLivesChanged?.Invoke(CurrentBalls);
            Debug.Log($"Balls Count: {CurrentBalls}");
        }
    }
}