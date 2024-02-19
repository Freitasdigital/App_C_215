using System;
using Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.UI
{
	public class DisplayBalls : MonoBehaviour
	{
		[SerializeField, Header("Ball Image Prefab")] private Image ballImagePrefab;

		private void OnValidate()
		{
			if (ballImagePrefab == null)
			{
				Debug.LogWarning($"{nameof(ballImagePrefab)} is null");
			}
		}

		public void SetActiveLives(int count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException(nameof(count));

			transform.DestroyChildObjects();
			
			for (int i = 0; i < count; i++)
			{
				Instantiate(ballImagePrefab, transform);
			}
		}
	}
}