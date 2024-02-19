using System;
using System.Collections.Generic;
using System.Linq;
using Game.LevelsManager.Scripts.Enums;
using UnityEngine;

namespace Game.LevelsManager.Scripts
{
	[DefaultExecutionOrder(-100)]
	public class LevelsManager : MonoBehaviour
	{
		public static LevelsManager Instance { get; private set; }

		[SerializeField, Header("Default Level")] private Levels defaultLevel = Levels.Level1;
		
		[SerializeField, Header("Levels")] private List<LevelInfo> levelsList;
		
		[SerializeField, Header("State Container")] private StateContainer stateContainer;

		public IReadOnlyList<LevelInfo> LevelsList => levelsList;
		public StateContainer StateContainer => stateContainer;
		
		private LevelInfo _currentLevel;

		private void OnValidate()
		{
			if (levelsList.Count <= 0)
			{
				Debug.LogWarning($"{nameof(levelsList)} is empty");
			}
		}

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				Instance = this;
			}
			
			SetLevel(defaultLevel);

			CheckEnemiesCount(3);
		}

		public void SetLevel(Levels level)
		{
			var newLevel = levelsList.FirstOrDefault(x => x.Level == level);

			if (newLevel != null)
			{
				_currentLevel = newLevel;
			}
			else
			{
				Debug.LogWarning("New Level is null");
			}
		}
		
		public LevelInfo GetCurrentLevel()
		{
			if (_currentLevel == null)
				throw new ArgumentNullException(nameof(_currentLevel));

			return _currentLevel;
		}
		
		public LevelInfo GetLevelInfo(Levels level)
		{
			var levelInfo = levelsList.FirstOrDefault(x => x.Level == level);

			if (levelInfo == null)
				throw new ArgumentNullException(nameof(levelInfo));

			return levelInfo;
		}
		
		public bool IsScoreEnough(int score)
		{
			return score >= _currentLevel.EnemiesCount;
		}
		
		public int GetRequiredEnemiesCount()
		{
			return _currentLevel.EnemiesCount;
		}
		
		public int GetLevelIndex()
		{
			return levelsList.FindLastIndex(x => x.Level == _currentLevel.Level);
		}

		public void CheckEnemiesCount(int value)
		{
			_currentLevel.CheckEnemiesCount(value);
		}
		
		public bool IsPreviousLevelDone(Levels level)
		{
			if (level == Levels.Level1)
				return true;
			
			var previousLevel = levelsList[levelsList.FindIndex(x => x.Level == level) - 1];

			if (previousLevel == null)
				throw new ArgumentNullException(nameof(previousLevel));
				
			return previousLevel.IsDone;
		}
		
		public void LoadNextLevel()
		{
			_currentLevel = _currentLevel.Level == Levels.Level10
				? levelsList.Last()
				: levelsList[levelsList.FindIndex(x => x.Level == _currentLevel.Level) + 1];
			
			Debug.Log($"Current Level is: {_currentLevel.Level.ToString()}");
		}
	}
}