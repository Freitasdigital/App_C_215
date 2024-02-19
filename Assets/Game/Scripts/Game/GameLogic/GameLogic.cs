using System.Collections;
using Game.LevelsManager.Scripts;
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
		[SerializeField] private UnityEvent<bool, int> onGameOver;

		[SerializeField, Header("Game Settings"), Min(0)] private float resultDelay = 2f;
		
		private LevelInfo _currentLevel;
		
		private bool _isFirstPlayed = true;
		private bool _isEnemiesDefeated;

		private int _enemiesCount;
		private int _defeatedEnemiesCount;

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

		public void AddDefeatedEnemy()
		{
			_defeatedEnemiesCount++;

			CheckEnemiesAmount();
		}
		
		public void IsPauseActive(bool isPaused)
		{
			Time.timeScale = isPaused ? 0f : 1f;
		}

		public void CheckDefeatedEnemies()
		{
			if (_isEnemiesDefeated) return;
			
			GameOver();
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
		
		private void CheckEnemiesAmount()
		{
			if (_defeatedEnemiesCount < _enemiesCount) return;
			
			_isEnemiesDefeated = true;
			GameOver();
		}
		
		private void GameOver()
		{
			_currentLevel.CheckEnemiesCount(_defeatedEnemiesCount);
			
			StartCoroutine(DisplayResult());
		}
		
		private void Init()
		{
			if (LevelsManager.Scripts.LevelsManager.Instance != null)
			{
				_currentLevel = LevelsManager.Scripts.LevelsManager.Instance.GetCurrentLevel();

				_enemiesCount = _currentLevel.EnemiesCount;
			}
			else
			{
				Debug.LogWarning($"{nameof(LevelsManager)} is null");
			}
			
			onGameStart?.Invoke();
		}

		private IEnumerator DisplayResult()
		{
			yield return new WaitForSeconds(resultDelay);
			
			Debug.Log("Game Over");
			
			AddReward();
			onGameOver?.Invoke(_isEnemiesDefeated, _currentLevel.LevelReward);
		}

		private void AddReward()
		{
			if (!_isEnemiesDefeated) return;
			
			Wallet.Wallet.AddMoney(_currentLevel.LevelReward);
		}

		private void Load()
		{
			_isFirstPlayed = PlayerPrefsManager.GetInt(Keys.FIRST_PLAY_KEY, 1) == 1;
		}
	}
}