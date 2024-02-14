using System;
using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Game.Background
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class LoadBackground : MonoBehaviour
	{
		[SerializeField] private List<Sprite> placesSpritesList;

		private SpriteRenderer _spriteRenderer;

		private void OnValidate()
		{
			if (placesSpritesList.Count <= 0)
			{
				Debug.LogWarning($"{nameof(placesSpritesList)} is empty");
			}
		}
		
		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			
			Load();
		}

		private void Load()
		{
			if (GameManager.Instance != null)
			{
				var id = GameManager.Instance.LocationId;

				if (id < placesSpritesList.Count - 1)
					throw new ArgumentOutOfRangeException(nameof(id));
				
				_spriteRenderer.sprite = placesSpritesList[id];
			}
			else
			{
				Debug.LogWarning($"{nameof(GameManager)} is null");
			}
		}
	}
}