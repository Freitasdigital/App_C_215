using UnityEngine;

namespace Game.Scripts.Game.Popups
{
	[System.Serializable]
	public class ResultSpritesContainer
	{
		[SerializeField] private Sprite winBodySprite;
		[SerializeField] private Sprite loseBodySprite;
		[SerializeField] private Sprite winTitleSprite;
		[SerializeField] private Sprite loseTitleSprite;

		public Sprite WinBodySprite => winBodySprite;
		public Sprite LoseBodySprite => loseBodySprite;
		public Sprite WinTitleSprite => winTitleSprite;
		public Sprite LoseTitleSprite => loseTitleSprite;
	}
}