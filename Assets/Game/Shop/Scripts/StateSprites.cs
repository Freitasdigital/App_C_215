using System.Collections.Generic;
using UnityEngine;

namespace Game.Shop.Scripts
{
	public class StateSprites : MonoBehaviour
	{
		[SerializeField, Header("Items List")] private List<BuyItem> itemsList;

		[SerializeField, Header("Sprites Container")] private SpritesContainer spritesContainer; 

		private void OnValidate()
		{
			if (itemsList.Count <= 0)
			{
				Debug.LogWarning($"{nameof(itemsList)} is empty");
			}
			
			if (spritesContainer.ActiveButtonSprite == null || spritesContainer.InactiveButtonSprite == null)
			{
				Debug.LogWarning($"{nameof(spritesContainer.ActiveButtonSprite)} " +
				                 $"or {nameof(spritesContainer.InactiveButtonSprite)}  is null");
			}
            
			if (spritesContainer.SelectedButtonSprite == null || spritesContainer.ChooseButtonSprite == null)
			{
				Debug.LogWarning($"{nameof(spritesContainer.SelectedButtonSprite)} " +
				                 $"or {nameof(spritesContainer.ChooseButtonSprite)}  is null");
			}
		}

		private void Awake()
		{
			SetStateSprites();
		}

		private void SetStateSprites()
		{
			foreach (var item in itemsList)
			{
				item.SetStateSprites(spritesContainer);
			}
		}
	}
}