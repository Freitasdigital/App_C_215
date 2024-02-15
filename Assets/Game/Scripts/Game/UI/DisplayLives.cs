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

		private void OnValidate()
		{
			if (activeSprite == null || inactiveSprite == null)
			{
				Debug.LogWarning($"{nameof(activeSprite)} or {nameof(inactiveSprite)} is null");
			}
		}

		public void SetActiveLives(int count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException(nameof(count));

			for (int i = 0; i < livesList.Count; i++)
			{
				livesList[i].sprite = i < count ? activeSprite : inactiveSprite;
			}
		}
	}
}