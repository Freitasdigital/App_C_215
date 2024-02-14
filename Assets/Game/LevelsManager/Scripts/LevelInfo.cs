using Game.LevelsManager.Scripts.Enums;
using UnityEngine;

namespace Game.LevelsManager.Scripts
{
	[System.Serializable]
	public class LevelInfo
	{
		[SerializeField, Header("Level Types")] private Levels level;
		
		[SerializeField, Min(0)] private int enemiesCount;

		public Levels Level => level;
		
		public int EnemiesCount => enemiesCount;
		public bool IsOpen => LevelsManager.Instance.IsPreviousLevelDone(level);
		
		public bool IsDone
		{
			get => PlayerPrefs.GetInt(level.ToString()) == 1;
			
			private set
			{
				PlayerPrefs.SetInt(level.ToString(), value ? 1 : 0);
				PlayerPrefs.Save();
			}
		}

		public void CheckEnemiesCount(int value)
		{
			if (IsDone) return;
			
			IsDone = value >= enemiesCount;
		}
	}
}