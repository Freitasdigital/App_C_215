using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Game.Player
{
    public class Lives : MonoBehaviour
    {
        [SerializeField, Header("Events")] private UnityEvent<int> onLivesChanged;

        [SerializeField, Header("Max Lives"), Min(1)] private int maxLives = 3;

        public int CurrentLives { get; private set; }
        
        private void Awake()
        {
            CurrentLives = maxLives;
        }

        public void RestoreHealth(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            ChangeLiveAmount(amount);
        }
        
        public void TakeDamage(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            ChangeLiveAmount(-amount);
        }

        private void ChangeLiveAmount(int amount)
        {
            CurrentLives += amount;
            CurrentLives = Mathf.Clamp(CurrentLives, 0, maxLives);
            
            onLivesChanged?.Invoke(CurrentLives);
            Debug.Log($"Player Lives: {CurrentLives}");
        }
    }
}