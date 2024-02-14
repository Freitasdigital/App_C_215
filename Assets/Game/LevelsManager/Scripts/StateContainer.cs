using UnityEngine;

namespace Game.LevelsManager.Scripts
{
	[System.Serializable]
	public class StateContainer
	{
		[SerializeField, Header("State Sprites")] private Sprite openSprite;
		[SerializeField] private Sprite lockSprite;
		[SerializeField] private Sprite doneSprite;
		
		[SerializeField, Header("State Text Color")] private Color openColor;
		[SerializeField] private Color lockColor;
		[SerializeField] private Color doneColor;
		
		public Sprite OpenSprite => openSprite;
		public Sprite LockSprite => lockSprite;
		public Sprite DoneSprite => doneSprite;
		
		public Color OpenColor => openColor;
		public Color LockColor => lockColor;
		public Color DoneColor => doneColor;
	}
}