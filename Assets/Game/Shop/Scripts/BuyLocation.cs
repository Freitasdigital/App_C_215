using System;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Shop.Scripts
{
	public class BuyLocation : BuyItem
	{
		protected override void SubscribeToEvent()
		{
			base.SubscribeToEvent();

			if (GameManager.Instance != null)
			{
				GameManager.Instance.OnLocationIdChanged += CheckId;
			}
			else
			{
				Debug.LogWarning($"{nameof(GameManager)} is null");
			}
		}

		protected override void UnsubscribeFromEvent()
		{
			base.UnsubscribeFromEvent();
			
			GameManager.Instance.OnLocationIdChanged -= CheckId;
		}

		protected override bool IsCurrentIdSelected()
		{
			if (GameManager.Instance == null)
				throw new ArgumentNullException(nameof(GameManager.Instance));
			
			return GameManager.Instance.LocationId == id;
		}

		protected override void SetId()
		{
			if (GameManager.Instance != null)
			{
				GameManager.Instance.SetLocationId(id);
			}
			else
			{
				Debug.LogWarning($"{nameof(GameManager)} is null");
			}
		}
	}
}