using UnityEngine;

namespace Game.Shop.Scripts
{
	[System.Serializable]
	public class SpritesContainer
	{
		[SerializeField, Header("Action Button Sprites")] private Sprite activeButtonSprite;
		[SerializeField] private Sprite inactiveButtonSprite;
		[SerializeField] private Sprite selectedButtonSprite;
		[SerializeField] private Sprite chooseButtonSprite;
		
		public Sprite ActiveButtonSprite => activeButtonSprite;
		public Sprite InactiveButtonSprite => inactiveButtonSprite;
		public Sprite SelectedButtonSprite => selectedButtonSprite;
		public Sprite ChooseButtonSprite => chooseButtonSprite;
	}
}