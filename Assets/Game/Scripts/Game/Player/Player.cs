using System;
using System.Collections;
using Game.Scripts.Game.Interfaces;
using Game.Scripts.Game.Player.States;
using UnityEngine;

namespace Game.Scripts.Game.Player
{
	[RequireComponent(typeof(PlayerController))]
	public class Player : MonoBehaviour, IDamageable
	{
		public static Player Instance { get; private set; }

		public event Action<bool> OnControlActive;
		
        public int Lives => _lives.CurrentLives;
        
		private Lives _lives;
		private PlayerController _playerController;
		private PlayerAnimation _playerAnimation;
		
		private StateMachine _stateMachine;
		
		private void Awake()
		{
			Instance = this;
			
			_lives = GetComponent<Lives>();
			_playerController = GetComponent<PlayerController>();
			_playerAnimation = GetComponent<PlayerAnimation>();
		}

		private void Start()
		{
			Init();
		}
		
		public void SetState(IState state)
		{
			_stateMachine.SwitchState(state);
		}
		
		public void SetState(IState state, float delay)
		{
			StartCoroutine(StateDelay(state, delay));
		}
		
		public void SetAnimation(Enums.State state)
		{
			_playerAnimation.SetAnimation(state);
		}
		
		public float GetCurrentAnimationDuration()
		{
			return _playerAnimation.GetCurrentAnimationDuration();
		}
		
		public void RestoreHealth(int amount)
		{
			if (amount <= 0)
				throw new ArgumentOutOfRangeException(nameof(amount));
			
			_lives.RestoreHealth(amount);
		}
		
		public void TakeDamage(int amount)
		{
			if (amount <= 0)
				throw new ArgumentOutOfRangeException(nameof(amount));
			
			_lives.TakeDamage(amount);
			
			if (GameLogic.GameLogic.Instance != null)
			{
				if (Lives > 0)
				{
					//GameLogic.GameLogic.Instance.Restart();
				}
				else
				{
					GameLogic.GameLogic.Instance.GameOver();
				}
			}
			else
			{
				Debug.LogWarning($"{nameof(GameLogic)} is null");
			}
		}
        
		public void SetControlActive()
		{
			OnControlActive?.Invoke(true);
		}
		
		public void SetControlInactive()
		{
			OnControlActive?.Invoke(false);
		}

		private void Init()
		{
			_stateMachine = new StateMachine(this);
			_stateMachine.Initialize(new IdleState());
		}
		
		private IEnumerator StateDelay(IState state, float duration)
		{
			yield return new WaitForSeconds(duration);
			
			SetState(state);
		}
	}
}