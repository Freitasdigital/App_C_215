using System;
using System.Collections;
using Game.Scripts.Preferences;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Game.GameLogic
{
	public class GameLogic : MonoBehaviour
	{
		public static GameLogic Instance { get; private set; }

		[SerializeField, Header("Events")] private UnityEvent onFirstPlayStart;
		[SerializeField] private UnityEvent onGameStart;
		[SerializeField] private UnityEvent<int> onGameOver;
		[SerializeField] private UnityEvent<int> onScoreChanged;

		[SerializeField, Header("Game Settings"), Min(0)] private float resultDelay = 2f;
		
		private bool _isFirstPlayed = true;
		private int _score;

		private void OnDestroy()
		{
			IsPauseActive(false);
		}

		private void Awake()
		{
			Instance = this;
		}
		
		private void Start()
		{
			Load();
			CheckFirstPlay();
		}
		
		public void SetFirstPlayDone()
		{
			_isFirstPlayed = false;
			PlayerPrefsManager.SetInt(Keys.FIRST_PLAY_KEY, _isFirstPlayed ? 1 : 0);
			
			IsPauseActive(false);
			Init();
		}

		public void AddScore(int value)
		{
			if (value <= 0) throw new ArgumentException(nameof(value));
			
			_score += value;
			onScoreChanged?.Invoke(_score);
		}
		
		public void IsPauseActive(bool isPaused)
		{
			Time.timeScale = isPaused ? 0f : 1f;
		}

		public void GameOver()
		{
			StartCoroutine(DisplayResult());
		}
		
		private void CheckFirstPlay()
		{
			if (_isFirstPlayed)
			{
				IsPauseActive(true);
				onFirstPlayStart?.Invoke();
			}
			else
			{
				Init();
			}
		}
		
		private void Init()
		{
			onGameStart?.Invoke();
		}

		private IEnumerator DisplayResult()
		{
			yield return new WaitForSeconds(resultDelay);
			
			Debug.Log("Game Over");
			
			Wallet.Wallet.AddMoney(_score);
			onGameOver?.Invoke(_score);
		}
        
		private void Load()
		{
			_isFirstPlayed = PlayerPrefsManager.GetInt(Keys.FIRST_PLAY_KEY, 1) == 1;
		}
	}
}