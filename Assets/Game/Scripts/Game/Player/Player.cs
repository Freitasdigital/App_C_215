using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using Game.Scripts.Game.Animations;
using Game.Trajectory.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Game.Player
{
	[RequireComponent(typeof(CharacterAnimation))]
	public class Player : MonoBehaviour
	{
		public static Player Instance { get; private set; }

		[SerializeField, Header("Events")] private UnityEvent onBallInit;
		
		[SerializeField, Header("Links")] private GunBehavior gun;
		
		[SerializeField, Header("Throw Settings")] private float throwDuration = 0.2f;
		
        public int BallsCount => _ballsCount.CurrentBalls;
        
		private BallsCount _ballsCount;
		private CharacterAnimation _characterAnimation;

		private List<BallBehavior> _ballsList;

		private void OnValidate()
		{
			if (gun == null)
			{
				Debug.LogWarning($"{nameof(gun)} is null");
			}
		}

		private void OnDestroy()
		{
			UnsubscribeFromAllEvents();
		}

		private void Awake()
		{
			Instance = this;
			
			_ballsCount = GetComponent<BallsCount>();
			_characterAnimation = GetComponent<CharacterAnimation>();
		}

		private void Start()
		{
			Init();
		}
		
		public void AddBall()
		{
			_ballsCount.AddBall();
			BallInit();
		}

		public void SetTrajectoryActive()
		{
			gun.SetTrajectoryActive();
		}

		public void ThrowBall()
		{
			SetAnimation(State.Throwing);
			_ballsCount.GetBall();
			gun.Shot();
			
			SubscribeToEvent(gun.CurrentBall);
			StartCoroutine(SetIdleAnimation(throwDuration));
		}
		
		private void SetAnimation(State state)
		{
			_characterAnimation.SetAnimation(state);
		}

		private void SubscribeToEvent(BallBehavior ball)
		{
			if (ball != null)
			{
				ball.onBallDestroyed += CheckBallsCount;
				_ballsList.Add(ball);
			}
			else
			{
				Debug.LogWarning($"{nameof(ball)} is null");
			}
		}

		private void UnsubscribeFromAllEvents()
		{
			foreach (var ball in _ballsList)
			{
				ball.onBallDestroyed -= CheckBallsCount;
			}
			
			_ballsList.Clear();
		}

		private void CheckBallsCount()
		{
			if (GameLogic.GameLogic.Instance != null)
			{
				if (BallsCount <= 0)
				{
					GameLogic.GameLogic.Instance.CheckDefeatedEnemies();
				}
				else
				{
					BallInit();
				}
			}
			else
			{
				Debug.LogWarning($"{nameof(GameLogic)} is null");
			}
		}

		private void Init()
		{
			if (LevelsManager.Scripts.LevelsManager.Instance != null)
			{
				_ballsCount.Init(LevelsManager.Scripts.LevelsManager.Instance.GetCurrentLevel().BallsAmount);
				_ballsList = new List<BallBehavior>();
				BallInit();
			}
			else
			{
				Debug.LogWarning($"{nameof(LevelsManager)} is null");
			}
		}

		private void BallInit()
		{
			onBallInit?.Invoke();
		}

		private IEnumerator SetIdleAnimation(float duration)
		{
			yield return new WaitForSeconds(duration);
			
			SetAnimation(State.Idling);
		}
	}
}