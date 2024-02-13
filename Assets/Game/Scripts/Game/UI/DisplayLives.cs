using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.UI
{
	public class DisplayLives : MonoBehaviour
	{
		[SerializeField, Header("Lives Images List")] private List<Image> livesList;

		[SerializeField, Header("Sprites")] private Sprite activeSprite;
		[SerializeField] private Sprite inactiveSprite;

		private void OnEnable()
		{
			LoadLives();
		}

		private void Start()
		{
			LoadLives();
		}
		
		public void SetActiveLives(int count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException(nameof(count));

			if (activeSprite != null && inactiveSprite != null)
			{
				for (int i = 0; i < livesList.Count; i++)
				{
					livesList[i].sprite = i < count ? activeSprite : inactiveSprite;
				}
			}
			else
			{
				Debug.LogWarning("Active Sprite inactive Sprite or is null");
			}
		}
		
		private void LoadLives()
		{
			if (Player.Player.Instance != null)
			{
				SetActiveLives(Player.Player.Instance.Lives);
			}
			else
			{
				Debug.LogWarning("Player.Instance is null");
			}
		}
	}
}